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
	public class GetClientInformation : BaseOperation {

		private Customer Client { get; set; }


		public int IdClient { get; set; }

		public override bool Execute() {

			if (IdClient <= 0) {
				Information = StringSource.ClientNotFound();
				return false;
			}

			var info = new StringBuilder();
			using (var localrepos = new Repositories()) {
				Client = localrepos.Customers.GetSingle(IdClient);
				if (Client == null) {
					Information = StringSource.ClientNotFound();
					return false;
				}
				Client.AccessCards = Client.AccessCards.ToList();
				foreach (var el in Client.AccessCards) {
					el.AccessCodes = localrepos.AccessCodes.GetAll(el2 => el2.AccessCardID == el.AccessCardID).ToList();
				}
			}

			info.Append(StringSource.ClientInformation(Client,true));

			Currency currency = null;
			BankAccount bankacc = null;
			CardAccount[] cardaccounts = null;
			using (var bankservice = new BankServiceClient()) {
				cardaccounts = bankservice.GetCardAccountsByCustomerId(Client.CustomerID);
				if (cardaccounts == null || cardaccounts.Length <= 0) {
					info.Append(StringSource.CardAccountNotFound());
				}
				foreach (var el in cardaccounts) {
					bankacc = bankservice.GetBankAccountById(el.BankAccountID);
					if (bankacc == null) {
						info.Append(StringSource.BankAccountNotFound());
					} else {
						currency = bankservice.GetCurrencyById(bankacc.CurrencyID);
						if (currency == null) {
							info.Append(StringSource.CurrencyNotFound());
						} else {
							info.Append(StringSource.CardAccountInformation(el, bankacc, currency));
						}
					}

				}
			}



			Information = info.ToString();
			return true;
		}
	}
}
