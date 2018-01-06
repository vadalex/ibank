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
	public class RemoveCardAccount : BaseOperation {
		private CardAccount CardAccount { get; set; }
		public int IdCardAccount { get; set; }


		public override bool Execute() {

			if (IdCardAccount <= 0) {
				Information = StringSource.CardAccountNotFound();
				return false;
			}

			using (var bankservice = new BLL.ServiceReference1.BankServiceClient()) {
				CardAccount = bankservice.GetCardAccountById(IdCardAccount);

				if (CardAccount == null) {
					Information = StringSource.CardAccountNotFound();
					return false;
				}
				bankservice.RemoveCardAccount(CardAccount);
			}

			Information = StringSource.CardAccountRemoved(CardAccount.CardNumber);
			return true;
		}
	}
}
