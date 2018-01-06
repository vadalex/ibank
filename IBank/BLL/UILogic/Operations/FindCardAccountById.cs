using BLL.ServiceReference1;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UILogic.Operations {
	public class FindCardAccountById:BaseOperation {

		public int IdCardAccount { get; set; }

		public Customer Customer { get; protected set; }
		public BankAccount BankAccount { get; protected set; }
		public CardAccount CardAccount { get; protected set; }
		public Currency Currency { get; protected set; }


		public override bool Execute() {
			if (IdCardAccount <= 0) {
				Information = StringSource.CardAccountNotFound();
				return false;
			}

			using (var bankService = new BLL.ServiceReference1.BankServiceClient()) {
				CardAccount = bankService.GetCardAccountById(IdCardAccount);
				if (CardAccount == null) {
					Information = StringSource.CardAccountNotFound();
					return false;
				}

				BankAccount = bankService.GetBankAccountById(CardAccount.BankAccountID);
				if (BankAccount == null) {
					Information = StringSource.BankAccountNotFound();
					return false;
				}

				Currency = bankService.GetCurrencyById(BankAccount.CurrencyID);
				if (Currency == null) {
					Information = StringSource.CurrencyNotFound();
					return false;
				}
			}

			using (var local = new DAL.Repositories()) {
				Customer = local.Customers.GetSingle(CardAccount.CustomerID);
				if (Customer == null) {
					Information = StringSource.CurrencyNotFound();
					return false;
				}
			}


			Information = StringSource.CardAccountAndClientShortInfo(CardAccount, Customer);
			return true;
		}

	}
}
