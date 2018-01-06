using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using DAL;
using Entities;
using WebClient.BankService;
using WebClient.BusinessLogic;
using WebClient.Filters;
using WebClient.Models;
using WebMatrix.WebData;

namespace WebClient.Controllers
{
    [Authorize]
    [ExtendCookie]
    [SetViewData("Active", "top-menu-home")]
    public class MobileAutoPaymentController : Controller
    {
        private Repositories repo = new Repositories();
        private BankServiceClient service = new BankServiceClient();

        public ActionResult Index()
        {
            var mobileAutoPayments = repo.MobileAutoPayments.GetAll(m => m.CustomerID == WebSecurity.CurrentUserId).ToList();
            var model = mobileAutoPayments.Select(m => new MobileAutoPayModel()
            {
                MobileAutoPayID = m.MobileAutoPayID,
                Amount = m.Amount,
                IntervalHours = m.Interval.Hours,
                IntervalMinutes = m.Interval.Minutes,
                IntervalDays = m.Interval.Days,
                MobileNumber = m.MobileNumber,
                CardNumber = CardAccountModule.ConvertCardNumberString(service.GetCardAccountById(m.PayCardId).CardNumber),
                Operator = m.MobileOperator,
                StartDate = m.StartDate,
                StartHours = m.StartDate.Hour,
                StartMinutes = m.StartDate.Minute,
                LastPayDate = m.LastExecutionDate
            }).ToList();

            var customer = repo.Customers.GetSingle(WebSecurity.CurrentUserId);
            ViewBag.FirstName = customer.FirstName;
            ViewBag.MiddleName = customer.MiddleName;
            ViewBag.LastName = customer.LastName;
            return View(model);
        }

        public ActionResult Create(MobileAutoPayModel model)
        {
            var customer = repo.Customers.GetSingle(WebSecurity.CurrentUserId);
            ViewBag.FirstName = customer.FirstName;
            ViewBag.MiddleName = customer.MiddleName;
            ViewBag.LastName = customer.LastName;

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(MobileAutoPayModel model, FormCollection collection)
        {
            var custome = repo.Customers.GetSingle(WebSecurity.CurrentUserId);
            ViewBag.FirstName = custome.FirstName;
            ViewBag.MiddleName = custome.MiddleName;
            ViewBag.LastName = custome.LastName;

            string language =  "ru";
            string culture =  "RU";

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", language, culture));
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(string.Format("{0}-{1}", language, culture));

            DateTime date =
                DateTime.ParseExact(collection["StartDate"], "dd.MM.yyyy", CultureInfo.CurrentCulture);

            model.StartDate = date;
            ModelState.Clear();

            int cardId;
            if (!int.TryParse(collection["item.CardAccount.CardAccountID"], out cardId))
            {
                ModelState.Clear();
                ModelState.AddModelError("CustomError", "Карта не найдена");
                return View(model);
            }
            else if (ModelState.IsValid)
            {
                if ((model.IntervalDays <= 0) && (model.IntervalHours <= 0) && (model.IntervalMinutes <= 0))
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Интервал должен быть положительным");
                    return View(model);
                }
                Customer customer = repo.Customers.GetSingle(WebSecurity.CurrentUserId);
                if (customer.IsLocked)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Ваш аккаунт заблокирован");
                    return View(model);
                }
                model.CardAccountID = cardId;
                CardAccount c = service.GetCardAccountById(cardId);
                if (c == null)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Карта не найдена");
                    return View(model);
                }
                model.CardNumber = CardAccountModule.ConvertCardNumberString(c.CardNumber);
                if (c.IsLocked)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Ваша платежная карта заблокирована");
                    return View(model);
                }
                if (c.ExpiredDate < Time.GetTime())
                {
                    c.Status = "Истек срок действия";
                    service.UpdateCardAccount(c);
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Истек срок действия карты");
                    return View(model);
                }
                
                var bankAccount = service.GetBankAccountById(c.BankAccountID);
                if (bankAccount == null)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Ошибка базы");
                    return View(model);
                }
                if (bankAccount.CurrencyID != 1)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Оплату мобильной связи можно проводить только беларусскими рублями");
                    return View(model);
                }
                if (model.Amount <= 0)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Сумма должна быть больше нуля");
                    return View(model);
                }
                if (!service.IsEnoughBalance(cardId, model.Amount))
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Недостаточно средств на счете");
                    return View(model);
                }
                model.StartDate = model.StartDate.AddMinutes(model.StartMinutes);
                model.StartDate = model.StartDate.AddHours(model.StartHours);
                return RedirectToAction("ConfirmAutoPay", model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult ConfirmAutoPay(MobileAutoPayModel model)
        {
            var custome = repo.Customers.GetSingle(WebSecurity.CurrentUserId);
            ViewBag.FirstName = custome.FirstName;
            ViewBag.MiddleName = custome.MiddleName;
            ViewBag.LastName = custome.LastName;

            if (model.CardAccountID == 0)
            {
                return RedirectToAction("Index");
            }
            return View("Confirm", model);
        }

        [HttpPost]
        public ActionResult ConfirmAutoPay(MobileAutoPayModel paymentInfo, FormCollection collection)
        {
            var custome = repo.Customers.GetSingle(WebSecurity.CurrentUserId);
            ViewBag.FirstName = custome.FirstName;
            ViewBag.MiddleName = custome.MiddleName;
            ViewBag.LastName = custome.LastName;

            if (paymentInfo.CardAccountID == 0)
            {
                return RedirectToAction("Index");
            }
            MobileAutoPay mt = new MobileAutoPay();
            mt.Amount = paymentInfo.Amount;
            mt.PayCardId = paymentInfo.CardAccountID;
            mt.CustomerID = WebSecurity.CurrentUserId;
            DateTime date = 
    DateTime.ParseExact(collection["StartDate"], "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            mt.StartDate = date;
            mt.LastExecutionDate = new DateTime(2000, 1,1,1,1,1);
            mt.MobileOperator = paymentInfo.Operator;
            mt.Interval = new TimeSpan(paymentInfo.IntervalDays, paymentInfo.IntervalHours, paymentInfo.IntervalMinutes, 0);
            mt.MobileNumber = paymentInfo.MobileNumber;
            try
            {
                repo.MobileAutoPayments.Add(mt);
                repo.SaveChanges();
                return View("Mesage", (object) "Автооплата успешно создана");
            }
            catch (Exception ex)
            {
                return View("Mesage", (object)"Что то пошло не так. Попробуйте еще раз" + ex.Message);
            }
        }

        [HttpPost]
        public JsonResult Delete(string Id)
        {

            var custome = repo.Customers.GetSingle(WebSecurity.CurrentUserId);
            ViewBag.FirstName = custome.FirstName;
            ViewBag.MiddleName = custome.MiddleName;
            ViewBag.LastName = custome.LastName;

            if (Id != null)
            {
                try
                {
                    MobileAutoPay autoPay = repo.MobileAutoPayments.GetSingle(Int32.Parse(Id));
                    if (autoPay == null) throw new Exception();
                    repo.MobileAutoPayments.Remove(autoPay);
                    repo.SaveChanges();

                    ServerResponse response = new ServerResponse()
                    {
                        Success = true,
                        Info = Id,
                        Message = "Автооплата удалена"
                    };
                    return Json(response);
                }
                catch (Exception ex)
                {
                    ServerResponse response = new ServerResponse()
                    {
                        Success = false,
                        Info = Id,
                        Message = "Ошибка удаления автооплаты"
                    };
                    return Json(response);
                }
            }
            else
            {
                ServerResponse response = new ServerResponse()
                {
                    Success = false,
                    Info = Id,
                    Message = "Не передан айди автооплаты"
                };
                return Json(response);
            }
        }

        public ActionResult Details(string Id)
        {
            var custome = repo.Customers.GetSingle(WebSecurity.CurrentUserId);
            ViewBag.FirstName = custome.FirstName;
            ViewBag.MiddleName = custome.MiddleName;
            ViewBag.LastName = custome.LastName;

            if (Id == null)
            {
                return View("Index");
            }
            var autoPay = repo.MobileAutoPayments.GetSingle(Int32.Parse(Id));
            if (autoPay == null)
            {
                return View("Index");
            }

            CardAccount c = service.GetCardAccountById(autoPay.PayCardId);
            MobileAutoPayModel model = new MobileAutoPayModel()
            {
                CardNumber = CardAccountModule.ConvertCardNumberString(c.CardNumber),
                Amount = autoPay.Amount,
                IntervalDays = autoPay.Interval.Days,
                IntervalHours = autoPay.Interval.Hours,
                CardAccountID = autoPay.PayCardId,
                IntervalMinutes = autoPay.Interval.Minutes,
                LastPayDate = autoPay.LastExecutionDate,
                StartDate = autoPay.StartDate,
                MobileNumber = autoPay.MobileNumber,
                Operator = autoPay.MobileOperator
            };
            return View(model);
        }
    }
}