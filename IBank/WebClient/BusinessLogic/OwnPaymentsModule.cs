using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebClient.Models;
using DAL;
using Entities;

namespace WebClient.BusinessLogic
{
    public static class OwnPaymentsModule
    {
        public static void AddArbitraryOwnPayment(ArbitraryPaymentModel model, Repositories rep, int customerId)
        {
            OwnArbitraryPayment p = new OwnArbitraryPayment();
            p.BankCode = model.BankName;
            p.CardAccountID = model.AccountCardId;
            p.CustomerID = customerId;
            p.Recipient = model.Recipient;
            p.RecipientAccount = model.RecipientAccount;
            p.Target = model.Target;
            p.UNP = model.UNP;
            p.Name = "Произвольный платеж "  + model.RecipientAccount;
            rep.OwnArbitraryPayments.Add(p);
            rep.SaveChanges();            
        }

        public static void AddMobileOwnPayment(MobilePaymentModel model, Repositories rep, int customerId)
        {
            OwnMobilePayment p = new OwnMobilePayment();
            p.CardAccountID = model.CardAccountID;
            p.CustomerID = customerId;
            p.MobileProvider = model.Provider;
            p.Number = model.Phone;
            p.Name = model.Provider + "  " + model.Phone;
            rep.OwnMobilePayments.Add(p);
            rep.SaveChanges();
        }

        public static void AddTransferOwnPayment(TransferModel model, Repositories rep, int customerId)
        {
            OwnTransferPayment p = new OwnTransferPayment();
            p.CardAccountID = model.CardId;
            p.CustomerID = customerId;
            p.Message = model.Message;
            p.TargetCardNumber = model.TargetCardAccountNumber;
            p.Name = "Перевод средств на " + model.TargetCardAccountNumber;
            rep.OwnTransferPayments.Add(p);
            rep.SaveChanges();
        }
        public static void AddSSISyOwnPayment(SSISPaymentModel model, Repositories rep, int customerId)
        {
            OwnSSISPayment p = new OwnSSISPayment();
            p.CardAccountID = model.CardAccountID;
            p.CustomerID = customerId;
            p.SSISPaymentID = model.SSISPaymentID;
            p.Number = model.Number;
            p.Name = model.Name + " " + model.Number;
            rep.OwnSSISPayments.Add(p);
            rep.SaveChanges();            
        }
        
        public static ArbitraryPaymentModel ArbitraryOwnPaymentToModel(int paymentId, Repositories rep)
        {
            OwnArbitraryPayment p = rep.OwnArbitraryPayments.GetSingle(paymentId);
            if (p == null)
                return null;
            ArbitraryPaymentModel model = new ArbitraryPaymentModel();
            model.AccountCardId = p.CardAccountID;
            model.BankName = p.BankCode;
            model.Recipient = p.Recipient;
            model.RecipientAccount = p.RecipientAccount;
            model.Target = p.Target;
            model.UNP = p.UNP;
            model.FromOwnPayments = true;
            return model;
        }

        public static MobilePaymentModel MobileOwnPaymentToModel(int paymentId, Repositories rep)
        {
            OwnMobilePayment p = rep.OwnMobilePayments.GetSingle(paymentId);
            if (p == null)
                return null;
            MobilePaymentModel model = new MobilePaymentModel();
            model.CardAccountID = p.CardAccountID;
            model.Phone = p.Number;
            model.Provider = p.MobileProvider;
            model.FromOwnPayments = true;
            return model;
        }

        public static TransferModel TransferOwnPaymentToModel(int paymentId, Repositories rep)
        {
            OwnTransferPayment p = rep.OwnTransferPayments.GetSingle(paymentId);
            if (p == null)
                return null;
            TransferModel model = new TransferModel();
            model.CardId = p.CardAccountID;
            model.Message = p.Message;
            model.TargetCardAccountNumber = p.TargetCardNumber;
            model.FromOwnPayments = true;
            return model;
        }

        public static SSISPaymentModel SSISOwnPaymentToModel(int paymentId, Repositories rep)
        {
            OwnSSISPayment p = rep.OwnSSISPayments.GetSingle(paymentId);
            if (p == null)
                return null;
            SSISPaymentModel model = new SSISPaymentModel();
            model.FromOwnPayments = true;
            model.CardAccountID = p.CardAccountID;
            model.Number = p.Number;
            model.SSISPaymentID = p.SSISPaymentID;
            return model;
        }
    }
}