using BLL.ServiceReference1;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UILogic.Operations {
	public class FindBankAccountById:BaseOperation {

		public int IdBankAccount { get;  set; }

		
		public Customer Customer { get; protected set; }
		public BankAccount BankAccount { get; protected set; }
		public Currency Currency { get; protected set; }


		public override bool Execute() {

			if (IdBankAccount<=0) {
				Information = StringSource.BankAccountNotFound();
				return false;
			}

			using (var bankService = new BLL.ServiceReference1.BankServiceClient()) {
				BankAccount = bankService.GetBankAccountById(IdBankAccount);

				if (BankAccount == null) {
					Information = StringSource.BankAccountNotFound();
					return false;
				}

				Currency = bankService.GetCurrencyById(BankAccount.CurrencyID);
			}

			using(var local=new DAL.Repositories()){
				Customer=local.Customers.GetSingle(BankAccount.CustomerID);
			}

			if (Currency == null) {
				Information = StringSource.CurrencyNotFound();
				return false;
			}

			IdBankAccount = BankAccount.BankAccountID;

			Information = StringSource.BankAccountAndClientInfo(BankAccount, Customer, Currency);
			return true;
		}

	}
}
