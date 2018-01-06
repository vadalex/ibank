using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DAL;
using WebClient.BankService;
using WebClient.BusinessLogic;
using WebClient.Filters;
using WebClient.Models;

namespace WebClient.Controllers
{
    [Authorize]
    [ExtendCookie]
    [SetViewData("Active", "top-menu-history")]
    public class HistoryController : Controller
    {
        private Repositories rep = new Repositories();
        private BankServiceClient service = new BankServiceClient();

        // GET: History
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            List<HistoryItemModel> HistoryItems = new List<HistoryItemModel>();
            Dictionary<int, CardAccount> CardAccounts = new Dictionary<int, CardAccount>();
            RouteValueDictionary RouteValues = new RouteValueDictionary();

            #region parsing

            List<string> cardIds = new List<string>();
            if (collection["item.CardAccount.CardAccountID"] == null)
            {
                return View("Index", (object)"Не найдена информация о платежных картах");
            }
            cardIds = collection["item.CardAccount.CardAccountID"].Split(',').Where(id => id != "false").ToList();

            if (cardIds.Any() == false)
            {
                return View("Index", (object)"Не выбрана ни одна из карт");
            }
            RouteValues.Add("item.CardAccount.CardAccountID", collection["item.CardAccount.CardAccountID"]);


            var dateFrom = collection["datepicker-from"];
            var dateTo = collection["datepicker-to"];
            DateTime DateFrom;
            DateTime DateTo;
            try
            {
                DateFrom = String.IsNullOrEmpty(dateFrom) ? DateTime.MinValue : DateTime.Parse(dateFrom);
                DateTo = String.IsNullOrEmpty(dateTo) ? DateTime.MaxValue : DateTime.Parse(dateTo);
            }
            catch (Exception ex)
            {
                return View("Index", (object)ex.Message);
            }
            RouteValues.Add("datepicker-from", collection["datepicker-from"]);
            RouteValues.Add("datepicker-to", collection["datepicker-to"]);

            var arbitraryPaymentsParam = collection["arbitrary_payments"];
            var ssispPaymentsParam = collection["ssisp_payments"];
            var mobilePaymentsParam = collection["mobile_payments"];
            var transferPaymentsParam = collection["transfer_payments"];
            if ((arbitraryPaymentsParam == null) && (ssispPaymentsParam == null) && (mobilePaymentsParam == null) &&
                (transferPaymentsParam == null))
            {
                return View("Index", (object)"Ни один из типов платежей не выбран");
            }

            RouteValues.Add("arbitrary_payments", collection["arbitrary_payments"]);
            RouteValues.Add("ssisp_payments", collection["ssisp_payments"]);
            RouteValues.Add("mobile_payments", collection["mobile_payments"]);
            RouteValues.Add("transfer_payments", collection["transfer_payments"]);
            #endregion parsing

            #region fill

            foreach (var cardId in cardIds)
            {
                var id = Int32.Parse(cardId);
                CardAccounts.Add(id, service.GetCardAccountById(id));
            }

            if (arbitraryPaymentsParam != null)
            {
                foreach (var cardId in cardIds)
                {
                    var payments = service.GetArbitraryTransactionsByCardId(Int32.Parse(cardId), DateFrom, DateTo);
                    if (payments != null && payments.Any())
                    {
                        foreach (var payment in payments)
                        {
                            HistoryItems.Add(new HistoryItemModel()
                            {
                                Amount = payment.Amount,
                                CardNumber = CardAccountModule.ConvertCardNumberString(CardAccounts[payment.CardAccountID].CardNumber),
                                Currency = service.GetBankAccountCurrencyShortString(CardAccounts[payment.CardAccountID].BankAccountID),
                                Date = payment.Date,
                                Recipient = payment.Recipient,
                                TransactionId = payment.ArbitraryTransactionID,
                                Type = payment.Type
                            });
                        }
                    }
                }
            }
            if (ssispPaymentsParam != null)
            {
                foreach (var cardId in cardIds)
                {
                    var payments = service.GetSSISTransactionsByCardId(Int32.Parse(cardId), DateFrom, DateTo);
                    if (payments != null && payments.Any())
                    {
                        foreach (var payment in payments)
                        {
                            HistoryItems.Add(new HistoryItemModel()
                            {
                                Amount = payment.Amount,
                                CardNumber = CardAccountModule.ConvertCardNumberString(CardAccounts[payment.CardAccountID].CardNumber),
                                Currency = service.GetBankAccountCurrencyShortString(CardAccounts[payment.CardAccountID].BankAccountID),
                                Date = payment.Date,
                                Recipient = rep.SSISPayments.GetSingle(payment.SSISPaymentID).Name,
                                TransactionId = payment.SSISTransactionID,
                                Type = payment.Type
                            });
                        }
                    }
                }
            }
            if (mobilePaymentsParam != null)
            {
                foreach (var cardId in cardIds)
                {
                    var payments = service.GetMobileTransactionsByCardId(Int32.Parse(cardId), DateFrom, DateTo);
                    if (payments != null && payments.Any())
                    {
                        foreach (var payment in payments)
                        {
                            HistoryItems.Add(new HistoryItemModel()
                            {
                                Amount = payment.Amount,
                                CardNumber = CardAccountModule.ConvertCardNumberString(CardAccounts[payment.CardAccountID].CardNumber),
                                Currency = service.GetBankAccountCurrencyShortString(CardAccounts[payment.CardAccountID].BankAccountID),
                                Date = payment.Date,
                                Recipient = payment.MobileProvider,
                                TransactionId = payment.MobileTransactionID,
                                Type = payment.Type
                            });
                        }
                    }
                }
            }
            if (transferPaymentsParam != null)
            {
                foreach (var cardId in cardIds)
                {
                    var payments = service.GetTransferTransactionsByCardId(Int32.Parse(cardId), DateFrom, DateTo);
                    if (payments != null && payments.Any())
                    {
                        foreach (var payment in payments)
                        {
                            HistoryItems.Add(new HistoryItemModel()
                            {
                                Amount = payment.Amount,
                                CardNumber = CardAccountModule.ConvertCardNumberString(CardAccounts[payment.CardAccountID].CardNumber),
                                Currency = service.GetBankAccountCurrencyShortString(CardAccounts[payment.CardAccountID].BankAccountID),
                                Date = payment.Date,
                                Recipient = payment.Number,
                                TransactionId = payment.TransferTransactionID,
                                Type = payment.Type
                            });
                        }
                    }
                }
            }
            #endregion fill

            HistoryListModel model = new HistoryListModel()
            {
                BackRouteValues = RouteValues,
                Items = HistoryItems.OrderBy(x => x.Date).ToList()
            };

            return View("HistoryList", model);
        }

        [HttpPost]
        public ActionResult HistoryList(FormCollection collection)
        {
            var id = collection["id"];
            var type = collection["type"];

            if (id == null || type == null)
            {
                return RedirectToAction("Index");
            }

            RouteValueDictionary RouteValues = new RouteValueDictionary();
            RouteValues.Add("item.CardAccount.CardAccountID", collection["item.CardAccount.CardAccountID"]);
            RouteValues.Add("datepicker-from", collection["datepicker-from"]);
            RouteValues.Add("datepicker-to", collection["datepicker-to"]);
            RouteValues.Add("arbitrary_payments", collection["arbitrary_payments"]);
            RouteValues.Add("ssisp_payments", collection["ssisp_payments"]);
            RouteValues.Add("mobile_payments", collection["mobile_payments"]);
            RouteValues.Add("transfer_payments", collection["transfer_payments"]);

            if (type == PaymentType.Mobile.ToString())
            {
                var mobileTransaction = service.GetMobileTransactionById(Int32.Parse(id));
                var cardAccout = service.GetCardAccountById(mobileTransaction.CardAccountID);
                var bankAccout = service.GetBankAccountById(cardAccout.BankAccountID);
                var currency = service.GetCurrencyById(bankAccout.CurrencyID);

                HistoryPaymentDetails info = new HistoryPaymentDetails();
                info.BackRouteValues = RouteValues;
                info.Dictionary = new Dictionary<string, string>();
                info.Dictionary.Add("Дата", mobileTransaction.Date.ToString());
                info.Dictionary.Add("Номер карты", CardAccountModule.ConvertCardNumberString(cardAccout.CardNumber));
                info.Dictionary.Add("Оператор", mobileTransaction.MobileProvider);
                info.Dictionary.Add("Номер телефона", mobileTransaction.Phone);
                info.Dictionary.Add("Сумма", mobileTransaction.Amount + " " + currency.ShortName);

                return View("Details", info);
            }

            if (type == PaymentType.Arbitrary.ToString())
            {
                var transaction = service.GetArbitraryTransactionById(Int32.Parse(id));
                var cardAccout = service.GetCardAccountById(transaction.CardAccountID);
                var bankAccout = service.GetBankAccountById(cardAccout.BankAccountID);
                var currency = service. GetCurrencyById(bankAccout.CurrencyID);

                HistoryPaymentDetails info = new HistoryPaymentDetails();
                info.BackRouteValues = RouteValues;
                info.Dictionary = new Dictionary<string, string>();
                info.Dictionary.Add("Дата", transaction.Date.ToString());
                info.Dictionary.Add("Номер карты", CardAccountModule.ConvertCardNumberString(cardAccout.CardNumber));
                info.Dictionary.Add("УНП", transaction.UNP);
                info.Dictionary.Add("Номер счета получателя", transaction.RecipientAccount);
                info.Dictionary.Add("Получатель", transaction.Recipient);
                info.Dictionary.Add("Цель", transaction.Target);
                info.Dictionary.Add("Сумма", transaction.Amount + " " + currency.ShortName);

                return View("Details", info);
            }

            if (type == PaymentType.Transfer.ToString())
            {
                var transaction = service.GetTransferTransactionById(Int32.Parse(id));
                var cardAccout = service.GetCardAccountById(transaction.CardAccountID);
                var bankAccout = service.GetBankAccountById(cardAccout.BankAccountID);
                var currency = service.GetCurrencyById(bankAccout.CurrencyID);

                HistoryPaymentDetails info = new HistoryPaymentDetails();
                info.BackRouteValues = RouteValues;
                info.Dictionary = new Dictionary<string, string>();
                info.Dictionary.Add("Дата", transaction.Date.ToString());
                info.Dictionary.Add("Номер карты", CardAccountModule.ConvertCardNumberString(cardAccout.CardNumber));
                string targetNumber = service.GetCardAccountById(transaction.TargetCardAccountID).CardNumber;
                info.Dictionary.Add("Номер карты получателя", CardAccountModule.ConvertCardNumberString(targetNumber));                
                info.Dictionary.Add("Сумма", transaction.Amount + " " + currency.ShortName);

                return View("Details", info);
            }

            if (type == PaymentType.SSIS.ToString())
            {
                var transaction = service.GetSSISTransactionById(Int32.Parse(id));
                var cardAccout = service.GetCardAccountById(transaction.CardAccountID);
                var bankAccout = service.GetBankAccountById(cardAccout.BankAccountID);
                var currency = service.GetCurrencyById(bankAccout.CurrencyID);
                var payment = rep.SSISPayments.GetSingle(transaction.SSISPaymentID);

                HistoryPaymentDetails info = new HistoryPaymentDetails();
                info.BackRouteValues = RouteValues;
                info.Dictionary = new Dictionary<string, string>();
                info.Dictionary.Add("Дата", transaction.Date.ToString());
                info.Dictionary.Add("Номер карты", CardAccountModule.ConvertCardNumberString(cardAccout.CardNumber));
                info.Dictionary.Add("Имя ЕРИП платежа", payment.Name);
                info.Dictionary.Add(payment.NumberLabel, transaction.Number);
                info.Dictionary.Add("Сумма", transaction.Amount + " " + currency.ShortName);

                return View("Details", info);
            }

            return View("Index");
        }
    }
}