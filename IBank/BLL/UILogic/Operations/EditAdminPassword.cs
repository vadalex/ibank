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
	public class EditAdminPassword:BaseOperation {

		public string Password { get; set; }
		public string NewPassword { get; set; }
		public string NewPasswordCheck { get; set; }

		public string Login { get; set; }

		public override bool Execute() {
			if (!Verifier.CheckLogin(Login)) {
				Information = StringSource.LoginStructureError();
				return false;
			}
			if (!Verifier.CheckPassword(Password) || !Verifier.CheckPassword(NewPassword)
				|| !Verifier.CheckPassword(NewPasswordCheck)) {
				Information = StringSource.PasswordStructureError();
				return false;
			}

			if (NewPassword != NewPasswordCheck) {
				Information = StringSource.PasswordNotIndentical();
				return false;
			}

			Employee admin = null;
			using (var bankservice = new BankServiceClient()) {
				var admines = bankservice.GetAllAdminEmployees();
				if (admines != null && admines.Length > 0) {
					admines = admines.Where(el => el.Login == Login).ToArray();
					if (admines.Length > 0) {
						admin = admines[0];
					}
				}

				if (admin == null) {
					Information = StringSource.AdminNotFound();
					return false;
				}
				
				admin.Password = NewPassword;
				bankservice.UpdateEmployee(admin);
			}

			Information = StringSource.EditedIsTrue();
			return true;
		}
	}
}
