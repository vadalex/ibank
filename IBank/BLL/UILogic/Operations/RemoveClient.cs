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
	public class RemoveClient : BaseOperation {

		private Customer Client { get; set; }
		public int IdClient { get; set; }

		public override bool Execute() {
			if (IdClient <= 0) {
				Information = StringSource.ClientNotFound();
				return false;
			}

			using (var localerepos = new Repositories()) {

				Client = localerepos.Customers.GetSingle(IdClient);

				if (Client != null) {

					new Supporting().RemoveClientInformationById(IdClient,true);
					//localerepos.Customers.Remove(Client);
					//localerepos.SaveChanges();

					Information = StringSource.ClientRemoved();
					return true;
				} else {
					Information = StringSource.ClientNotFound();
					return false;
				}
			}
		}



	}
}
