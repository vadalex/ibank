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
	public class AddOperator:BaseOperation {

		public string Password { get;set; }
		public string Login {get;set;}
		public string PasswordCheck{get;set;}

		private Employee Operator { get; set; }
		public int IdOperator { get; protected set; }


		public override bool Execute() {

			if(!Verifier.CheckLogin(Login)){
				Information = StringSource.LoginStructureError();
				return false;
			}
			if(!Verifier.CheckPassword(Password)||!Verifier.CheckPassword(PasswordCheck)){
				Information = StringSource.PasswordStructureError();
				return false;
			}
			if (Password != PasswordCheck) {
				Information = StringSource.PasswordNotIndentical();
				return false;
			}

			using (var bankservice = new BankServiceClient()) {
				Operator = bankservice.GetEmployeeByLogin(Login);
			}

			if (Operator != null) {
				Information = StringSource.LoginIsContains();
				return false;
			}

			using (var bankservice = new BankServiceClient()) {
				Operator = new Employee { Login = Login, Password = Password, IsAdmin = false };
				bankservice.AddEmployee(Operator);
			}
			IdOperator = Operator.EmployeeID;
			Information = StringSource.OperatorRegistred();

			return true;
		}
	}
}
