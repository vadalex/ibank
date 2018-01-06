using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;

namespace BLL.UILogic.Operations {
	public class GetNBRBCurrencies:BaseOperation {

		private DataSet ExRatesDyn { get; set; }
		public List<string> Rates { get; set; }
		public List<string> ShortNames { get; set; }
		public List<string> Names { get; set; }
		public DateTime UpdateTime { get; set; }

		public override bool Execute() {
			using (var nbrb = new BLL.ServiceReference2.ExRatesSoapClient()) {

				UpdateTime = nbrb.LastDailyExRatesDate();
				var crtm = DateTime.Now;
				if (UpdateTime > crtm) {
					UpdateTime = crtm;
				}
				ExRatesDyn = nbrb.ExRatesDaily(UpdateTime);
				
			}

			var xmlstring = ExRatesDyn.GetXml();
			var xml = new System.Xml.XmlDocument();
			xml.LoadXml(xmlstring);
			XmlElement xel = xml.DocumentElement;

			XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
			nsmgr.AddNamespace("", "");
			var asd = xel.SelectNodes("DailyExRatesOnDate", nsmgr);

			ShortNames = new List<string>();
			Names = new List<string>();
			Rates = new List<string>();
			string abr;
			string rt;
			string nm;
			foreach (XmlElement el in asd) {
				abr = el.SelectSingleNode("Cur_Abbreviation").InnerText;
				if (DataSource.GetCurrenciesCusrseNBRB.Contains(abr)) {
					rt = el.SelectSingleNode("Cur_OfficialRate").InnerText;
					nm = el.SelectSingleNode("Cur_QuotName").InnerText;
					Rates.Add(rt);
					Names.Add(nm);
					ShortNames.Add(abr);
				}
			}
			Information = StringSource.CurrenciesInfo(Names, ShortNames, Rates, UpdateTime) + "\n";
			return true;
		}



	}
}
