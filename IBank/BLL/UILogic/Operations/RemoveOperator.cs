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
	public class RemoveOperator:BaseOperation {

		private Employee Operator { get; set; }
		public int IdOperator { get; set; }

		public override bool Execute(){
			if (IdOperator <= 0) {
				Information = StringSource.OperatorNotFound();
				return false;
			}


			using (var bankservice = new BankServiceClient()) {
				Operator = bankservice.GetEmployeeById(IdOperator);
				if (Operator != null) {
					bankservice.RemoveEmployee(Operator);

					Information = StringSource.OperatorRemoved();
					return true;
				} else {
					Information = StringSource.OperatorNotFound();
					return false;
				}
			}
		} 

	}
}
