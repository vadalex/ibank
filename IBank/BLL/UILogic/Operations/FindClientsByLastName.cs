using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UILogic.Operations {
	public class FindClientsByLastName:BaseOperation {
				
		public string LastName { get; set; }
		public List<int> IdsClients { get; protected set; }
		public List<string> ClientsInformation { get; set; }

		public int AmmountElements { get; set; }

		private Customer FirstElement;
		private Customer LastElement;
		private List<Customer> CurrentList = new List<Customer>();
		public bool IsNext { get; set; }

		private IQueryable<Customer> Client { get; set; }
		
		protected void Clear() {
			IdsClients = new List<int>();
			ClientsInformation = new List<string>();
			Information = "";
		}


		public override bool Execute() {
			if (!Verifier.CheckName(LastName)) {
				Information = StringSource.NameStructureError();
				return false;
			}
			if (!Verifier.CheckAmmountElements(AmmountElements)) {
				Information = StringSource.AmmountElementsStructureError();
				return false;
			}
			Clear();
			int id=0;
			using (var localrepos = new DAL.Repositories()) {
				if(IsNext) {
					if (LastElement == null) id = 0;
					else id = LastElement.CustomerID;
					Client = localrepos.Customers.GetAll(el => el.LastName == LastName&&el.CustomerID>id);
					CurrentList = Client.Take(AmmountElements).ToList();
				} else {
					if (FirstElement == null) id = int.MaxValue;
					else id = FirstElement.CustomerID;
					Client = localrepos.Customers.GetAll().OrderByDescending(el=>el.CustomerID).
						Where(el => el.LastName == LastName && el.CustomerID < id);
					CurrentList = Client.Take(AmmountElements).OrderBy(el => el.CustomerID).ToList();
				}
				
			}

			if (CurrentList != null&&CurrentList.Count>0) {
				FirstElement = CurrentList.First();
				LastElement = CurrentList.Last();
			} else {
				FirstElement = null;
				LastElement = null;
				Information = StringSource.EndList();
				return false;
			}

			foreach (var el in CurrentList) {
				ClientsInformation.Add(StringSource.ClientInformation(el));
				IdsClients.Add(el.CustomerID);
			}

			Information = StringSource.Successfully();
			return true;

		}

	}
}
