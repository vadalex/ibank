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
	public class AddCardAccount : BaseOperation {

		private Customer Client { get; set; }
		private BankAccount BankAccount { get; set; }
		private CardAccount CardAccount { get; set; }
		private string CartNumber { get; set; }

		public int IdClient { get; protected set; }
		public int IdBankAccount { get; set; }
		public int IdCardAccount { get; protected set; }

		public string CardAccountName { get; set; }
		public string CardType { get; set; }
		public string CardAccountStatus { get; set; }
		public DateTime ExpiredDate { get; set; }

		public override bool Execute() {
			if (!Verifier.CheckExpiredDate(ExpiredDate)) {
				Information = StringSource.ExpiredDateStructureError();
					return false;
			}
			if (!Verifier.CheckName(CardAccountName)) {
				Information = StringSource.NameStructureError();
				return false;
			}
			
			if (!Verifier.CheckName(CardType)) {
				Information = StringSource.NameStructureError();
				return false;
			}
			if (!Verifier.CheckName(CardAccountStatus)) {
				Information = StringSource.NameStructureError();
				return false;
			}
			
			

			var gen=new BLL.Generator(){ NumberCount=DataSource.LengthCardAccountNumber};
			CartNumber=string.Empty;
			using (var bankservices = new BLL.ServiceReference1.BankServiceClient()) {

				BankAccount=bankservices.GetBankAccountById(IdBankAccount);
				if (BankAccount == null) {
					Information = StringSource.BankAccountNotFound();
					return false;
				}

				using (var localrepos = new Repositories()) {
					Client=localrepos.Customers.GetSingle(BankAccount.CustomerID);
				}
				if (Client == null) {
					Information = StringSource.ClientNotFound();
					return false;
				}

				//if (BankAccount.CustomerID != IdClient) {
				//	Information=StringSource.BankAccountBelongsOtherCliente();
				//	return false;
				//}


				while(true){
					CartNumber=gen.NumberGenerate().ToString();
					if (bankservices.GetBankAccountByNumber(CartNumber) == null) {
						break;
					}
				}

				CardAccount = new CardAccount {
					
					BankAccountID = BankAccount.BankAccountID, 
					CreatedDate = DateTime.Now, 
					IsLocked = false,
					CustomerID = Client.CustomerID,
					CardNumber= CartNumber,
					///////////////////////////////////////////////////////////////////////
					Name= CardAccountName,
					IsDefault=false,
					CardType = CardType,
					ExpiredDate=ExpiredDate,
					//ExtensionData=new System.Runtime.Serialization.ExtensionDataObject(),
					Status = CardAccountStatus,
					/////////////////////////////////////////////////
				};

				bankservices.AddCardAccount(CardAccount);				

			}
			Information = StringSource.CardAccountRegistred(CardAccount.CardNumber);
			IdCardAccount = CardAccount.CardAccountID;
			IdClient = BankAccount.CustomerID;
			return true;
		}

	}
}
