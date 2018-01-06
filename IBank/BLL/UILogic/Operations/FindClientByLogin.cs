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
	public class FindClientByLogin:BaseOperation {

		private Customer Client { get; set; }
		public int IdClient { get; protected set; }

		public string PassportNumber { get; set; }
		public string Login { get; set; }


		public override bool Execute() {
			IdClient = -1;
			
			if (PassportNumber == null) {
				if (!Verifier.CheckLogin(Login)) {
					Information = StringSource.LoginStructureError();
					return false;
				}

				using (var localrepos = new DAL.Repositories()) {
					//////////////////////////////////////////////////////////////////////////
					var tempo = localrepos.Customers.GetAll();
					if (tempo != null && tempo.Count() > 0) {
						var tt = tempo.Where(el => el.Login == Login).ToList();
						Client = tt.Count() > 0 ? tt.ElementAt(0) : null;
					}
				}
			} else {
				if (!Verifier.CheckPassportNumber(PassportNumber)) {
					Information = StringSource.PassportNumberStructureError();
					return false;
				}

				using (var localrepos = new DAL.Repositories()) {
					if (localrepos.Customers.GetAll(el => el.PassportNumber == PassportNumber).Count() > 1) {
						Information = StringSource.MorePassportnumberError();
						return false;
					}
					//////////////////////////////////////////////////////////////////////////
					var tempo = localrepos.Customers.GetAll();
					if (tempo != null && tempo.Count() > 0) {
						var tt = tempo.Where(el => el.PassportNumber == PassportNumber).ToList();
						Client = tt.Count() > 0 ? tt.ElementAt(0) : null;
					}
				}

			}


			if (Client != null) {
				IdClient = Client.CustomerID;
				PassportNumber = Client.PassportNumber;
				Login = Client.Login;
				Information = StringSource.ClientInformation(Client);
				return true;
			} else {
				Information = StringSource.ClientNotFound();
				return false;
			}
		}

	}
}
