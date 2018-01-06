using BLL.ServiceReference1;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UILogic.Operations {
	public class FindCardAccountFullList : BaseOperationGetFullList<CardAccount> {

		private IQueryable<Customer> Customers { get; set; }

	
		public override bool Execute() {
			var isbs = base.Execute();
			if (!isbs) return false;

			int id = 0;

			using (var services=new BankServiceClient()) {
				id = LastElement == null ? 0 : LastElement.CardAccountID;
				if (!IsUpdate) {
					if (IsNext) {
						CurrentList = services.GetNextCardAccounts(id,AmountElements).ToList();
					} else {
						if (FirstElement == null) id = int.MaxValue;
						else id = FirstElement.CardAccountID;
						CurrentList = services.GetPrevCardAccounts(id, AmountElements).ToList();
					}
				} else {
					CurrentList = services.GetNextCardAccounts(id-1, AmountElements).ToList();
				}
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
			using (var localrepos = new DAL.Repositories()) {
				Customers = localrepos.Customers.GetAll();					

				foreach (var el in CurrentList) {
					if (Customers.Count() > 0) {
						cust=Customers.FirstOrDefault(cs => cs.CustomerID == el.CustomerID);
					}
					EntitiesInformation.Add(StringSource.CardAccountAndClientShortInfo(el, cust));
					IdsEntities.Add(el.CardAccountID);
				}

			}

			Information = StringSource.Successfully();
			return true;

		}

	}
}
