using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UILogic.Operations {
	public class FindClientFullList:BaseOperationGetFullList<Customer> {

		
		public override bool Execute() {
			var isbs = base.Execute();
			if (!isbs) return false;

			int id = 0;
			using (var localrepos = new DAL.Repositories()) {
				id=LastElement == null?0:LastElement.CustomerID;
				if (!IsUpdate) {
					if (IsNext) {
						Entities = localrepos.Customers.GetAll(el => el.CustomerID > id);
						CurrentList = Entities.Take(AmountElements).ToList();
					} else {
						id =FirstElement == null?int.MaxValue:FirstElement.CustomerID;
						Entities = localrepos.Customers.GetAll().OrderByDescending(el => el.CustomerID).
							Where(el => el.CustomerID < id);
						CurrentList = Entities.Take(AmountElements).OrderBy(el => el.CustomerID).ToList();
					}
				} else {
					Entities = localrepos.Customers.GetAll().Where(el => el.CustomerID >= id);
					CurrentList = Entities.Take(AmountElements).ToList();
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

			foreach (var el in CurrentList) {
				EntitiesInformation.Add(StringSource.ClientInformation(el,false));
				IdsEntities.Add(el.CustomerID);
			}

			Information = StringSource.Successfully();
			return true;

		}

	}
}
