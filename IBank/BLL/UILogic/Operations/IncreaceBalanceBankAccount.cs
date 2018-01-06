using BLL.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UILogic.Operations {
	public class IncreaceBalanceBankAccount:BaseOperation {

		public int IdBankAccount { get; set; }
		//public string Password { get; set; }

		//public int IdCurrency { get; protected set; }
		public string Balance { get; set; }

		public string BankAccountNumber { get; protected set; }

		private BankAccount BankAccount { get; set; }


		public override bool Execute() {
			if (IdBankAccount <= 0) {
				Information = StringSource.BankAccountNotFound();
				return false;
			}
			//if (IdCurrency <= 0) {
			//	Information = StringSource.CurrencyNotFound();
			//	return false;
			//}
			if (!Verifier.CheckBalance(Balance)) {
				Information = StringSource.BalanceStructureError();
				return false;
			}
			//if (!Verifier.CheckPassword(Password)) {
			//	Information = StringSource.PasswordStructureError();
			//	return false;
			//}
			

			using (var bankservice = new BankServiceClient()) {

				BankAccount=bankservice.GetBankAccountById(IdBankAccount);

				if (BankAccount != null) {

					//if (Password != BankAccount.Password) {
					//	Information = StringSource.PasswordError();
					//	return false;
					//}

					BankAccount.Balance += double.Parse(Balance);
					//BankAccount.CurrencyID = IdCurrency;

					bankservice.UpdateBankAccount(BankAccount);

					BankAccountNumber = BankAccount.AcountNumber;
					Information = StringSource.BankAccountBalanceUp();
					return true;
				}

			}
			Information = StringSource.BankAccountNotFound();
			return false;
		}


	}
}
