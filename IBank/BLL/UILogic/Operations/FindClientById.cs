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
	public class FindClientById:BaseOperation {

		public int IdClient { get;  set; }

		public Customer Client { get; protected set; }

		public override bool Execute() {
			Client = null;

			if (IdClient<=0) {
				Information = StringSource.ClientNotFound();
				return false;
			}

			using (var localrepos = new DAL.Repositories()) {
				Client = localrepos.Customers.GetSingle(IdClient);
			}

			if (Client != null) {
				Information = StringSource.ClientInformation(Client);
				return true;
			} else {
				Information = StringSource.ClientNotFound();
				return false;
			}
		}

	}
}
