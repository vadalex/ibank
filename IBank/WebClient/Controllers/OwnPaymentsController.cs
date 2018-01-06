using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebClient.Filters;
using WebMatrix.WebData;
using DAL;
using Entities;
using WebClient.BankService;
using WebClient.Models;
using WebClient.BusinessLogic;

namespace WebClient.Controllers
{
    [Authorize]
    [ExtendCookie]
    [SetViewData("Active", "top-menu-home")]
    public class OwnPaymentsController : Controller
    {
        private Repositories rep = new Repositories();

        public ActionResult Index()
        {
            OwnPaymentsModel model = new OwnPaymentsModel();
            Customer c = rep.Customers.GetSingle(WebSecurity.CurrentUserId);
            model.Arbitrary = c.OwnArbitraryPayments;
            model.Mobile = c.OwnMobilePayments;
            model.Transfer = c.OwnTransferPayments;
            model.SSIS = c.OwnSSISPayments;

            var customer = rep.Customers.GetSingle(WebSecurity.CurrentUserId);
            ViewBag.FirstName = customer.FirstName;
            ViewBag.MiddleName = customer.MiddleName;
            ViewBag.LastName = customer.LastName;
            return View(model);
        }

        public ActionResult OwnPayment(string type, int? paymentId)
        {
            if (type == null || paymentId == null)
                return RedirectToAction("Index");
            switch (type)
            {
                case "Arbitrary": return RedirectToAction("Arbitrary", "Payments", OwnPaymentsModule.ArbitraryOwnPaymentToModel((int)paymentId, rep));
                case "Mobile": return RedirectToAction("Mobile", "Payments", OwnPaymentsModule.MobileOwnPaymentToModel((int)paymentId, rep));
                case "Transfer": return RedirectToAction("Transfer", "Payments", OwnPaymentsModule.TransferOwnPaymentToModel((int)paymentId, rep));
                case "SSIS": return RedirectToAction("SSIS", "Payments", OwnPaymentsModule.SSISOwnPaymentToModel((int)paymentId, rep));
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult ChangePaymentName(string paymentId, string name)
        {
            string type = paymentId.Split('/')[0];
            int ownPyamentId = int.Parse(paymentId.Split('/')[1]);

            switch (type)
            {
                case "Arbitrary":
                    {
                        var p = rep.OwnArbitraryPayments.GetSingle(ownPyamentId);
                        p.Name = name;
                        rep.OwnArbitraryPayments.Update(p);
                        rep.SaveChanges();
                        break;
                    }
                case "Mobile":
                    {
                        var p = rep.OwnMobilePayments.GetSingle(ownPyamentId);
                        p.Name = name;
                        rep.OwnMobilePayments.Update(p);
                        rep.SaveChanges();
                        break;
                    }
                case "Transfer":
                    {
                        var p = rep.OwnTransferPayments.GetSingle(ownPyamentId);
                        p.Name = name;
                        rep.OwnTransferPayments.Update(p);
                        rep.SaveChanges();
                        break;
                    }
                case "SSIS":
                    {
                        var p = rep.OwnSSISPayments.GetSingle(ownPyamentId);
                        p.Name = name;
                        rep.OwnSSISPayments.Update(p);
                        rep.SaveChanges();
                        break;
                    } 
            }
                        
            ServerResponse response = new ServerResponse()
            {
                Success = true,
                Info = paymentId,
                Message = name
            };
            return Json(response);
        }

        [HttpPost]
        public JsonResult DeleteOwnPayment(string paymentId)
        {
            string type = paymentId.Split('/')[0];
            int ownPyamentId = int.Parse(paymentId.Split('/')[1]);

            switch (type)
            {
                case "Arbitrary":
                    {
                        var p = rep.OwnArbitraryPayments.GetSingle(ownPyamentId);                        
                        rep.OwnArbitraryPayments.Remove(p);
                        rep.SaveChanges();
                        break;
                    }
                case "Mobile":
                    {
                        var p = rep.OwnMobilePayments.GetSingle(ownPyamentId);
                        rep.OwnMobilePayments.Remove(p);
                        rep.SaveChanges();
                        break;
                    }
                case "Transfer":
                    {
                        var p = rep.OwnTransferPayments.GetSingle(ownPyamentId);
                        rep.OwnTransferPayments.Remove(p);
                        rep.SaveChanges();
                        break;
                    }
                case "SSIS":
                    {
                        var p = rep.OwnSSISPayments.GetSingle(ownPyamentId);
                        rep.OwnSSISPayments.Remove(p);
                        rep.SaveChanges();
                        break;
                    }
            }

            ServerResponse response = new ServerResponse()
            {
                Success = true,
                Info = paymentId,
                Message = "Собственный платеж удален"
            };
            return Json(response);
        }
    }
}