using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using BLL.ServiceReference1;
using DAL;
using Entities;

namespace BLL.UILogic {
	public class Supporting {

		


		public bool IsLoginContains(string login) {
			using (var localrepos = new Repositories()) {
				////////////////////////////////////////////////////////////////
				var css = localrepos.Customers.GetAll();
				if (css == null || css.Where(cl => cl.Login == login).Count() <= 0) {
					return false;
				}
				return true;
			}
		}

		public bool RemoveClientInformationById(int IdClient,bool isRemoveClient=false) {
			Customer client;

			using (var localrepos = new Repositories()) {
				client = localrepos.Customers.GetSingle(IdClient);
				if (client == null) {
					return false;
				}
			}


			if (client != null) {

				CardAccount[] cardaccounts = null;
				BankAccount bankaccount = null;
				using (var bankservice = new BankServiceClient()) {

					cardaccounts = bankservice.GetCardAccountsByCustomerId(client.CustomerID);
					if (cardaccounts != null) {
						for (int i = 0; i < cardaccounts.Length;i++ ) {

							bankaccount = bankservice.GetBankAccountById(cardaccounts[i].BankAccountID);
							if (bankaccount != null) {
								bankservice.RemoveBankAccount(bankaccount);
							}
							
							bankservice.RemoveCardAccount(cardaccounts[i]);

						}
					}

				}

				if (isRemoveClient) {
					using (var localrepos = new Repositories()) {
							localrepos.Customers.Remove(client);
							localrepos.SaveChanges(); 
					}
				}

			} else {
				return false;
			}

			return true;
		}


	}
}
