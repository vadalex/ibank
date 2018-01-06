using BLL.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UILogic.Operations {
	public class FindOperatorById:BaseOperation {

		public int IdOperator { get; set; }

		public Employee Operator { get; protected set; }

		public override bool Execute() {
			Operator = null;

			if (IdOperator <= 0) {
				Information = StringSource.OperatorNotFound();
				return false;
			}

			using (var service=new BankServiceClient()) {
				Operator = service.GetEmployeeById(IdOperator);
			}

			if (Operator != null) {
				Information = StringSource.OperatorInformation(Operator);
				return true;
			} else {
				Information = StringSource.OperatorNotFound();
				return false;
			}
		}

	}
}
