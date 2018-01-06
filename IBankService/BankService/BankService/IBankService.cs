using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BankEntities;
using System.Linq.Expressions;

namespace BankService
{    
    [ServiceContract]
    [ServiceKnownType(typeof(ArbitraryTransaction))]
    [ServiceKnownType(typeof(TransferTransaction))]
    [ServiceKnownType(typeof(MobileTransaction))]
    [ServiceKnownType(typeof(SSISTransaction))]
    public interface IBankService
    {
        [OperationContract]
        BankAccount GetBankAccountById(int Id);
        [OperationContract]
        BankAccount GetBankAccountByNumber(string number);
        [OperationContract]
        ICollection<BankAccount> GetAllBankAccounts();
        [OperationContract]
        void AddBankAccount(BankAccount bankAccount);
        [OperationContract]
        void UpdateBankAccount(BankAccount bankAccount);
        [OperationContract]
        void RemoveBankAccount(BankAccount bankAccount);
        [OperationContract]
        double GetBankAccountBalance(int bankAccountId);

        [OperationContract]
        ICollection<BankAccount> GetNextBankAccounts(int lastElementId, int ammount);
        [OperationContract]
        ICollection<BankAccount> GetPrevBankAccounts(int firstElementId, int ammount);
        [OperationContract]
        ICollection<CardAccount> GetNextCardAccounts(int lastElementId, int ammount);
        [OperationContract]
        ICollection<CardAccount> GetPrevCardAccounts(int firstElementId, int ammount);
        [OperationContract]
        ICollection<Employee> GetNextOperators(int lastElementId, int ammount);
        [OperationContract]
        ICollection<Employee> GetPrevOperators(int firstElementId, int ammount);

        [OperationContract]
        CardAccount GetCardAccountById(int Id);
        [OperationContract]
        CardAccount GetCardAccountByNumber(string number);
        [OperationContract]
        ICollection<CardAccount> GetAllCardAccounts();
        [OperationContract]
        ICollection<CardAccount> GetCardAccountsByCustomerId(int customerId);
        [OperationContract]
        void AddCardAccount(CardAccount cardAccount);
        [OperationContract]
        void UpdateCardAccount(CardAccount cardAccount);
        [OperationContract]
        void RemoveCardAccount(CardAccount cardAccount);
        [OperationContract]
        bool IsEnoughBalance(int cardAccountId, double amount);
        


        [OperationContract]
        Currency GetCurrencyById(int Id);
        [OperationContract]
        ICollection<Currency> GetAllCurrency();
        [OperationContract]
        void AddCurrency(Currency currency);
        [OperationContract]
        void UpdateCurrency(Currency currency);
        [OperationContract]
        void RemoveCurrency(Currency currency);
        [OperationContract]
        void RemoveAllCurrency();
        [OperationContract]
        string GetBankAccountCurrencyShortString(int bankAccountId);


        [OperationContract]
        Employee GetEmployeeById(int Id);
        [OperationContract]
        ICollection<Employee> GetAllEmployees();
        [OperationContract]
        void AddEmployee(Employee employee);
        [OperationContract]
        void UpdateEmployee(Employee employee);
        [OperationContract]
        void RemoveEmployee(Employee employee);
        [OperationContract]
        Employee GetEmployeeByLogin(string login);
        [OperationContract]
        ICollection<Employee> GetAllAdminEmployees();

        
        [OperationContract]
        ArbitraryTransaction GetArbitraryTransactionById(int Id);
        [OperationContract]
        ICollection<ArbitraryTransaction> GetAllArbitraryTransactions();
        [OperationContract]
        void AddArbitraryTransaction(ArbitraryTransaction arbitraryTransaction);
        [OperationContract]
        void UpdateArbitraryTransaction(ArbitraryTransaction arbitraryTransaction);
        [OperationContract]
        void RemoveArbitraryTransaction(ArbitraryTransaction arbitraryTransaction);
        [OperationContract]
        bool CreateArbitraryTransaction(ArbitraryTransaction arbitraryTransaction);


        [OperationContract]
        MobileTransaction GetMobileTransactionById(int Id);
        [OperationContract]
        ICollection<MobileTransaction> GetAllMobileTransactions();
        [OperationContract]
        void AddMobileTransaction(MobileTransaction mobileTransaction);
        [OperationContract]
        void UpdateMobileTransaction(MobileTransaction mobileTransaction);
        [OperationContract]
        void RemoveMobileTransaction(MobileTransaction mobileTransaction);
        [OperationContract]
        bool CreateMobileTransaction(MobileTransaction mobileTransaction);


        [OperationContract]
        TransferTransaction GetTransferTransactionById(int Id);
        [OperationContract]
        ICollection<TransferTransaction> GetAllTransferTransactions();
        [OperationContract]
        void AddTransferTransaction(TransferTransaction transferTransaction);
        [OperationContract]
        void UpdateTransferTransaction(TransferTransaction transferTransaction);
        [OperationContract]
        void RemoveTransferTransaction(TransferTransaction transferTransaction);
        [OperationContract]
        bool CreateTransferTransaction(TransferTransaction transferTransaction);


        [OperationContract]
        SSISTransaction GetSSISTransactionById(int Id);
        [OperationContract]
        ICollection<SSISTransaction> GetAllSSISTransactions();
        [OperationContract]
        void AddSSISTransaction(SSISTransaction SSISTransaction);
        [OperationContract]
        void UpdateSSISTransaction(SSISTransaction SSISTransaction);
        [OperationContract]
        void RemoveSSISTransaction(SSISTransaction SSISTransaction);
        [OperationContract]
        bool CreateSSISTransaction(SSISTransaction SSISTransaction);

        
        [OperationContract]
        ICollection<ArbitraryTransaction> GetArbitraryTransactionsByCardId(int cardId, DateTime from, DateTime to);
        [OperationContract]
        ICollection<MobileTransaction> GetMobileTransactionsByCardId(int cardId, DateTime from, DateTime to);
        [OperationContract]
        ICollection<SSISTransaction> GetSSISTransactionsByCardId(int cardId, DateTime from, DateTime to);
        [OperationContract]
        ICollection<TransferTransaction> GetTransferTransactionsByCardId(int cardId, DateTime from, DateTime to);
    }
}

