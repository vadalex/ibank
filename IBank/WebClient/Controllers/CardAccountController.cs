using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using WebClient.BankService;
using DAL;
using WebClient.Filters;
using WebClient.Models;
using WebClient.BusinessLogic;

namespace WebClient.Controllers
{
    [Authorize]
    [ExtendCookie]
    [SetViewData("Active", "top-menu-cards")]
    public class CardAccountController : Controller
    {
        private Repositories rep = new Repositories();
        private BankServiceClient service = new BankServiceClient();

        public ActionResult Index()
        {
            return View("CardAccounts");
        }

        [HttpPost]
        public JsonResult BlockCard(string cardId)
        {
            CardAccount card = service.GetCardAccountById(Int32.Parse(cardId));
            card.IsLocked = true;
            card.Status = "Заблокирована";
            service.UpdateCardAccount(card);

            ServerResponse response = new ServerResponse()
            {
                Success = true,
                Info = cardId,
                Message = card.Status
            };
            return Json(response);
        }

        [HttpPost]
        public JsonResult UnBlockCard(string cardId)
        {
            CardAccount card = service.GetCardAccountById(Int32.Parse(cardId));
            if (card.IsLocked == true)
            {
                card.IsLocked = false;
                card.Status = "Действительна";
            }
            if (card.ExpiredDate < Time.GetTime())
            {
                card.Status = "Истек срок действия";
            }

            service.UpdateCardAccount(card);
            ServerResponse response = new ServerResponse()
            {
                Success = true,
                Info = cardId,
                Message = card.Status
            };
            return Json(response);
        }

        [HttpPost]
        public JsonResult ChangeName(string cardId, string cardName)
        {
            CardAccount card = service.GetCardAccountById(Int32.Parse(cardId));
            card.Name = cardName;
            service.UpdateCardAccount(card);
            ServerResponse response = new ServerResponse()
            {
                Success = true,
                Info = cardId,
                Message = cardName
            };
            return Json(response);
        }

        public PartialViewResult CardAccountsPartial(string cardNumber, string cardId)
        {
            var cardAccounts = service.GetCardAccountsByCustomerId(WebSecurity.CurrentUserId);
            foreach(var c in cardAccounts)
            {
                if (c.ExpiredDate <= Time.GetTime() && c.Status != "Истек срок действия" && !c.IsLocked)
                {
                    c.Status = "Истек срок действия";
                }  
            }
            if (cardAccounts.Any())
            {
                IEnumerable<CardAccountModel> cardAcountModels = cardAccounts
                    .Select(c => new CardAccountModel()
                    {
                        CardAccount = c,
                        Currency = service.GetBankAccountCurrencyShortString(c.BankAccountID),
                        Balance = service.GetBankAccountBalance(c.BankAccountID),
                        CardNumber = CardAccountModule.ConvertCardNumberString(c.CardNumber)
                    });

                var cardExists =
                    cardAccounts.Any(c => (c.CardNumber == cardNumber) || (c.CardAccountID.ToString() == cardId));
                var selectCardId = cardExists
                    ? cardAccounts.First(c => (c.CardNumber == cardNumber) || (c.CardAccountID.ToString() == cardId))
                        .CardAccountID.ToString()
                    : cardAccounts.First().CardAccountID.ToString();

                CardAccountsModel model = new CardAccountsModel()
                {
                    CardAccounts = cardAcountModels,
                    SelectCardAccountId = selectCardId
                };

                return PartialView("CardAccountsPartial", model);
            }
            else
            {
                CardAccountsModel model = new CardAccountsModel()
                {
                    CardAccounts = new List<CardAccountModel>(),
                    SelectCardAccountId = "0"
                };

                return PartialView("CardAccountsPartial", model);
            }

            
            
        }

        public PartialViewResult CardAccountsSelectPartial()
        {
            var cardAccounts = service.GetCardAccountsByCustomerId(WebSecurity.CurrentUserId);

            if (cardAccounts.Any())
            {
                IEnumerable<CardAccountModel> cardAcountModels = cardAccounts
                    .Select(c => new CardAccountModel()
                    {
                        CardAccount = c,
                        Currency = service.GetBankAccountCurrencyShortString(c.BankAccountID),
                        Balance = service.GetBankAccountBalance(c.BankAccountID),
                        CardNumber = CardAccountModule.ConvertCardNumberString(c.CardNumber)
                    });

                return PartialView("CardAccountsSelectPartial", cardAcountModels);
            }
            else
            {
                return PartialView("CardAccountsSelectPartial", new List<CardAccountModel>());
            }
            
        }

    }
}
