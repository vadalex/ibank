using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using WebClient.Filters;
using WebClient.Models;
using WebMatrix.WebData;
using System.Security.Cryptography;
using DAL;
using Entities;

namespace WebClient.Controllers
{
    public class LoginController : Controller
    {
        private Repositories _repo = new Repositories();
        private const int _expiration_time_sec = 60;

        [HttpGet]
        [NonAuthorizedOnly]
        public ActionResult Index()
        {
            HttpRuntime.Cache.Remove(HttpContext.Request.UserHostAddress + HttpContext.Request.UserAgent);
            var loginModel = new LoginModel();
            return View(loginModel);
        }

        [HttpPost]
        [NonAuthorizedOnly]
        public ActionResult Index(LoginModel loginModel)
        {
            HttpRuntime.Cache.Remove(HttpContext.Request.UserHostAddress + HttpContext.Request.UserAgent);
            if (ModelState.IsValid && Membership.ValidateUser(loginModel.Name, loginModel.Password))
            {
                Customer customer = _repo.Customers.GetAll(c => (c.Login == loginModel.Name) && (c.Passoword == loginModel.Password)).ToList().First();
                if (customer.AccessCards.First().IsBlocked)
                {
                    return View("AccountBlocked");
                }
                ResetCookie(loginModel);
                return RedirectToAction("SecretCode");
            }

            ModelState.AddModelError("", "Неправильный логин или пароль!");
            return View(loginModel);
        }

        [HttpGet]
        [NonAuthorizedOnly]
        public ActionResult SecretCode()
        {
            HttpCookie cookie = HttpContext.Request.Cookies.Get(".SECURECODE");
            CookieUserInfo userInfo = (CookieUserInfo)HttpRuntime.Cache.Get(HttpContext.Request.UserHostAddress + HttpContext.Request.UserAgent);

            if ((cookie == null) || (userInfo == null) || (cookie.Value != userInfo.Cookie.Value))
            {
                return RedirectToAction("Index");
            }

            Customer customer = _repo.Customers.GetAll(c => (c.Login == userInfo.LoginModel.Name) && (c.Passoword == userInfo.LoginModel.Password)).ToList().First();
            var card = customer.AccessCards.First();
            var codeCount = _repo.AccessCodes.GetAll(c => c.AccessCardID == card.AccessCardID).Count();            

            ModelState.Remove("CodeIndex");
            var secretCodeModel = new SecretCodeModel()
            {
                CodeIndex = (new Random()).Next(1, codeCount),
                Remaining = GetEnterCodeRemaining(card)
            };

            ResetCookie(userInfo.LoginModel);
            return View(secretCodeModel);
        }

        [HttpPost]
        [NonAuthorizedOnly]
        public ActionResult SecretCode(SecretCodeModel secretCode)
        {
            try
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get(".SECURECODE");
                CookieUserInfo userInfo = (CookieUserInfo)HttpRuntime.Cache.Get(HttpContext.Request.UserHostAddress + HttpContext.Request.UserAgent);

                if ((cookie == null) || (userInfo == null) || (cookie.Value != userInfo.Cookie.Value))
                {
                    return RedirectToAction("Index");
                }

                Customer customer = _repo.Customers.GetAll(c => (c.Login == userInfo.LoginModel.Name) && (c.Passoword == userInfo.LoginModel.Password)).ToList().First();
                var card = customer.AccessCards.First();
                var codes = _repo.AccessCodes.GetAll(c => c.AccessCardID == card.AccessCardID).ToList();
                var code = codes.Where(c => c.Number == secretCode.CodeIndex).First();
                var codeCount = _repo.AccessCodes.GetAll(c => c.AccessCardID == card.AccessCardID).Count();


                if (code.Code == secretCode.EnteredCode)
                {
                    WebSecurity.Login(userInfo.LoginModel.Name, userInfo.LoginModel.Password);
                    RedirectToAction("Index", "Home");
                }
                else
                {
                    CodeEnterFail(card);
                    if (card.IsBlocked == true)
                    {
                        return View("AccountBlocked");
                    }
                    ModelState.Remove("CodeIndex");
                    var secretCodeModel = new SecretCodeModel()
                    {
                        CodeIndex = (new Random()).Next(1, codeCount),
                        Remaining = GetEnterCodeRemaining(card)
                    };

                    ResetCookie(userInfo.LoginModel);
                    ModelState.AddModelError("", "Неправильный код. Попробуйте еще раз");
                    return View(secretCodeModel);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }


        }

        [HttpGet]
        [Authorize]
        public ActionResult Logout()
        {
            if (WebSecurity.IsAuthenticated)
            {
                WebSecurity.Logout();
            }
            return RedirectToAction("Index", "Login");
        }

        private void ResetCookie(LoginModel loginModel)
        {
            HttpRuntime.Cache.Remove(HttpContext.Request.UserHostAddress + HttpContext.Request.UserAgent);

            var cookie = new HttpCookie(".SECURECODE")
            {
                Expires = DateTime.Now.AddSeconds(_expiration_time_sec),
                Value = Tools.GetCookieValue(_expiration_time_sec)
            };

            var userInfo = new CookieUserInfo()
            {
                Cookie = cookie,
                LoginModel = loginModel
            };

            HttpRuntime.Cache.Add(HttpContext.Request.UserHostAddress + HttpContext.Request.UserAgent, userInfo, null, DateTime.Now.AddSeconds(_expiration_time_sec),
                Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
            HttpContext.Response.AppendCookie(cookie);
        }

        private void CodeEnterFail(AccessCard card)
        {
            card.EnteredCodeFail++;
            if (card.EnteredCodeFail >= 3)
            {
                card.IsBlocked = true;
                card.EnteredCodeFail = 0;
            }
            _repo.SaveChanges();
        }

        private int GetEnterCodeRemaining(AccessCard card)
        {
            return 3 - card.EnteredCodeFail;
        }

    }
}