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
	public class LockUnlockCardAccount : BaseOperation {

		private CardAccount CardAccount { get; set; }
		private BankAccount BankAccount { get; set; }
		private Currency Currency { get; set; }

		public int IdCardAccount { get; set; }


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

				CardAccount.IsLocked = CardAccount.IsLocked == true ? false : true;
				CardAccount.Status = CardAccount.IsLocked == true ? 
					DataSource.CardStatusLocked : DataSource.CardStatusUnLocked; 

				bankService.UpdateCardAccount(CardAccount);

				BankAccount = bankService.GetBankAccountById(CardAccount.BankAccountID);

				if (BankAccount != null) Currency = bankService.GetCurrencyById(BankAccount.CurrencyID);

			}

			Information = CardAccount.IsLocked == true ? StringSource.CardAccountLocked() : StringSource.CardAccountUnlocked();
			Information+="\n"+StringSource.CardAccountInformation(CardAccount,BankAccount,Currency);
			return true;
		}
	}
}
