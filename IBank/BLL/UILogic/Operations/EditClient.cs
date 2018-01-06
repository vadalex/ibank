using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ServiceReference1;
using Entities;
using DAL;

namespace BLL.UILogic.Operations {

	public class EditClient:BaseOperation {

		private Customer Client { get; set; }
		private AccessCard AccessCard { get; set; }

		public int IdClient { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string PassportNumber { get; set; }
		public string Country { get; set; }
		public string Address { get; set; }
		public string EMail { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public bool IsLocked { get; set; }


		public override bool Execute() {
			if (IdClient <= 0) {
				Information = StringSource.ClientNotFound();
				return false;
			}
			if (!Verifier.CheckPassword(Password)) {
				Information = StringSource.PasswordStructureError();
				return false;
			}
			if (!Verifier.CheckName(Country)) {
				Information = StringSource.NameStructureError();
				return false;
			}
			if (!Verifier.CheckAddress(Address)) {
				Information = StringSource.AddressStructureError();
				return false;
			}
			if (!Verifier.CheckPassportNumber(PassportNumber)) {
				Information = StringSource.PassportNumberStructureError();
				return false;
			}
			if (!Verifier.CheckName(FirstName)) {
				Information = StringSource.NameStructureError();
				return false;
			}
			if (!Verifier.CheckName(LastName)) {
				Information = StringSource.NameStructureError();
				return false;
			}
			if (!Verifier.CheckName(MiddleName)) {
				Information = StringSource.NameStructureError();
				return false;
			}
			if (!Verifier.CheckEMail(EMail)) {
				Information = StringSource.EMailStructureError();
				return false;
			}

			using (var localrepos = new Repositories()) {
				if (localrepos.Customers.GetAll(el => el.PassportNumber == PassportNumber&&el.CustomerID!=IdClient).Count() > 0) {
					Information = StringSource.ClientPassportNumberIsContains();
					return false;
				}

				Client=localrepos.Customers.GetSingle(IdClient);
				if (Client == null) {
					Information = StringSource.ClientNotFound();
					return false;
				}

				Client.FirstName = FirstName;
				Client.LastName = LastName;
				Client.MiddleName = MiddleName;
				Client.PassportNumber = PassportNumber;
				Client.Country = Country;
				Client.Address = Address;
				Client.Email = EMail;
				Client.Login = Login;
				Client.Passoword = Password;
				Client.IsLocked = IsLocked;
				//Client.CustomerID=

				localrepos.Customers.Update(Client);
				localrepos.SaveChanges();								
			}

			Information = StringSource.ClientEdited();
			return true;
		}

	


	}
}
