using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebClient.Filters;
using WebMatrix.WebData;
using DAL;
using WebClient.Models;
using Entities;
using WebClient.BankService;

namespace WebClient.Controllers
{
    [Authorize]
    [ExtendCookie]
    [SetViewData("Active", "top-menu-home")]
    public class HomeController : Controller
    {
        private Repositories _repo = new Repositories();
        private BankServiceClient service = new BankServiceClient();

        public ActionResult Index()
        {
            var customer = _repo.Customers.GetSingle(WebSecurity.CurrentUserId);
            ViewBag.FirstName = customer.FirstName;
            ViewBag.MiddleName = customer.MiddleName;
            ViewBag.LastName = customer.LastName;
            return View();
        }

        [ExtendCookie]
        [HttpGet]
        public ActionResult PasswordReset()
        {
            var customer = _repo.Customers.GetSingle(WebSecurity.CurrentUserId);
            ViewBag.FirstName = customer.FirstName;
            ViewBag.MiddleName = customer.MiddleName;
            ViewBag.LastName = customer.LastName;

            return View(new PasswordResetModel());
        }

        [HttpPost]
        public ActionResult PasswordReset(PasswordResetModel resetPassModel)
        {
            var customer = _repo.Customers.GetSingle(WebSecurity.CurrentUserId);
            ViewBag.FirstName = customer.FirstName;
            ViewBag.MiddleName = customer.MiddleName;
            ViewBag.LastName = customer.LastName;

            if ((resetPassModel == null) || (resetPassModel.OldPassword == "") || (resetPassModel.NewPassword == "") || (resetPassModel.ConfirmPassword == ""))
            {
                return View(new PasswordResetModel());
            } else if(!ModelState.IsValid){
                return View(resetPassModel);
            } else if(customer.Passoword != resetPassModel.OldPassword) {
                
                ModelState.AddModelError("", "Неправильный пароль(");
                return View(resetPassModel);
            } else if(resetPassModel.NewPassword != resetPassModel.ConfirmPassword) {
                ModelState.AddModelError("", "Не совпадают новый и подтвержденный пароль!");
                return View(resetPassModel);
            } else {
                customer.Passoword = resetPassModel.NewPassword;
                _repo.SaveChanges();
                return View("PasswordResetted");
            }

            return View(resetPassModel);
        }

        public ActionResult PersonalInfo()
        {
            Customer customer = _repo.Customers.GetSingle(WebSecurity.CurrentUserId);
            ViewBag.FirstName = customer.FirstName;
            ViewBag.MiddleName = customer.MiddleName;
            ViewBag.LastName = customer.LastName;

            return View(customer);
        }

        public PartialViewResult CurrencyPartial()
        {
            List<Currency> currensies = service.GetAllCurrency().Where(c => c.CurrencyID != 1).ToList();
            CurrencyModel model = new CurrencyModel();

            foreach (var currency in currensies)
            {
                var item = new CurrencyItem()
                {
                    Name = currency.Name,
                    BankBuys = Math.Round(currency.Rate * 1.003) + "р",
                    BankCells = Math.Round(currency.Rate) + "р"
                };

                model.Rates.Add(item);
            }

            foreach (var currency in currensies)
            {
                foreach (var crossCurrency in currensies)
                {
                    if (currency.CurrencyID != crossCurrency.CurrencyID)
                    {
                        model.CrossRates.Add(currency.ShortName + "/" + crossCurrency.ShortName, Math.Round(currency.Rate/crossCurrency.Rate, 3).ToString());
                    }
                }
            }

            return PartialView("CurrencyPartial", model);
        }
    }
}