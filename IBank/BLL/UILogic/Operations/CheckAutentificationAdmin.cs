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
	public class CheckAutentificationAdmin:BaseOperation {

		public string Password { get; set; }
		public string Login { get; set; }

		public override bool Execute() {
			if (!Verifier.CheckLogin(Login)) {
				Information = StringSource.LoginStructureError();
				return false;
			}
			if (!Verifier.CheckPassword(Password)) {
				Information = StringSource.PasswordStructureError();
				return false;
			}

			Employee admin = null;
			using (var bankservice = new BLL.ServiceReference1.BankServiceClient()) {
				var admines = bankservice.GetAllAdminEmployees();
				if (admines != null && admines.Length > 0) {
					admines = admines.Where(el=>el.Login==Login).ToArray();
					if (admines.Length > 0) {
						admin = admines[0];
					}
				}
			}
			if (admin == null) {
				Information = StringSource.AdminNotFound();
				return false;
			}
			if (admin.Password != Password) {
				Information = StringSource.CheckAutentificationFalse();
				return false;
			}
			Information = string.Empty;
			return true;
		}

	}
}
