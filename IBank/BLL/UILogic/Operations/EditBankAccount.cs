using BLL.ServiceReference1;
using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UILogic.Operations {
	public class EditBankAccount:BaseOperation {

		public int IdBankAccount { get; set; }
		public int IdCurrency { get; set; }
		public int IdCustomer { get; set; }
		public string Balance { get; set; }
		public bool IsLocked { get; set; }
		public string BankAccountNumber { get; set; }


		private BankAccount BankAccount { get; set; }
		private Customer Client { get; set; }
		private Currency Currency { get; set; }


		public override bool Execute() {
			if (IdBankAccount <= 0) {
				Information = StringSource.BankAccountNotFound();
				return false;
			}
			if (IdCurrency <= 0) {
				Information = StringSource.CurrencyNotFound();
				return false;
			}
			if (IdCustomer <= 0) {
				Information = StringSource.ClientNotFound();
				return false;
			}
			if (!Verifier.CheckBalance(Balance)) {
				Information = StringSource.BalanceStructureError();
				return false;
			}


			using (var localrepos = new Repositories()) {
				Client = localrepos.Customers.GetSingle(IdCustomer);
			}
			if (Client == null) {
				Information = StringSource.ClientNotFound();
				return false;
			}

			using (var bankservice = new BankServiceClient()) {

				BankAccount = bankservice.GetBankAccountById(IdBankAccount);
				if (BankAccount == null) {
					Information = StringSource.BankAccountNotFound();
					return false;
				}
				if (BankAccount.AcountNumber!=BankAccountNumber&&bankservice.GetBankAccountByNumber(BankAccountNumber) != null) {
					Information=StringSource.BankAccountNumberIsContains();
					return false;
				}

				//Currency	...

				BankAccount.Balance = double.Parse(Balance);
				BankAccount.AcountNumber = BankAccountNumber;
				BankAccount.IsLocked = IsLocked;
				BankAccount.CurrencyID = IdCurrency;
				BankAccount.CustomerID = IdCustomer;

				bankservice.UpdateBankAccount(BankAccount);
			}

			Information = StringSource.BankAccountEdited();
			return true;	
		}


	}
}
