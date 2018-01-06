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
	public class GetAllCurrencies:BaseOperation {

		public Currency[] AllCurrencies { get; set; }

		public List<string> CurrenciesNames { get; set; }
		public List<string> CurrenciesShortNames { get; set; }
		public List<string> CurrenciesIds { get; set; }
		public List<string> CurrenciesRates { get; set; }

		public override bool Execute() {
			using (var bankservice = new BankServiceClient()) {
				AllCurrencies= bankservice.GetAllCurrency();
			}

			CurrenciesNames = new List<string>();
			CurrenciesShortNames = new List<string>();
			CurrenciesIds=new List<string>();
			CurrenciesRates = new List<string>();

			foreach (var el in AllCurrencies) {
				CurrenciesIds.Add(el.CurrencyID.ToString());
				CurrenciesNames.Add(el.Name);
				CurrenciesRates.Add(el.Rate.ToString());
				CurrenciesShortNames.Add(el.ShortName);
			}

			return true;
		}
	}
}
