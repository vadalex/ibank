using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankDAL;
using BankEntities;

namespace BankDAL
{
    public class BankInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BankContext>
    {
        protected override void Seed(BankContext context)
        {
            var currency = new List<Currency>
            {
                new Currency(){ CurrencyID=1, Name="Белорусский рубль", ShortName="BYR", Rate=1},
                new Currency(){ CurrencyID=2, Name="Евро", ShortName="EUR", Rate=15000},
                new Currency(){ CurrencyID=3, Name="Доллар США", ShortName="USD", Rate=11000},
                new Currency(){ CurrencyID=4, Name="Российский рубль", ShortName="RUB", Rate=200}                
            };
            currency.ForEach(s => context.Currency.Add(s));
            context.SaveChanges();

            var bankaccounts = new List<BankAccount>
            {
                new BankAccount(){ BankAccountID=1, Balance=1000, AcountNumber="1150081065024333", CurrencyID=2, CustomerID=1, IsLocked = false, Password="12345678" },
                new BankAccount(){ BankAccountID=2, Balance=1000, AcountNumber="1150081065023542", CurrencyID=3, CustomerID=2, IsLocked = false, Password="12345678" },
                new BankAccount(){ BankAccountID=3, Balance=100000, AcountNumber="1150081065022324", CurrencyID=4, CustomerID=3, IsLocked = false, Password="12345678" },
                new BankAccount(){ BankAccountID=4, Balance=10000000, AcountNumber="8981150081065021", CurrencyID=1, CustomerID=4, IsLocked = false, Password="12345678" },
                new BankAccount(){ BankAccountID=5, Balance=10000000, AcountNumber="1777700810650213", CurrencyID=1, CustomerID=1, IsLocked = false, Password="12345678" },
                new BankAccount(){ BankAccountID=6, Balance=10000000, AcountNumber="1150081065021284", CurrencyID=1, CustomerID=2, IsLocked = false, Password="12345678" },

                new BankAccount(){ BankAccountID=7, Balance=0, AcountNumber="1111111111111111", CurrencyID=1, CustomerID=0, IsLocked = false, Password="12345678" },
                new BankAccount(){ BankAccountID=8, Balance=0, AcountNumber="1111111111111112", CurrencyID=2, CustomerID=0, IsLocked = false, Password="12345678" },
                new BankAccount(){ BankAccountID=9, Balance=0, AcountNumber="1111111111111113", CurrencyID=3, CustomerID=0, IsLocked = false, Password="12345678" },
                new BankAccount(){ BankAccountID=10, Balance=0, AcountNumber="1111111111111114", CurrencyID=4, CustomerID=0, IsLocked = false, Password="12345678" }
            };
            bankaccounts.ForEach(s => context.BankAccounts.Add(s));
            context.SaveChanges();

            var accounts = new List<CardAccount>
            {
                new CardAccount(){ CardAccountID=1, CardNumber="4916989600273307", CustomerID=1, IsLocked=false, Name="Моя Visa", BankAccountID= 1, CardType="Visa Classic", Status="Действительна", ExpiredDate=new DateTime(2018, 07, 1) },                
                new CardAccount(){ CardAccountID=2, CardNumber="3916982601273307", CustomerID=2, IsLocked=false, Name="Карточка", BankAccountID= 2, CardType="Mastercard", Status="Действительна", ExpiredDate=new DateTime(2017, 06, 1) },
                new CardAccount(){ CardAccountID=3, CardNumber="3216989600273307", CustomerID=3, IsLocked=false, Name="Мaestro", BankAccountID= 3, CardType="Maestro", Status="Действительна", ExpiredDate=new DateTime(2020, 02, 1) },
                new CardAccount(){ CardAccountID=4, CardNumber="8911239600273307", CustomerID=4, IsLocked=false, Name="Бел. рубли", BankAccountID= 4, CardType="Белкарт", Status="Действительна", ExpiredDate=new DateTime(2015, 03, 1) },
                new CardAccount(){ CardAccountID=5, CardNumber="1006989600273111", CustomerID=1, IsLocked=false, Name="Мой Mastercard", BankAccountID= 1, CardType="Mastercard", Status="Действительна", ExpiredDate=new DateTime(2017, 07, 1) },                
                new CardAccount(){ CardAccountID=6, CardNumber="1226982601279809", CustomerID=2, IsLocked=false, Name="Карточка 2", BankAccountID= 2, CardType="Visa Gold", Status="Действительна", ExpiredDate=new DateTime(2019, 02, 1) },
                new CardAccount(){ CardAccountID=7, CardNumber="1886989600273123", CustomerID=1, IsLocked=false, Name="Белки", BankAccountID= 5, CardType="Белкарт", Status="Действительна", ExpiredDate=new DateTime(2016, 08, 1) },                
                new CardAccount(){ CardAccountID=8, CardNumber="1776982601279876", CustomerID=2, IsLocked=false, Name="Белки", BankAccountID= 6, CardType="Белкарт", Status="Действительна", ExpiredDate=new DateTime(2019, 02, 1) },

                new CardAccount(){ CardAccountID=9, CardNumber="1111111111111111", CustomerID=0, IsLocked=false, Name="Банк BYR", BankAccountID= 7, CardType="Visa", Status="Действительна", ExpiredDate=DateTime.MaxValue},                
                new CardAccount(){ CardAccountID=10, CardNumber="1111111111111112", CustomerID=0, IsLocked=false, Name="Банк EUR", BankAccountID= 8, CardType="Visa", Status="Действительна", ExpiredDate=DateTime.MaxValue },
                new CardAccount(){ CardAccountID=11, CardNumber="1111111111111113", CustomerID=0, IsLocked=false, Name="Банк USD", BankAccountID= 9, CardType="Visa", Status="Действительна", ExpiredDate=DateTime.MaxValue },
                new CardAccount(){ CardAccountID=12, CardNumber="1111111111111114", CustomerID=0, IsLocked=false, Name="Банк RUB", BankAccountID= 10, CardType="Visa", Status="Действительна", ExpiredDate=DateTime.MaxValue },

            };
            accounts.ForEach(s => context.CardAccounts.Add(s));
            context.SaveChanges();

            var employees = new List<Employee>
            {
                new Employee(){EmployeeID=1, IsAdmin = true, Login="adminadmin", Password="adminadmin" },
                new Employee(){EmployeeID=2, IsAdmin = false, Login="operator", Password="operator" },
                new Employee(){EmployeeID=3, IsAdmin = true, Login="adminadmin1", Password="adminadmin1" },
                new Employee(){EmployeeID=4, IsAdmin = false, Login="operator1", Password="operator1" }                
            };
            employees.ForEach(s => context.Employees.Add(s));
            context.SaveChanges();

            var ssisTransactions = new List<SSISTransaction>
            {                
            };
            ssisTransactions.ForEach(s => context.SSISTransactions.Add(s));
            context.SaveChanges();

            var transferTransactions = new List<TransferTransaction>
            {                
                
            };
            transferTransactions.ForEach(s => context.TransferTransactions.Add(s));
            context.SaveChanges();
        }
    }
}
