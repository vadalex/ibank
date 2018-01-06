using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UILogic {
	public class DataSources {

		private Verifier _Verifier;
		public Verifier Verifier {
			get {
				if (_Verifier == null) {
					_Verifier = new Verifier();
				}
				return _Verifier;
			}
			set { _Verifier = value; }
		}

		public int MaxLengthStringTextMessage{get{return 100;}}
		public int LengthCode { get { return 4; } }
		public int LengthLoginMin { get { return 8; } }
		public static int LengthLoginMax { get { return 32; } }
		public int LengthPasswordMin { get { return 8; } }
		public int LengthPasswordMax { get { return 32; } }
		public double BalanceMinValue { get { return 0; } }
		public double BalanceMaxValue { get { return double.MaxValue; } }
		public double CoefMinValue { get { return 0; } }
		public double CoefMaxValue { get { return double.MaxValue; } }

		public List<char> LoginTryeSymbols { get { return 
			@"QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890-_".ToList(); } }

		public int PassportNumberMinLength { get { return 4; } }
		public List<char> PassportTryeSymbols {		get {		return
					@"QWERTYUIOPASDFGHJKLZXCVBNM1234567890".ToList();
			}
		}
		public List<char> PasswordTryeSymbols { get { return
			@"QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890".ToList();
			}
		}
		public List<char> NameTryeSymbols { get { return 
			@"ЁёЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮйцукенгшщзхъфывапролджэячсмитьбюQWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890- "
			.ToList(); } }
		public List<char> AddressTryeSymbols {
			get {
				return 
			@"ЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮйцукенгшщзхъфывапролджэячсмитьбюQWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890- .,"
			.ToList(); } }

		public int LengthBankAccountNumber { get { return 16; } }
		public List<char> BankAccountNumberTryeSymbols { get { return @"1234567890".ToList(); } }
		public int LengthCardAccountNumber { get { return 16; } }
		public List<char> CardAccountNumberTryeSymbols { get { return @"1234567890".ToList(); } }

		public TimeSpan CardAccountMinPeriod { get { return new TimeSpan(30,0,0,0,0); } }
		public TimeSpan CardAccountDefaultPeriod { get { return new TimeSpan(730, 0, 0, 0, 0); } }

		public List<string> LandNames { get {
				var cultureList = new List<string>();
				CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
				
				foreach (CultureInfo culture in cultures) {
					try {
						RegionInfo region = new RegionInfo(culture.LCID);
						if (!(cultureList.Contains(region.DisplayName)) ) {
							if (Verifier.CheckName(region.DisplayName)) {
								cultureList.Add(region.DisplayName);
							} 
							//else {
							//	cultureList.Add(region.EnglishName);
							//}
						}
					} catch (ArgumentException) {
						continue;
					}
				}
				cultureList.Sort();

				return cultureList;
			//return new List<string> {"Республика Беларусь","Российская Федерация" }; 
		
		} }
		public List<string> CardAccountTypes { get { return new List<string> { "MasterCard", "VISA", "VISA Classic", "VISA Gold", "Maestro", "Белкарт" }; } }
		public List<string> CardAccountStatuses { get { return new List<string> { "действителен" }; } }
		public string CardStatusLocked { get { return "Заблокирована"; } }
		public string CardStatusUnLocked { get { return "Действительна"; } }


		public List<string> CardAccountDefaultNames { get { return new List<string> { "Мой счет" }; } }

		public List<string> GetCurrenciesCusrseNBRB { get { return new List<string> { "USD","EUR","RUB" }; } }

		public string NotEMail { get { return "нет электронной почты"; } }



		public int MaxAmmountElements { get { return 100; } }
		public int DefaultAmmountElements { get { return 5; } }

		public DateTime CardAccountDefaultExpiredDate { get { return DateTime.Now.Add(CardAccountDefaultPeriod); } }
		public DateTime CardAccountMinExpiredDate { get { return DateTime.Now.Add(CardAccountMinPeriod); } }
	}
}
