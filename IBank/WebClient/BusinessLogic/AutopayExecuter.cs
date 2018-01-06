using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using DAL;
using Entities;
using WebClient.BankService;
using WebClient.Models;

namespace WebClient.BusinessLogic
{

    public static class Time
    {
        public static DateTime GetTime()
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "E. Europe Standard Time").AddHours(1);
        }
    }

    public class AutopayExecuter
    {
        private Repositories repo = new Repositories();
        private BankServiceClient service = new BankServiceClient();

        public void Execute()
        {
            while (true)
            {
                var autoPays = repo.MobileAutoPayments.GetAll().ToList();

                foreach (var autoPay in autoPays)
                {
                    if (autoPay.LastExecutionDate == new DateTime(2000, 1, 1, 1, 1, 1))
                    {
                        if (autoPay.StartDate <= Time.GetTime())
                        {
                            var transaction = PrepareTransaction(autoPay);
                            bool sucess = service.CreateMobileTransaction(transaction);
                            if (sucess)
                            {
                                autoPay.LastExecutionDate = Time.GetTime();
                                repo.MobileAutoPayments.Update(autoPay);
                                repo.SaveChanges();
                            }
                            
                        }
                    }
                    else
                    {
                        if (autoPay.LastExecutionDate.AddTicks(autoPay.Interval.Ticks) <= Time.GetTime())
                        {
                            var transaction = PrepareTransaction(autoPay);
                            bool sucess = service.CreateMobileTransaction(transaction);
                            if (sucess)
                            {
                                autoPay.LastExecutionDate = Time.GetTime();
                                repo.MobileAutoPayments.Update(autoPay);
                                repo.SaveChanges();
                            }
                        }
                    }
                }
                Thread.Sleep(new TimeSpan(0, 0, 1, 0));
            }
            
        }

        public MobileTransaction PrepareTransaction(MobileAutoPay model)
        {
            MobileTransaction transaction = new MobileTransaction()
            {
                Amount = model.Amount,
                CardAccountID = model.PayCardId,
                CustomerID = model.CustomerID,
                Date = Time.GetTime(),
                MobileProvider = model.MobileOperator,
                Phone = model.MobileNumber,
                Type = PaymentType.Mobile
            };

            return transaction;
        }
    }
}