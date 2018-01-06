using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DAL;

using BLL.ServiceReference1;
using BLL.UILogic;

namespace BLL.UILogic.Operations {
	public class FindBankAccountByNumber:BaseOperation {

		private Customer Customer { get; set; }
		private BankAccount BankAccount { get; set; }


		public int IdBankAccount { get; protected set; }
		//
		private Currency Currency { get; set; }

		public string Balance { get; protected set; }
		public string CurrencyName { get; protected set; }
		public string CurrencyShortName { get; protected set; }
		public int IdCurrency { get; protected set; }
		//

		public string BankAccountNumber { get; set; }


		public override bool Execute() {

			if (!Verifier.CheckBankAccountNumber(BankAccountNumber)) {
				Information = StringSource.BankAccountNumberStructureError();
				return false;
			}

			using (var bankService = new BLL.ServiceReference1.BankServiceClient()) {
				BankAccount = bankService.GetBankAccountByNumber(BankAccountNumber);

				if (BankAccount == null) {
					Information = StringSource.BankAccountNotFound();
					return false;
				}

				Currency=bankService.GetCurrencyById(BankAccount.CurrencyID);
			}

			if (Currency == null) {
				Information=StringSource.CurrencyNotFound();
				return false;
			}

			IdBankAccount = BankAccount.BankAccountID;
			Balance = BankAccount.Balance.ToString();
			CurrencyName = Currency.Name;
			CurrencyShortName = Currency.ShortName;
			IdCurrency = Currency.CurrencyID;
			Information = StringSource.BankAccountAndClientInfo(BankAccount, Customer,Currency);
			return true;
		}
	}
}
