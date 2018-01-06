using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient.BankService;
using WebClient.BusinessLogic;
using WebClient.Filters;
using WebClient.Models;
using WebMatrix.WebData;
using Entities;

namespace WebClient.Controllers
{
    [Authorize]
    [ExtendCookie]
    [SetViewData("Active", "top-menu-payments")]
    public class PaymentsController : Controller
    {
        private Repositories rep = new Repositories();
        private BankServiceClient service = new BankServiceClient();

        public ActionResult Index()
        {
            return View();
        }

        #region arbitrary
        [HttpGet]
        public ActionResult Arbitrary(ArbitraryPaymentModel paymentInfo)
        {
            return View(paymentInfo);
        }

        [HttpPost]
        public ActionResult Arbitrary(ArbitraryPaymentModel paymentInfo, FormCollection collection)
        {
            int cardId;
            if (!int.TryParse(collection["item.CardAccount.CardAccountID"], out cardId))
            {
                ModelState.Clear();
                ModelState.AddModelError("CustomError", "Карта не найдена");
                return View(paymentInfo);
            }
            else if (ModelState.IsValid)
            {
                Customer customer = rep.Customers.GetSingle(WebSecurity.CurrentUserId);
                if (customer.IsLocked)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Ваш аккаунт заблокирован");
                    return View(paymentInfo);                    
                }
                paymentInfo.AccountCardId = cardId;
                CardAccount c = service.GetCardAccountById(cardId);
                if (c == null)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Карта не найдена");
                    return View(paymentInfo);
                }
                paymentInfo.AccountCardNumber = CardAccountModule.ConvertCardNumberString(c.CardNumber);
                if (c.IsLocked)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Ваша платежная карта заблокирована");
                    return View(paymentInfo);
                }
                if (c.ExpiredDate < Time.GetTime())
                {
                    c.Status = "Истек срок действия";
                    service.UpdateCardAccount(c);
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Истек срок действия карты");
                    return View(paymentInfo);
                }                         
                var bankAccount = service.GetBankAccountById(c.BankAccountID);                
                if (bankAccount == null)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Ошибка базы");
                    return View(paymentInfo);
                }                
                if (paymentInfo.Amount <= 0)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Сумма должна быть больше нуля");
                    return View(paymentInfo);
                }
                if (!service.IsEnoughBalance(cardId, paymentInfo.Amount))
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Недостаточно средств на счете");
                    return View(paymentInfo);
                }
                
                return RedirectToAction("ConfirmArbitrary", paymentInfo);
            }
            else
            {
                return View(paymentInfo);
            }
        }

        [HttpGet]
        public ActionResult ConfirmArbitrary(ArbitraryPaymentModel paymentInfo)
        {
            if (paymentInfo.AccountCardId == 0)
                return RedirectToAction("Arbitrary");
            return View("ConfirmArbitrary", paymentInfo);
        }

        [HttpPost]
        public ActionResult ConfirmArbitrary(ArbitraryPaymentModel paymentInfo, FormCollection collcetion)
        {
            if (paymentInfo.AccountCardId == 0)
                return RedirectToAction("Arbitrary");
            if (paymentInfo.ToOwnPayments)
                OwnPaymentsModule.AddArbitraryOwnPayment(paymentInfo, rep, WebSecurity.CurrentUserId);
            ArbitraryTransaction tr = new ArbitraryTransaction();
            tr.Amount = paymentInfo.Amount;
            tr.BankCode = paymentInfo.BankName;
            tr.CardAccountID = paymentInfo.AccountCardId;
            tr.CustomerID = WebSecurity.CurrentUserId;
            tr.Recipient = paymentInfo.Recipient;
            tr.RecipientAccount = paymentInfo.RecipientAccount;
            tr.Target = paymentInfo.Target;
            tr.UNP = paymentInfo.UNP;
            bool success = service.CreateArbitraryTransaction(tr);
            if (success)
                return View("Message", (object)"Ваш платеж успешно завершен");                
            else
                return View("Message", (object)"Что то пошло не так. Попробуйте еще раз");                
        }
        #endregion arbitrary

        #region mobile
        [HttpGet]
        public ActionResult Mobile(string provider, MobilePaymentModel paymentInfo)
        {
            if (provider != null)
            {
                switch (provider)
                { 
                    case "MTS":
                        paymentInfo.Provider = "МТС";
                        break;
                    case "Velcom":
                        paymentInfo.Provider = "Velcom";
                        break;
                    case "Dialog":
                        paymentInfo.Provider = "Dialog";
                        break;
                    case "Life":
                        paymentInfo.Provider = "Life :)";
                        break;
                    default:
                        return View("Message", (object)"Неправильное имя оператора мобильной связи");
                }                              
            }                
            if(paymentInfo.Provider == null)
                return View("Message", (object)"Неправильное имя оператора мобильной связи");
            
            return View(paymentInfo);
        }

        [HttpPost]
        public ActionResult Mobile(MobilePaymentModel paymentInfo, FormCollection collection)
        {
            int cardId;
            if (!int.TryParse(collection["item.CardAccount.CardAccountID"], out cardId))
            {
                ModelState.Clear();
                ModelState.AddModelError("CustomError", "Карта не найдена");
                return View(paymentInfo);
            }
            else if (ModelState.IsValid)
            {
                Customer customer = rep.Customers.GetSingle(WebSecurity.CurrentUserId);
                if (customer.IsLocked)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Ваш аккаунт заблокирован");
                    return View(paymentInfo);
                }
                paymentInfo.CardAccountID = cardId;
                CardAccount c = service.GetCardAccountById(cardId);
                if (c == null)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Карта не найдена");
                    return View(paymentInfo);
                }
                paymentInfo.CardNumber = CardAccountModule.ConvertCardNumberString(c.CardNumber);
                if (c.IsLocked)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Ваша платежная карта заблокирована");
                    return View(paymentInfo);
                }
                if (c.ExpiredDate < Time.GetTime())
                {
                    c.Status = "Истек срок действия";
                    service.UpdateCardAccount(c);
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Истек срок действия карты");
                    return View(paymentInfo);
                }
                var bankAccount = service.GetBankAccountById(c.BankAccountID);
                if (bankAccount == null)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Ошибка базы");
                    return View(paymentInfo);
                }                
                if (bankAccount.CurrencyID != 1)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Оплату мобильной связи можно проводить только беларусскими рублями");
                    return View(paymentInfo);
                }
                if (paymentInfo.Amount <= 0)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Сумма должна быть больше нуля");
                    return View(paymentInfo);
                }
                if (!service.IsEnoughBalance(cardId, paymentInfo.Amount))
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Недостаточно средств на счете");
                    return View(paymentInfo);
                }
                return RedirectToAction("ConfirmMobile", paymentInfo);
            }
            else
            {
                return View(paymentInfo);
            }
        }

        [HttpGet]
        public ActionResult ConfirmMobile(MobilePaymentModel paymentInfo)
        {
            if (paymentInfo.CardAccountID == 0)
            {                
                return RedirectToAction("Index");
            }
            return View("ConfirmMobile", paymentInfo);
        }

        [HttpPost]
        public ActionResult ConfirmMobile(MobilePaymentModel paymentInfo, FormCollection collcetion)
        {
            if (paymentInfo.CardAccountID == 0)
            {
                return RedirectToAction("Index");
            }
            if (paymentInfo.ToOwnPayments)
                OwnPaymentsModule.AddMobileOwnPayment(paymentInfo, rep, WebSecurity.CurrentUserId);
            MobileTransaction mt = new MobileTransaction();
            mt.Amount = paymentInfo.Amount;
            mt.CardAccountID = paymentInfo.CardAccountID;
            mt.CustomerID = WebSecurity.CurrentUserId;
            mt.Date = Time.GetTime();
            mt.MobileProvider = paymentInfo.Provider;
            mt.Phone = paymentInfo.Phone;
            mt.Type = PaymentType.Mobile;
            bool success = service.CreateMobileTransaction(mt);
            if (success)
                return View("Message", (object)"Платеж успешно завершен");
            else
                return View("Message", (object)"Что то пошло не так. Попробуйте еще раз");
        }
        #endregion mobile

        #region transfer
        [HttpGet]
        public ActionResult Transfer(TransferModel paymentInfo)
        {
            return View(paymentInfo);
        }

        [HttpPost]
        public ActionResult Transfer(TransferModel paymentInfo, FormCollection collection)
        {
            int cardId;
            if (!int.TryParse(collection["item.CardAccount.CardAccountID"], out cardId))
            {
                ModelState.Clear();
                ModelState.AddModelError("CustomError", "Карта не найдена");
                return View(paymentInfo);
            }
            else if (ModelState.IsValid)
            {
                Customer customer = rep.Customers.GetSingle(WebSecurity.CurrentUserId);
                if (customer.IsLocked)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Ваш аккаунт заблокирован");
                    return View(paymentInfo);
                }
                paymentInfo.CardId = cardId;
                CardAccount c = service.GetCardAccountById(cardId);
                if (c == null)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Карта не найдена");
                    return View(paymentInfo);
                }
                paymentInfo.CardNumber = CardAccountModule.ConvertCardNumberString(c.CardNumber);
                if (c.IsLocked)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Ваша платежная карта заблокирована");
                    return View(paymentInfo);
                }
                if (c.ExpiredDate < Time.GetTime())
                {
                    c.Status = "Истек срок действия";
                    service.UpdateCardAccount(c);
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Истек срок действия карты");
                    return View(paymentInfo);
                }
                var targetCard = service.GetCardAccountByNumber(paymentInfo.TargetCardAccountNumber);
                if (targetCard == null)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Не найдена карта с таким номером");
                    return View(paymentInfo);
                }
                var bankAccount = service.GetBankAccountById(c.BankAccountID);
                var targetBankAccount = service.GetBankAccountById(targetCard.BankAccountID);
                if (targetBankAccount == null || bankAccount == null)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Ошибка базы");
                    return View(paymentInfo);
                }
                paymentInfo.TargetCardAccountId = targetCard.CardAccountID;
                if (targetBankAccount.BankAccountID == bankAccount.BankAccountID)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Нельзя переводить на этот же счет");
                    return View(paymentInfo);
                }
                var currency = service.GetCurrencyById(bankAccount.CurrencyID);
                var targetCurrency = service.GetCurrencyById(targetBankAccount.CurrencyID);
                if (currency.CurrencyID != targetCurrency.CurrencyID)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Разные валюты");
                    return View(paymentInfo);
                }
                if (paymentInfo.Amount <= 0)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Сумма должна быть больше нуля");
                    return View(paymentInfo);
                }
                if (!service.IsEnoughBalance(cardId, paymentInfo.Amount))
                {
                    ModelState.Clear();
                    ModelState.AddModelError("CustomError", "Недостаточно средств на счете");
                    return View(paymentInfo);
                }
                return RedirectToAction("ConfirmTransfer", paymentInfo);
            }
            else
            {
                return View(paymentInfo);
            }
        }

        [HttpGet]
        public ActionResult ConfirmTransfer(TransferModel paymentInfo)
        {
            if (paymentInfo.CardId == 0)
                return RedirectToAction("Transfer");
            return View("ConfirmTransfer", paymentInfo);
        }

        [HttpPost]
        public ActionResult ConfirmTransfer(TransferModel paymentInfo, FormCollection collcetion)
        {
            if (paymentInfo.CardId == 0)
                return RedirectToAction("Transfer");
            if (paymentInfo.ToOwnPayments)
                OwnPaymentsModule.AddTransferOwnPayment(paymentInfo, rep, WebSecurity.CurrentUserId);
            TransferTransaction tt = new TransferTransaction();
            tt.Amount = paymentInfo.Amount;
            tt.CardAccountID = paymentInfo.CardId;
            tt.CustomerID = WebSecurity.CurrentUserId;
            tt.Date = Time.GetTime();
            tt.TargetCardAccountID = paymentInfo.TargetCardAccountId;
            tt.Number = paymentInfo.Message;
            tt.Type = PaymentType.Transfer;
            bool success = service.CreateTransferTransaction(tt);
            if (success)
                return View("Message", (object)"Вы перевели средства");
            else
                return View("Message", (object)"Что то пошло не так. Попробуйте еще раз");
        }
        #endregion transfer

        #region ssis

        public ActionResult SSISList(int? id)
        {
            if ((id == null) || (rep.SSISGroups.GetSingle((int)id) == null))
            {
                id = 1;
            }

            SSISListModel model = new SSISListModel();

            model.GroupsPath = new Stack<SSISGroup>();
            SSISGroup ssisGroup = rep.SSISGroups.GetSingle((int)id);
            while (ssisGroup != null) 
            {
                model.GroupsPath.Push(ssisGroup);
                ssisGroup = rep.SSISGroups.GetSingle(ssisGroup.ParentGroupID);
            }

            List<SSISListItemModel> list = new List<SSISListItemModel>();
            var groups = rep.SSISGroups.GetAll(group => group.ParentGroupID == id).ToList();
            var payments = rep.SSISPayments.GetAll(payment => payment.GroupID == id).ToList();

            if ((groups != null) && (groups.Any()))
            {
                foreach (var group in groups)
                {
                    list.Add(new SSISListItemModel()
                    {
                        Id = group.SSISGroupID,
                        Label = group.GroupName,
                        Type = SSISListItemType.Group
                    });
                }
            }
            if ((payments != null) && (payments.Any()))
            {
                foreach (var payment in payments)
                {
                    list.Add(new SSISListItemModel()
                    {
                        Id = payment.SSISPaymentID,
                        Label = payment.Name,
                        Type = SSISListItemType.Payment
                    });
                }
            }
            model.Items = list;

            return View(model);
        }

        [HttpGet]
        public ActionResult SSIS(int? id, SSISPaymentModel model)
        {
            if (id != null)
            {
                model.SSISPaymentID = (int)id;
            }
            if (model.SSISPaymentID == 0)
                return View("Message", (object)"Указан несуществующий ЕРИП платеж");

            var payment = rep.SSISPayments.GetSingle(model.SSISPaymentID);
            if (payment == null)
                return View("Message", (object)"Указан несуществующий ЕРИП платеж");

            model.Group = payment.Group;
            model.Information = payment.Information;
            model.Name = payment.Name;
            model.NumberLabel = payment.NumberLabel;

            return View(model);
        }

        [HttpPost]
        public ActionResult SSIS(SSISPaymentModel model, FormCollection collection)
        {
            int cardId;
            if (!int.TryParse(collection["item.CardAccount.CardAccountID"], out cardId))
            {
                ModelState.Clear();
                ModelState.AddModelError("CustomError", "Карта не найдена");
                return View(model);
            }
            else if (ModelState.IsValid)
            {
                Customer customer = rep.Customers.GetSingle(WebSecurity.CurrentUserId);
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
                    ModelState.AddModelError("CustomError", "Оплата в системе ЕРИП возможна только в беларусских рублях");
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
                return RedirectToAction("ConfirmSSIS", model);
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult ConfirmSSIS(SSISPaymentModel paymentInfo)
        {
            if (paymentInfo.CardAccountID == 0)
            {
                return RedirectToAction("Index");
            }
            return View("ConfirmSSIS", paymentInfo);
        }

        [HttpPost]
        public ActionResult ConfirmSSIS(SSISPaymentModel paymentInfo, FormCollection collcetion)
        {
            if (paymentInfo.CardAccountID == 0)
            {
                return RedirectToAction("Index");
            }



            if (paymentInfo.ToOwnPayments)
                OwnPaymentsModule.AddSSISyOwnPayment(paymentInfo, rep, WebSecurity.CurrentUserId);

            SSISTransaction t = new SSISTransaction();
            t.Amount = paymentInfo.Amount;
            t.CardAccountID = paymentInfo.CardAccountID;
            t.CustomerID = WebSecurity.CurrentUserId;
            t.Date = Time.GetTime();
            t.Number = paymentInfo.Number;
            t.SSISPaymentID = paymentInfo.SSISPaymentID;
            t.Type = PaymentType.SSIS;

            bool success = service.CreateSSISTransaction(t);
            if (success)
                return View("Message", (object)"Платеж успешно завершен");
            else
                return View("Message", (object)"Что то пошло не так. Попробуйте еще раз");
        }

        #endregion
    }
}