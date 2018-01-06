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
	public class CheckAutentificationOperator:BaseOperation {

		public string Login { get; set; }
		public string Password { get; set; }


		public override bool Execute() {
			if (!Verifier.CheckLogin(Login)) {
				Information = StringSource.LoginStructureError();
				return false;
			}
			if (!Verifier.CheckPassword(Password)) {
				Information = StringSource.PasswordStructureError();
				return false;
			}

			using (var bankService = new BLL.ServiceReference1.BankServiceClient()) {
				var user = bankService.GetEmployeeByLogin(Login);
				if (user == null || user.Password != Password) {
					Information = StringSource.CheckAutentificationFalse();
					return false;
				}
			}
			Information = String.Empty;
			return true;
		}
	}
}
