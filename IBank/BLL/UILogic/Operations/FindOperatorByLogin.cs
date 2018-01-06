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
	public class FindOperatorByLogin : BaseOperation {

		public string Login { get; set; }

		private Employee Operator { get; set; }
		public int IdOperator { get; protected set; }

		public override bool Execute() {
			if (!Verifier.CheckLogin(Login)) {
				Information = StringSource.LoginStructureError();
				return false;
			}

			using (var bankservice = new BankServiceClient()) {
				Operator = bankservice.GetEmployeeByLogin(Login);
			}

			if (Operator != null) {
				Information = StringSource.OperatorInformation(Operator);
				IdOperator = Operator.EmployeeID;
				return true;
			} else {
				Information = StringSource.OperatorNotFound();
				return false;
			}
		}

	}
}
