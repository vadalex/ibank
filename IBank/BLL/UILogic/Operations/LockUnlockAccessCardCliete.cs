using BLL.ServiceReference1;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL.UILogic.Operations {
	public class LockUnlockAccessCardCliete:BaseOperation {

		private Customer Client { get; set; }
		private AccessCard AccessCard { get; set; }

		public int IdClient { get; set; }


		public override bool Execute() {
			if (IdClient <= 0) {
				Information = StringSource.ClientNotFound();
				return false;
			}

			Information = String.Empty;

			using (var localrepos = new Repositories()) {
				Client = localrepos.Customers.GetSingle(IdClient);
				if (Client == null) {
					Information = StringSource.ClientNotFound();
					return false;
				}

				AccessCard = Client.AccessCards.First();
				AccessCard.EnteredCodeFail = 0;
				AccessCard.IsBlocked = AccessCard.IsBlocked == true ? false : true;
				Client.IsLocked = AccessCard.IsBlocked;

				localrepos.AccessCards.Update(AccessCard);
				localrepos.Customers.Update(Client);
				
				localrepos.SaveChanges();
			}


			Information += AccessCard.IsBlocked == true ? StringSource.AccessCardLocked() : StringSource.AccessCardUnlocked();
			Information += "\n" + StringSource.ClientInformation(Client);
			return true;
		}


	}
}
