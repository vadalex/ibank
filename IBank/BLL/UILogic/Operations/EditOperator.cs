using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ServiceReference1;

namespace BLL.UILogic.Operations {
	public class EditOperator:BaseOperation {		

		private Employee Operator { get; set; }
		public string Password { get;set; }
		public string Login {get;set;}
		public string PasswordCheck{get;set;}
		public int IdOperator { get; set; }

		public override bool Execute() {
			if(IdOperator<=0){
				Information=StringSource.OperatorNotFound();
				return false;
			}
			if(!Verifier.CheckLogin(Login)){
				Information = StringSource.LoginStructureError();
				return false;
			}
			if(!Verifier.CheckPassword(Password)){
				Information = StringSource.PasswordStructureError();
				return false;
			}


			using (var bankservice = new BankServiceClient()) {
				Operator = bankservice.GetEmployeeById(IdOperator);
			
				if (Operator == null) {
					Information = StringSource.OperatorNotFound();
					return false;
				}
			
				Operator.Login=Login;
				Operator.Password=Password;
				//Operator.IsAdmin=Is	
			
				bankservice.UpdateEmployee(Operator);
			}

			Information = StringSource.OperatorEdited();
			return true;
		}
	}

	
}
