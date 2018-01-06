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
	public class EditCurrencies:BaseOperation{

		public List<string> CurrenciesRates { get; set; }
		public List<string> CurrenciesId { get; set; }
		public List<string> CurrenciesNames { get; set; }
		public List<string> CurrenciesShortNames { get; set; }

		public override bool Execute() {

			foreach (var el in CurrenciesNames) {
				if (!Verifier.CheckName(el)) {
					Information = StringSource.NameStructureError();
					return false;
				}
			}
			foreach (var el in CurrenciesShortNames) {
				if (!Verifier.CheckName(el)) {
					Information = StringSource.NameStructureError();
					return false;
				}
			}
			foreach (var el in CurrenciesRates) {
				if (!Verifier.CheckCurrencyCoef(el)) {
					Information = StringSource.CurrencyCoefStructureError();
					return false;
				}
			}


			var currencies = new List<Currency>();
			int idd = -1;
			for (int i = 0; i < CurrenciesRates.Count; i++,idd=-1) {
				if (!int.TryParse(CurrenciesId.ElementAt(i),out idd)) {
					idd = -1;
				}
				currencies.Add(new Currency {
					CurrencyID = idd,
					Name = CurrenciesNames.ElementAt(i),
					ShortName = CurrenciesShortNames.ElementAt(i),
					Rate = double.Parse(CurrenciesRates.ElementAt(i)),
				});
			}
			using (var bankaccoun = new BankServiceClient()) {
				//bankaccoun.RemoveAllCurrency();
				var oldcurr = bankaccoun.GetAllCurrency();
				foreach (var el in oldcurr) {
					if (currencies.Where(vl=>vl.CurrencyID==el.CurrencyID).Count()<=0) {
						bankaccoun.RemoveCurrency(el);
					}
				}

				foreach (var el in currencies) {
					if (el.CurrencyID == -1) {
						bankaccoun.AddCurrency(el);
					} else {
						bankaccoun.UpdateCurrency(el);
					}
				}
			}

			Information = StringSource.EditedIsTrue();
			return true;
		}
	}
}
