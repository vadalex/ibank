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
	public class FindCardAccountByNumber:BaseOperation {

		public string CardAccountNumber{get;set;}


		private Customer Customer { get; set; }
		private CardAccount CardAccount { get; set; }

		public int IdCardAccount { get; protected set; }


		public override bool Execute() {

			if (!Verifier.CheckCardAccountNumber(CardAccountNumber)) {
				Information = StringSource.CardAccountNumberStructureError();
				return false;
			}

			StringBuilder textMessage = new StringBuilder();
			using (var bankService = new BLL.ServiceReference1.BankServiceClient()) {
				CardAccount = bankService.GetCardAccountByNumber(CardAccountNumber);
			}

			if (CardAccount != null) {
				using (var localrepos = new DAL.Repositories()) {
					Customer = localrepos.Customers.GetSingle(CardAccount.CustomerID);
				}

				textMessage.Append(StringSource.CardAccountAndClientShortInfo(CardAccount, Customer));
				textMessage.Append("\n");
				if (CardAccount.IsLocked == true) {
					textMessage.Append(StringSource.CardAccountLocked());
				} else {
					textMessage.Append(StringSource.CardAccountUnlocked());
				}
			} else {
				Information= StringSource.CardAccountNotFound();
				return false;
			}

			Information = textMessage.ToString();
			IdCardAccount = CardAccount.CardAccountID;
			return true;
		}
	}
}
