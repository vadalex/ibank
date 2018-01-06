using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using BLL.UILogic.Interfaces;

namespace BLL.UILogic {
	public class Verifier:IGetDataSources {

		private DataSources _DataSources;
		public DataSources DataSource {
			get {
				if (_DataSources == null) {
					_DataSources = new DataSources();
				}
				return _DataSources;
			}
			set { _DataSources = value; }
		}

		public bool CheckPassword(string password) {
			if (string.IsNullOrWhiteSpace(password)) {
				return false;
			}
			if (password.Length < DataSource.LengthPasswordMin || password.Length > DataSource.LengthPasswordMax) {
				return false;
			}
			foreach (var el in password) {
				if (!DataSource.PasswordTryeSymbols.Contains(el)) {
					return false;
				}
			}

			return true;
		}

		public bool CheckLogin(string login) {
			if (string.IsNullOrWhiteSpace(login)) {
				return false;
			}
			if (login.Length < DataSource.LengthLoginMin || login.Length > DataSources.LengthLoginMax) {
				return false;
			}
			foreach (var el in login) {
				if (!DataSource.LoginTryeSymbols.Contains(el)) {
					return false;
				}
			}

			return true;
		}

		public bool CheckBalance(string balance) {
			double vl;
			if (!string.IsNullOrWhiteSpace(balance) && double.TryParse(balance, out vl)) {

				if ((vl >= DataSource.BalanceMinValue) && (vl <= DataSource.BalanceMaxValue)) {
					return true;
				}

			}

			return false;
		}


		public bool CheckCurrencyCoef(string coef) {
			double vl;
			if (!string.IsNullOrWhiteSpace(coef) && double.TryParse(coef, out vl)) {
				if (vl >= DataSource.CoefMinValue && vl <= DataSource.CoefMaxValue) {
					return true;
				}
			}
			return false;
		}


		public bool CheckBankAccountNumber(string bancAccountNum) {
			if (bancAccountNum==null||bancAccountNum.Length != DataSource.LengthBankAccountNumber) {
				return false;
			}
			foreach (var el in bancAccountNum) {
				if (!DataSource.BankAccountNumberTryeSymbols.Contains(el)) {
					return false;
				}
			}

			return true;
		}

		public bool CheckCardAccountNumber(string cardAccountNum) {
			if (cardAccountNum==null||cardAccountNum.Length != DataSource.LengthCardAccountNumber) {
				return false;
			}
			foreach (var el in cardAccountNum) {
				if (!DataSource.CardAccountNumberTryeSymbols.Contains(el)) {
					return false;
				}
			}

			return true;
		}

		public bool CheckExpiredDate(DateTime date) {
			if (date != null && date > DateTime.Now+DataSource.CardAccountMinPeriod) {
				return true;
			}
			return false;
		}

		public bool CheckName(string name) {
			if (!string.IsNullOrWhiteSpace(name)) {
				foreach (var el in name) {
					if (!DataSource.NameTryeSymbols.Contains(el)) {
						return false;
					}
				}
				return true;
			}
			return false;
		}

		public bool CheckPassportNumber(string passportNumber) {
			if (!string.IsNullOrWhiteSpace(passportNumber)) {

				if (passportNumber.Length < DataSource.PassportNumberMinLength) {
					return false;
				}


				foreach (var el in passportNumber) {
					if (!DataSource.PassportTryeSymbols.Contains(el)) {
						return false;
					}
				}
				return true;
			}
			return false;
		}


		public bool CheckAddress(string address) {
			if (!string.IsNullOrWhiteSpace(address)) {

				foreach (var el in address) {
					if (!DataSource.AddressTryeSymbols.Contains(el)) {
						return false;
					}
				}
				return true;
			}
			return false;
		}

		public bool CheckEMail(string email) {
			if (!string.IsNullOrWhiteSpace(email)) {

				if (email == DataSource.NotEMail) {
					return true;
				}

				//var m1 = @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
				var m2=@"\b[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}\b";
				var regexp = new Regex(m2);
				if (regexp.IsMatch(email)) {
					return true;
				}

			}

			return false;
		}



		public bool CheckAmmountElements(int AmmountElements) {
			if (AmmountElements <= 0||AmmountElements>DataSource.MaxAmmountElements) {
				return false;
			}
			return true;
		}
	}
}
