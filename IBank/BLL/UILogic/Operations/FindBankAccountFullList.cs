using BLL.ServiceReference1;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UILogic.Operations {
	public class FindBankAccountFullList:BaseOperationGetFullList<BankAccount> {


		private IQueryable<Customer> Customers { get; set; }
		private Currency[] Currencies { get; set; }
		

		public override bool Execute() {
			var isbs = base.Execute();
			if (!isbs) return false;

			int id=0;

			using (var services = new BankServiceClient()) {

				id = LastElement == null ? 0 : LastElement.BankAccountID;
				if (!IsUpdate) {
					if (IsNext) {
						CurrentList=services.GetNextBankAccounts(id, AmountElements).ToList();
					} else {
						if (FirstElement == null) id = int.MaxValue;
						else id = FirstElement.BankAccountID;
						CurrentList = services.GetPrevBankAccounts(id, AmountElements).ToList();
					}
				} else {
					CurrentList = services.GetNextBankAccounts(id-1, AmountElements).ToList();
				}

				Currencies=services.GetAllCurrency().ToArray();

			}

			if (CurrentList != null && CurrentList.Count > 0) {
				FirstElement = CurrentList.First();
				LastElement = CurrentList.Last();
			} else {
				FirstElement = null;
				LastElement = null;
				Information = StringSource.EndList();
				return false;
			}

			Customer cust = null;
			Currency curr = null;
			using (var localrepos = new DAL.Repositories()) {
				Customers = localrepos.Customers.GetAll();
				foreach (var el in CurrentList) {
					if (Customers.Count() > 0) {
						cust = Customers.FirstOrDefault(cs => cs.CustomerID == el.CustomerID);
					}
					if (Currencies.Count() > 0) {
						curr = Currencies.FirstOrDefault(cur => cur.CurrencyID == el.CurrencyID);
					}
					EntitiesInformation.Add(
						StringSource.BankAccountAndClientInfo(	el, cust, curr));
					IdsEntities.Add(el.BankAccountID);
				}
			}
			Information = StringSource.Successfully();
			return true;

		}

	}
}
