using BLL.ServiceReference1;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UILogic.Operations {
	public class RemoveBankAccount:BaseOperation {

		private BankAccount BankAccount { get; set; }
		public int IdBankAccount { get; set; }

		public override bool Execute() {
			if (IdBankAccount <= 0) {
				Information = StringSource.BankAccountNotFound();
				return false;
			}

			using (var service = new BankServiceClient()) {

				BankAccount = service.GetBankAccountById(IdBankAccount);

				if (BankAccount != null) {

					service.RemoveBankAccount(BankAccount);

					Information = StringSource.BankAccountRemoved();
					return true;
				} else {
					Information = StringSource.BankAccountNotFound();
					return false;
				}
			}

		}


	}
}
