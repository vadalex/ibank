using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ServiceReference1;
using Entities;
using DAL;

namespace BLL.UILogic.Operations {
	public class EditCardAccount:BaseOperation {

		private Customer Client { get; set; }
		private BankAccount BankAccount { get; set; }
		private CardAccount CardAccount { get; set; }
		private string CartNumber { get; set; }

		public int IdClient { get; set; }
		public int IdBankAccount { get; set; }
		public int IdCardAccount { get; set; }
		public string CardAccountName { get; set; }
		public string CardType { get; set; }
		public string CardAccountStatus { get; set; }
		public DateTime ExpiredDate { get; set; }
		public bool IsLocked { get; set; }
		public string CardAccountNumber { get; set; }

		
		public override bool Execute() {
			if (IdCardAccount <= 0) {
				Information = StringSource.CardAccountNotFound();
				return false;
			}
			if (!Verifier.CheckCardAccountNumber(CardAccountNumber)) {
				Information = StringSource.CardAccountNumberStructureError();
				return false;
			}
			if (!Verifier.CheckExpiredDate(ExpiredDate)) {
				Information = StringSource.ExpiredDateStructureError();
				return false;
			}
			if (!Verifier.CheckName(CardAccountName)) {
				Information = StringSource.NameStructureError();
				return false;
			}

			if (!Verifier.CheckName(CardType)) {
				Information = StringSource.NameStructureError();
				return false;
			}
			if (!Verifier.CheckName(CardAccountStatus)) {
				Information = StringSource.NameStructureError();
				return false;
			}

		
			using (var localrepos = new Repositories()) {
				Client = localrepos.Customers.GetSingle(IdClient);
			}
			if (Client == null) {
				Information = StringSource.ClientNotFound();
				return false;
			}

			using (var bankservices = new BLL.ServiceReference1.BankServiceClient()) {
				BankAccount = bankservices.GetBankAccountById(IdBankAccount);
				if (BankAccount == null) {
					Information = StringSource.BankAccountNotFound();
					return false;
				}
				
				CardAccount = bankservices.GetCardAccountById(IdCardAccount);
				if (CardAccount == null) {
					Information = StringSource.CardAccountNotFound();
					return false;
				}

				//CardAccount.CardAccountID=
				CardAccount.BankAccountID = IdBankAccount;
				//CardAccount.CreatedDate
				CardAccount.IsLocked = IsLocked;
				CardAccount.CustomerID = IdClient;
				CardAccount.CardNumber = CardAccountNumber;
				CardAccount.CardType = CardType;
				CardAccount.ExpiredDate = ExpiredDate;
				CardAccount.Name = CardAccountName;
				CardAccount.Status = CardAccountStatus;
				//CardAccount.IsDefault				

				bankservices.UpdateCardAccount(CardAccount);
			}

			Information = StringSource.CardAccountEdited();
			return true;
		}


	}
}
