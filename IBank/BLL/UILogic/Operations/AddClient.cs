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
	public class AddClient :BaseOperation{

		private Customer Client { get; set; }
		private AccessCard AccessCard { get; set; }

		public int IdClient { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string PassportNumber { get; set; }
		public string Country { get; set; }
		public string Address { get; set; }

		public string EMail { get; set; }


		public string Login { get; protected set; }

		public override bool Execute() {
			if (!Verifier.CheckName(Country)) {
				Information = StringSource.NameStructureError();
				return false;
			}
			if (!Verifier.CheckAddress(Address)) {
				Information = StringSource.AddressStructureError();
				return false;
			}
			if (!Verifier.CheckPassportNumber(PassportNumber)) {
				Information = StringSource.PassportNumberStructureError();
				return false;
			}
			if (!Verifier.CheckName(FirstName)) {
				Information = StringSource.NameStructureError();
				return false;
			}
			if (!Verifier.CheckName(LastName)) {
				Information = StringSource.NameStructureError();
				return false;
			}
			if (!Verifier.CheckName(MiddleName)) {
				Information = StringSource.NameStructureError();
				return false;
			}
			if (!Verifier.CheckEMail(EMail)) {
				Information = StringSource.EMailStructureError();
				return false;
			}
		
			Client = new Customer {
				FirstName = FirstName,
				LastName = LastName,
				MiddleName = MiddleName,
				PassportNumber = PassportNumber,
				Country = Country,
				Address=Address,
				Email=EMail,
				Login = LoginGeneration(),
				Passoword = PasswordGeneration(),
				IsLocked=false,
			};
			
			var gen=new Generator{ NumberCount=DataSource.LengthCode};
			using (var localrepos = new Repositories()) {
				var all = localrepos.Customers.GetAll();
				if (all.Where(el => el.PassportNumber == PassportNumber).Count()>0) {
					Information = StringSource.ClientPassportNumberIsContains();
					return false;
				}

				localrepos.Customers.Add(Client);
				localrepos.SaveChanges();
		////////////////////////////////////////////////////////////////////
				new Supporting().RemoveClientInformationById(Client.CustomerID);
		////////////////////////////////////////////////////////////////////
				if (all.Count() > 0) {
					var cl = all.Where(el => el.Login == Client.Login).ToList();
					if (cl.Count() > 0) {
						var cc = (Customer)cl.ElementAt(0);
						IdClient = cc.CustomerID;
					} else { Information = StringSource.ClientRegistredError(); return false; }
				} else { Information = StringSource.ClientRegistredError(); return false; }
				
				// зарегаем карту доступа
				AccessCard=new AccessCard{ 
					//Customer=Client, 
					CustomerID=Client.CustomerID, 
					EnteredCodeFail=0, 
					IsBlocked=false     ,
					 //AccessCodes=,
				};
				localrepos.AccessCards.Add(AccessCard);
				localrepos.SaveChanges();

				var acs=localrepos.AccessCards.GetAll();
				if(acs.Count()>0){
					var acs2=acs.Where(el=>el.CustomerID==Client.CustomerID).ToArray();
					if(acs2.Count()>0){
						AccessCard=acs2[0];
					} else { Information = StringSource.ClientRegistredError(); return false; }
				} else { Information = StringSource.ClientRegistredError(); return false; }

				for(int i=0; i<40;i++){
					localrepos.AccessCodes.Add(new AccessCode{ 
						//AccessCard=ac, 
						AccessCardID=AccessCard.AccessCardID, 
						Code=gen.NumberGenerate().ToString(),
						Number=i+1,
					});
				};
				localrepos.SaveChanges();
			}
			Login = Client.Login;
			Information = StringSource.ClientRegistred(Client.Login);
			return true;
		}

		public string LoginGeneration() {
			var login = "";
			var gn = new Generator { CharCount = DataSource.LengthLoginMin };
			while (true) {
				login = gn.StringGenerate();
				if (!new Supporting().IsLoginContains(login)) {
					return login;
				}
			}
		}
		public string PasswordGeneration() {
			return new Generator { CharCount = DataSource.LengthPasswordMin }.StringGenerate();
		}

	}
}
