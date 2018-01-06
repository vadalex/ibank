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
	public class AddBankAccount:BaseOperation {

		public string Balance { get; set; }
		public int IdCurrency { get; set; }
		public int IdCustomer { get; set; }

		private string Password { get; set; }
		public string AccountNumber { get; protected set; }

		private BankAccount BankAccount { get; set; }
		private Customer Client { get; set; }
		private Currency Currency { get; set; }


		public override bool Execute() {
			if (IdCustomer <= 0) {
				Information = StringSource.ClientNotFound();
				return false;
			}
			if (IdCurrency <= 0) {
				Information = StringSource.CurrencyNotFound();
				return false;
			}
			if (!Verifier.CheckBalance(Balance)) {
				Information = StringSource.BalanceStructureError();
				return false;
			}

			AccountNumber=string.Empty;
			var gen = new BLL.Generator { NumberCount=DataSource.LengthBankAccountNumber, 
				CharCount=DataSource.LengthPasswordMin };
			Password = gen.StringGenerate();
			BankAccount = null;
			using (var bankservice = new BankServiceClient()) {
				
				while(true){
					AccountNumber = gen.NumberGenerate().ToString();
					if(bankservice.GetBankAccountByNumber(AccountNumber)==null){
						break;
					}
				}
				
				 BankAccount = new BankAccount {
					Balance = double.Parse(Balance),
					IsLocked = false,
					CreatedDate = DateTime.Now,
					CurrencyID = IdCurrency,
					AcountNumber = AccountNumber,
					Password=Password,
					CustomerID=IdCustomer,
					//	ExtensionData=,
					
				};
				
				bankservice.AddBankAccount(BankAccount);

				Currency=bankservice.GetCurrencyById(BankAccount.CurrencyID);

			}

			using (var locale = new Repositories()) {
				Client=locale.Customers.GetSingle(BankAccount.CustomerID);
			}


			Information = StringSource.BankAccountCreated(BankAccount);
			Information +="\n"+ StringSource.BankAccountAndClientInfo(BankAccount, Client, Currency);
			return true;
		}





	}
}
