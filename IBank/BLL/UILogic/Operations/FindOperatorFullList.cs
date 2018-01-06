using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.UILogic;
using BLL.ServiceReference1;

namespace BLL.UILogic.Operations {
	public class FindOperatorFullList : BaseOperationGetFullList<Employee> {

		public override bool Execute() {
			var isbs = base.Execute();
			if (!isbs) return false;

			int id = 0;

			using (var services = new BankServiceClient()) {
				id = LastElement == null ? 0 : LastElement.EmployeeID;
				if (!IsUpdate) {
					if (IsNext) {
						CurrentList = services.GetNextOperators(id, AmountElements).ToList();
					} else {
						if (FirstElement == null) id = int.MaxValue;
						else id = FirstElement.EmployeeID;
						CurrentList = services.GetPrevOperators(id, AmountElements).ToList();
					}
				} else {
					CurrentList = services.GetNextOperators(id-1, AmountElements).ToList();
				}
			}

			if (CurrentList != null && CurrentList.Count > 0) {
				FirstElement = CurrentList.First();
				LastElement = CurrentList.Last();
			} else {
				FirstElement = null;
				LastElement = null;
				Information = StringSource.EndList();
				return false;
			}

			foreach (var el in CurrentList) {
				EntitiesInformation.Add(
				StringSource.OperatorInformation(el));
				IdsEntities.Add(el.EmployeeID);
			}
			
			Information = StringSource.Successfully();
			return true;

		}

	}
}
