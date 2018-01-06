using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BankEntities;
using BankDAL;
using System.Linq.Expressions;

namespace BankService
{

    public static class Time
    {
        public static DateTime GetTime()
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.Now, "E. Europe Standard Time").AddHours(1);
        }
    }

    public class BankService : IBankService
    {
        #region BankAccount
        public BankAccount GetBankAccountById(int Id)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.BankAccounts.GetSingle(Id);
            }
        }

        public BankAccount GetBankAccountByNumber(string number)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.BankAccounts.GetAll(a => a.AcountNumber.Equals(number)).FirstOrDefault();
            }
        }

        public ICollection<BankAccount> GetAllBankAccounts()
        {
            using (Repositories rep = new Repositories())
            {
                return rep.BankAccounts.GetAll().ToList();
            }
        }

        public void AddBankAccount(BankAccount bankAccount)
        {
            using (Repositories rep = new Repositories())
            {
                rep.BankAccounts.Add(bankAccount);
                rep.SaveChanges();
            }
        }

        public void UpdateBankAccount(BankAccount bankAccount)
        {
            using (Repositories rep = new Repositories())
            {
                rep.BankAccounts.Update(bankAccount);
                rep.SaveChanges();
            }
        }

        public void RemoveBankAccount(BankAccount bankAccount)
        {
            using (Repositories rep = new Repositories())
            {
                rep.BankAccounts.Remove(bankAccount);
                rep.SaveChanges();
            }
        }
        public double GetBankAccountBalance(int bankAccountId)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.BankAccounts.GetSingle(bankAccountId).Balance;
            }
        }

        public ICollection<BankAccount> GetNextBankAccounts(int lastElementId, int ammount)
        {
            using (Repositories rep = new Repositories())
            {

                return rep.BankAccounts.GetAll(el => el.BankAccountID > lastElementId).ToList()
                            .OrderBy(e => e.BankAccountID).Take(ammount).ToList();
            }                        
        }

        public ICollection<BankAccount> GetPrevBankAccounts(int firstElementId, int ammount)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.BankAccounts.GetAll(el => el.BankAccountID < firstElementId).ToList()
                            .OrderByDescending(e => e.BankAccountID).Take(ammount).OrderBy(el => el.BankAccountID).ToList();                
            }
        }

        public ICollection<CardAccount> GetNextCardAccounts(int lastElementId, int ammount)
        {
            using (Repositories rep = new Repositories())
            {

                return rep.CardAccounts.GetAll(el => el.CardAccountID> lastElementId).ToList()
                            .OrderBy(e => e.CardAccountID).Take(ammount).ToList();
            }
        }

        public ICollection<CardAccount> GetPrevCardAccounts(int firstElementId, int ammount)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.CardAccounts.GetAll(el => el.CardAccountID < firstElementId).ToList()
                            .OrderByDescending(e => e.CardAccountID).Take(ammount).OrderBy(el => el.CardAccountID).ToList();
            }
        }

        public ICollection<Employee> GetNextOperators(int lastElementId, int ammount)
        {
            using (Repositories rep = new Repositories())
            {

                return rep.Employees.GetAll(el => el.EmployeeID > lastElementId && !el.IsAdmin).ToList()
                            .OrderBy(e => e.EmployeeID).Take(ammount).ToList();
            }
        }

        public ICollection<Employee> GetPrevOperators(int firstElementId, int ammount)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.Employees.GetAll(el => el.EmployeeID > firstElementId && !el.IsAdmin).ToList()
                            .OrderByDescending(e => e.EmployeeID).Take(ammount).OrderBy(el => el.EmployeeID).ToList();
            }
        }
        #endregion

        #region CardAccount

        public CardAccount GetCardAccountById(int Id)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.CardAccounts.GetSingle(Id);
            }
        }
        public CardAccount GetCardAccountByNumber(string number)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.CardAccounts.GetAll(a => a.CardNumber == number).FirstOrDefault();
            }
        }
        public ICollection<CardAccount> GetAllCardAccounts()
        {
            using (Repositories rep = new Repositories())
            {
                return rep.CardAccounts.GetAll().ToList();
            }
        }

        public ICollection<CardAccount> GetCardAccountsByCustomerId(int customerId)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.CardAccounts.GetAll(c => c.CustomerID == customerId).ToList();
            }
        }

        public void AddCardAccount(CardAccount cardAccount)
        {
            using (Repositories rep = new Repositories())
            {
                rep.CardAccounts.Add(cardAccount);
                rep.SaveChanges();
            }
        }

        public void UpdateCardAccount(CardAccount cardAccount)
        {
            using (Repositories rep = new Repositories())
            {
                rep.CardAccounts.Update(cardAccount);
                rep.SaveChanges();
            }
        }

        public void RemoveCardAccount(CardAccount cardAccount)
        {
            using (Repositories rep = new Repositories())
            {
                rep.CardAccounts.Remove(cardAccount);
                rep.SaveChanges();
            }
        }
        public bool IsEnoughBalance(int cardAccountId, double amount)
        {
            using (Repositories rep = new Repositories())
            {
                double balance = rep.BankAccounts.GetSingle(rep.CardAccounts.GetSingle(cardAccountId).BankAccountID).Balance;
                return balance >= amount;                
            }
        }
        #endregion
        
        #region Currency
        public Currency GetCurrencyById(int Id)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.Currency.GetSingle(Id);
            }
        }

        public ICollection<Currency> GetAllCurrency()
        {
            using (Repositories rep = new Repositories())
            {
                return rep.Currency.GetAll().ToList();
            }
        }

        public void AddCurrency(Currency currency)
        {
            using (Repositories rep = new Repositories())
            {
                rep.Currency.Add(currency);
                rep.SaveChanges();
            }
        }

        public void UpdateCurrency(Currency currency)
        {
            using (Repositories rep = new Repositories())
            {
                rep.Currency.Update(currency);
                rep.SaveChanges();
            }
        }

        public void RemoveCurrency(Currency currency)
        {
            using (Repositories rep = new Repositories())
            {
                rep.Currency.Remove(currency);
                rep.SaveChanges();
            }
        }

        public void RemoveAllCurrency()
        {
            using (Repositories rep = new Repositories())
            {
                foreach(Currency c in rep.Currency.GetAll())
                {
                    rep.Currency.Remove(c);
                }                
                rep.SaveChanges();
            }
        }
        public string GetBankAccountCurrencyShortString(int bankAccountId)
        {
            using (Repositories rep = new Repositories())
            {
                var currencyId = rep.BankAccounts.GetSingle(bankAccountId).CurrencyID;
                return rep.Currency.GetSingle(currencyId).ShortName;
            }         
        }
        #endregion

        #region Employee
        public Employee GetEmployeeById(int Id)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.Employees.GetSingle(Id);
            }
        }

        public ICollection<Employee> GetAllEmployees()
        {
            using (Repositories rep = new Repositories())
            {
                return rep.Employees.GetAll().ToList();
            }
        }

        public void AddEmployee(Employee employee)
        {
            using (Repositories rep = new Repositories())
            {
                rep.Employees.Add(employee);
                rep.SaveChanges();
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (Repositories rep = new Repositories())
            {
                rep.Employees.Update(employee);
                rep.SaveChanges();
            }
        }

        public void RemoveEmployee(Employee employee)
        {
            using (Repositories rep = new Repositories())
            {
                rep.Employees.Remove(employee);
                rep.SaveChanges();
            }
        }

        public Employee GetEmployeeByLogin(string login)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.Employees.GetAll(e => e.Login == login).FirstOrDefault();
            }
        }

        public ICollection<Employee> GetAllAdminEmployees()
        {
            using (Repositories rep = new Repositories())
            {
                return rep.Employees.GetAll(e=>e.IsAdmin).ToList();
            }

        }
        #endregion

        #region ArbitraryTransaction
        public ArbitraryTransaction GetArbitraryTransactionById(int Id)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.ArbitraryTransactions.GetSingle(Id);
            }
        }

        public ICollection<ArbitraryTransaction> GetAllArbitraryTransactions()
        {
            using (Repositories rep = new Repositories())
            {
                return rep.ArbitraryTransactions.GetAll().ToList();
            }
        }

        public void AddArbitraryTransaction(ArbitraryTransaction arbitraryTransaction)
        {
            using (Repositories rep = new Repositories())
            {
                rep.ArbitraryTransactions.Add(arbitraryTransaction);
                rep.SaveChanges();
            }
        }

        public void UpdateArbitraryTransaction(ArbitraryTransaction arbitraryTransaction)
        {
            using (Repositories rep = new Repositories())
            {
                rep.ArbitraryTransactions.Update(arbitraryTransaction);
                rep.SaveChanges();
            }
        }

        public void RemoveArbitraryTransaction(ArbitraryTransaction arbitraryTransaction)
        {
            using (Repositories rep = new Repositories())
            {
                rep.ArbitraryTransactions.Remove(arbitraryTransaction);
                rep.SaveChanges();
            }
        }

        public ICollection<ArbitraryTransaction> GetArbitraryTransactionsByCardId(int cardId, DateTime from, DateTime to)
        {
            using (Repositories rep = new Repositories())
            {
                var transactions = rep.ArbitraryTransactions.GetAll(t => t.CardAccountID == cardId).ToList();
                if (from == null && to == null)
                    return transactions;
                if (from == null)
                    return transactions.Where(t => t.Date < to.AddDays(1d).Date).ToList();
                if (to == null)
                    return transactions.Where(t => t.Date >= from.Date).ToList();
                return transactions.Where(t => t.Date >= from.Date && t.Date < to.AddDays(1d).Date).ToList();
            }
        }
        public bool CreateArbitraryTransaction(ArbitraryTransaction arbitraryTransaction)
        {
            try
            {
                using (Repositories rep = new Repositories())
                {
                    CardAccount card = rep.CardAccounts.GetSingle(arbitraryTransaction.CardAccountID);
                    BankAccount acc = rep.BankAccounts.GetSingle(card.BankAccountID);
                    BankAccount bank = null;
                    switch (acc.CurrencyID)
                    {
                        case 1:
                            bank = rep.BankAccounts.GetAll(a => a.AcountNumber == "1111111111111111").FirstOrDefault();
                            break;
                        case 2:
                            bank = rep.BankAccounts.GetAll(a => a.AcountNumber == "1111111111111112").FirstOrDefault();
                            break;
                        case 3:
                            bank = rep.BankAccounts.GetAll(a => a.AcountNumber == "1111111111111113").FirstOrDefault();
                            break;
                        case 4:
                            bank = rep.BankAccounts.GetAll(a => a.AcountNumber == "1111111111111114").FirstOrDefault();
                            break;                        
                    }


                    if (arbitraryTransaction.Amount > acc.Balance)
                        return false;
                    if (card.IsLocked)
                        return false;
                    if (card.ExpiredDate < Time.GetTime())
                        return false;
                    arbitraryTransaction.Date = Time.GetTime();
                    acc.Balance -= arbitraryTransaction.Amount;
                    bank.Balance += arbitraryTransaction.Amount;
                    rep.BankAccounts.Update(acc);
                    rep.BankAccounts.Update(bank);
                    rep.ArbitraryTransactions.Add(arbitraryTransaction);
                    rep.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region MobileTransaction
        public MobileTransaction GetMobileTransactionById(int Id)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.MobileTransactions.GetSingle(Id);
            }
        }

        public ICollection<MobileTransaction> GetAllMobileTransactions()
        {
            using (Repositories rep = new Repositories())
            {
                return rep.MobileTransactions.GetAll().ToList();
            }
        }

        public void AddMobileTransaction(MobileTransaction mobileTransaction)
        {
            using (Repositories rep = new Repositories())
            {
                rep.MobileTransactions.Add(mobileTransaction);
                rep.SaveChanges();
            }
        }

        public void UpdateMobileTransaction(MobileTransaction mobileTransaction)
        {
            using (Repositories rep = new Repositories())
            {
                rep.MobileTransactions.Update(mobileTransaction);
                rep.SaveChanges();
            }
        }

        public void RemoveMobileTransaction(MobileTransaction mobileTransaction)
        {
            using (Repositories rep = new Repositories())
            {
                rep.MobileTransactions.Remove(mobileTransaction);
                rep.SaveChanges();
            }
        }

        public ICollection<MobileTransaction> GetMobileTransactionsByCardId(int cardId, DateTime from, DateTime to)
        {
            using (Repositories rep = new Repositories())
            {
                var transactions = rep.MobileTransactions.GetAll(t => t.CardAccountID == cardId).ToList();
                if (from == null && to == null)
                    return transactions;
                if (from == null)
                    return transactions.Where(t => t.Date < to.AddDays(1d).Date).ToList();
                if (to == null)
                    return transactions.Where(t => t.Date >= from.Date).ToList();
                return transactions.Where(t => t.Date >= from.Date && t.Date < to.AddDays(1d).Date).ToList();
            }
        }
        public bool CreateMobileTransaction(MobileTransaction mobileTransaction)
        {
            try
            {
                using (Repositories rep = new Repositories())
                {
                    CardAccount card = rep.CardAccounts.GetSingle(mobileTransaction.CardAccountID);
                    BankAccount acc = rep.BankAccounts.GetSingle(card.BankAccountID);
                    BankAccount bank = rep.BankAccounts.GetAll(a => a.AcountNumber == "1111111111111111").FirstOrDefault();

                    if (acc.CurrencyID != 1)
                        return false;
                    if (mobileTransaction.Amount > acc.Balance)
                        return false;
                    if (card.IsLocked)
                        return false;
                    if (card.ExpiredDate < Time.GetTime())
                        return false;
                    mobileTransaction.Date = Time.GetTime();
                    acc.Balance -= mobileTransaction.Amount;
                    bank.Balance += mobileTransaction.Amount;
                    rep.BankAccounts.Update(acc);
                    rep.BankAccounts.Update(bank);
                    rep.MobileTransactions.Add(mobileTransaction);
                    rep.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region SSISTransaction
        public SSISTransaction GetSSISTransactionById(int Id)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.SSISTransactions.GetSingle(Id);
            }
        }

        public ICollection<SSISTransaction> GetAllSSISTransactions()
        {
            using (Repositories rep = new Repositories())
            {
                return rep.SSISTransactions.GetAll().ToList();
            }
        }

        public void AddSSISTransaction(SSISTransaction SSISTransaction)
        {
            using (Repositories rep = new Repositories())
            {
                rep.SSISTransactions.Add(SSISTransaction);
                rep.SaveChanges();
            }
        }

        public void UpdateSSISTransaction(SSISTransaction SSISTransaction)
        {
            using (Repositories rep = new Repositories())
            {
                rep.SSISTransactions.Update(SSISTransaction);
                rep.SaveChanges();
            }
        }

        public void RemoveSSISTransaction(SSISTransaction SSISTransaction)
        {
            using (Repositories rep = new Repositories())
            {
                rep.SSISTransactions.Remove(SSISTransaction);
                rep.SaveChanges();
            }
        }

        public ICollection<SSISTransaction> GetSSISTransactionsByCardId(int cardId, DateTime from, DateTime to)
        {
            using (Repositories rep = new Repositories())
            {
                var transactions = rep.SSISTransactions.GetAll(t => t.CardAccountID == cardId).ToList();
                if (from == null && to == null)
                    return transactions;
                if (from == null)
                    return transactions.Where(t => t.Date < to.AddDays(1d).Date).ToList();
                if (to == null)
                    return transactions.Where(t => t.Date >= from.Date).ToList();
                return transactions.Where(t => t.Date >= from.Date && t.Date < to.AddDays(1d).Date).ToList();
            }
        }
        public bool CreateSSISTransaction(SSISTransaction SSISTransaction)
        {
            try
            {
                using (Repositories rep = new Repositories())
                {
                    CardAccount card = rep.CardAccounts.GetSingle(SSISTransaction.CardAccountID);
                    BankAccount acc = rep.BankAccounts.GetSingle(card.BankAccountID);
                    BankAccount bank = rep.BankAccounts.GetAll(a => a.AcountNumber == "1111111111111111").FirstOrDefault();

                    if (acc.CurrencyID != 1)
                        return false;
                    if (SSISTransaction.Amount > acc.Balance)
                        return false;
                    if (card.IsLocked)
                        return false;
                    if (card.ExpiredDate < Time.GetTime())
                        return false;
                    SSISTransaction.Date = Time.GetTime();
                    acc.Balance -= SSISTransaction.Amount;
                    bank.Balance += SSISTransaction.Amount;
                    rep.BankAccounts.Update(acc);
                    rep.BankAccounts.Update(bank);
                    rep.SSISTransactions.Add(SSISTransaction);
                    rep.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region TransferTransaction
        public TransferTransaction GetTransferTransactionById(int Id)
        {
            using (Repositories rep = new Repositories())
            {
                return rep.TransferTransactions.GetSingle(Id);
            }
        }

        public ICollection<TransferTransaction> GetAllTransferTransactions()
        {
            using (Repositories rep = new Repositories())
            {
                return rep.TransferTransactions.GetAll().ToList();
            }
        }

        public void AddTransferTransaction(TransferTransaction transferTransaction)
        {
            using (Repositories rep = new Repositories())
            {
                rep.TransferTransactions.Add(transferTransaction);
                rep.SaveChanges();
            }
        }

        public void UpdateTransferTransaction(TransferTransaction transferTransaction)
        {
            using (Repositories rep = new Repositories())
            {
                rep.TransferTransactions.Update(transferTransaction);
                rep.SaveChanges();
            }
        }

        public void RemoveTransferTransaction(TransferTransaction transferTransaction)
        {
            using (Repositories rep = new Repositories())
            {
                rep.TransferTransactions.Remove(transferTransaction);
                rep.SaveChanges();
            }
        }

        public ICollection<TransferTransaction> GetTransferTransactionsByCardId(int cardId, DateTime from, DateTime to)
        {
            using (Repositories rep = new Repositories())
            {
                var transactions = rep.TransferTransactions.GetAll(t => t.CardAccountID == cardId).ToList();
                if (from == null && to == null)
                    return transactions;
                if (from == null)
                    return transactions.Where(t => t.Date < to.AddDays(1d).Date).ToList();
                if (to == null)
                    return transactions.Where(t => t.Date >= from.Date).ToList();
                return transactions.Where(t => t.Date >= from.Date && t.Date < to.AddDays(1d).Date).ToList();
            }
        }
        public bool CreateTransferTransaction(TransferTransaction transferTransaction)
        {
            try
            {
                using (Repositories rep = new Repositories())
                {
                    CardAccount targetCard = rep.CardAccounts.GetSingle(transferTransaction.TargetCardAccountID);                    
                    BankAccount targetAccount = rep.BankAccounts.GetSingle(targetCard.BankAccountID);
                    CardAccount card = rep.CardAccounts.GetSingle(transferTransaction.CardAccountID);                    
                    BankAccount acc = rep.BankAccounts.GetSingle(card.BankAccountID);
                    if (targetAccount.CurrencyID != acc.CurrencyID)
                        return false;
                    if (transferTransaction.Amount > acc.Balance)
                        return false;
                    if (card.IsLocked)
                        return false;
                    if (card.ExpiredDate < Time.GetTime())
                        return false;
                    if (targetAccount.AcountNumber == acc.AcountNumber)
                        return false;
                    transferTransaction.Date = Time.GetTime();
                    acc.Balance -= transferTransaction.Amount;
                    targetAccount.Balance += transferTransaction.Amount;
                    rep.BankAccounts.Update(acc);
                    rep.BankAccounts.Update(targetAccount);
                    rep.TransferTransactions.Add(transferTransaction);
                    rep.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
