using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using BLL.ServiceReference1;
using BLL.UILogic.Interfaces;

namespace BLL.UILogic {
	public class StringSources:IGetDataSources {

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

		public string NoSelectedCurrency() {
			return "Необходимо выбрать валюту.";
		}

		public string BalanceStructureError() {
			return "Величина баланса может быть от "+DataSource.BalanceMinValue+" до "+DataSource.BalanceMaxValue+".";
		}


		public string HandlAllExeptions() {
			return "Непредвиденная ошибка. Обратитесь в службу поддержки.";
		}


		public string BankAccountNotFound() {
			return "Банковский счет не найден.";
		}

		public string BankAccountAndClientInfo(BankAccount bankaccount, Customer client, Currency currency) {
			if (bankaccount == null) return BankAccountNotFound();

			if (client == null) {
				return BankAccountInformation(bankaccount,currency);
			}

			return BankAccountInformation(bankaccount, currency) + "\nПринадлежет клиенту:\n" + ClientInformation(client);
		}

		public string BankAccountInformation(BankAccount bankaccount,Currency currency) {
			if (bankaccount == null) return BankAccountNotFound();
			if (currency == null) return CurrencyNotFound();

			return "Банковский счет номер " + bankaccount.AcountNumber+
				"- баланс составляет "+bankaccount.Balance+
				" "+currency.ShortName+" ("+currency.Name+")."
				;
		}

		public string BankAccountNoCorrectNumber() {
			return "Введите номер счета в банке корректно.";
		}

		public string CardAccountNoCorrectNumber() {
			return "Введите номер карт-счета корректно.";
		}

		public string CardAccountAndClientShortInfo(CardAccount cardaccount, Customer client) {
			return "Карт-счет номер " + cardaccount.CardNumber+ 
				", с наименованием: "+cardaccount.Name+
				".\nТип карт-счета: "+cardaccount.CardType+
				".\nКарт-счет " + (cardaccount.IsLocked == true ? "заблокирован" : "не заблокирован") +
				".\nДата окончания срока использования " + cardaccount.ExpiredDate +
				".\nСтатус: " + cardaccount.Status.ToLower() + ///////////////////////////////////////////////////////////////////////////////////////////////
				"\n" + ClientInformation(client);
		}

		public string CardAccountInformation(CardAccount el, BankAccount bankAccount, Currency currency) {
			return 
				"Номер карт-счета "+el.CardNumber+
						".\nНаименование валюты " + currency.ShortName +" ("+currency.Name+")"+
						".\nСредства  составляют " + bankAccount.Balance +
						//".\nКарт-счет " + (el.IsLocked== true ? DataSource.CardStatusLocked : DataSource.CardStatusUnLocked) +
						".\nДата создания " + el.CreatedDate +
						".\nДата окончания срока использования " + el.ExpiredDate +
						".\nСтатус " + el.Status.ToLower() +
						".\nТип карт-счета - " + el.CardType +
						".\n\n"
			 ;
		}

		public string CardAccountRegistred(string cardNumber) {
			return "Карт-счет " + cardNumber + " успешно зарегистрирован.";
		}

		public string CardAccountRemoved(string cardNumber) {
			return "Карт-счет номер " + cardNumber + " успешно удален.";
		}

		public string CardAccountNotFound() {
			return "Карт-счет не найден.";
		}

		public string CardAccountLocked() {
			return "Карт-счет заблокирован.";
		}

		public string CardAccountUnlocked() {
			return "Карт-счет действителен.";
		}

		public string AccessCardLocked() {
			return "Учетная запись клиента заблокирована.";
		}

		public string AccessCardUnlocked() {
			return "Учетная запись клиента действительна.";
		}

		public string ClientRegistred(string login) {
			return "Пользователь " + login + " успешно зарегистрирован.";
		}

		public string ClientRegistredError() {
			return "При регистрации клиента произошла ошибка.";
		}

		public string ClientPassportNumberIsContains() {
			return "Такой номер паспорта уже зарегистрирован.";	
		}

		public string MorePassportnumberError() {
			return "Существует несколько паспортов с таким номером.";
		}

		public string ClientNotFound() {
			return "Клиент не найден."; 
		}

		public string AccessCardInformation(AccessCard accessCard) {
			var info = new StringBuilder();
			if (accessCard != null) {
				info.Append("Неудачных попыток входа: ");
				info.Append(accessCard.EnteredCodeFail);
				info.Append(".\nКарта доступа ");
				info.Append(accessCard.IsBlocked?"заблокирована.":"не заблокирована.");
				if(accessCard.AccessCodes!=null&&accessCard.AccessCodes.Count>0){
					info.Append("\nКоды доступа:\n");
					for (int i = 0,nn=0; i < accessCard.AccessCodes.Count; i++,nn++) {
						info.Append(accessCard.AccessCodes.ElementAt(i).Number);
						info.Append(". ");
						info.Append(accessCard.AccessCodes.ElementAt(i).Code);
						info.Append("  \t");
						if (nn >= 3) {
							nn = -1;
							info.Append("\n");
						}
					}
				} else {
					//info.Append("\nКоды доступа не обнаружены.");
				}
			} else {
				info.Append("Карта доступа не обнаружена.");
			}
			return info.ToString();
		}

		public string ClientInformation(Customer client,bool viewCodes=false) {
			var info = new StringBuilder();
			info.Append("Информация о клиенте (на " + DateTime.Now + ")\n");
			info.Append(ClientShortInfo(client));
			if(client!=null) info.Append("\nПароль "+client.Passoword);
			AccessCard ac=null;
			if (viewCodes&&client.AccessCards != null && client.AccessCards.Count > 0) {
				ac = client.AccessCards.First();
				info.Append(".\n\nИнформация о карт-счете клиента: \n");
				info.Append(AccessCardInformation(ac));
				info.Append("\nКарт-счета клиента:\n");
			}
				
			return info.ToString();
		}
		public string ClientShortInfo(Customer client) {
			if (client == null) return ClientNotFound();
			return
				  "\nЛогин " + client.Login +
				  ".\nФ.И.О. " + client.LastName + " " + client.FirstName + " " + client.MiddleName +
				  ".\nСтрана "+client.Country+
				  ".\nИдентификационный номер паспорта " + client.PassportNumber+
				  ".\nАдрес прописки " + client.Address +
				  ".\nЭлектронная почта " + client.Email+
				  ".\nУчетная запись клиента " + (client.IsLocked?DataSource.CardStatusLocked:DataSource.CardStatusUnLocked)+
				  "."
			  ;
		}

		public string CheckAutentificationFalse() {
			return "Неверный логин или пароль.";
		}

		public string AdminNotFound() {
			return "Учетная запись администратора не найдена.";
		}

		public string OperatorNotFound() {
			return "Оператор не найден.";
		}

		public string OperatorInformation(Employee oper) {
			return  "Данные оператора: " +
				  "\nЛогин " + oper.Login+", пароль "+oper.Password+".";
		}

		public string OperatorRemoved() {
			return "Оператор успешно удален.";
		}

		public string OperatorRegistred() {
			return "Оператор успешно зарегистрирован.";
		}

		public string ClientRemoved() {
			return "Клиент успешно удален.";
		}

		public string NameStructureError() {
			var rs = new StringBuilder();
			rs.Append("Наименование может содержать символы:\n");
			foreach (var el in DataSource.NameTryeSymbols) {
				rs.Append(" ");
				rs.Append(el);
			}
			return rs.ToString();
		}

		public string PassportNumberStructureError() {
			var rs = new StringBuilder();
			rs.Append("Длина номера паспорта должна составлять не менее ");
			rs.Append(DataSource.PassportNumberMinLength);
			rs.Append(" символов.\n");
			rs.Append("Номер паспорта может содержать только символы:\n");
			foreach (var el in DataSource.PassportTryeSymbols) {
				rs.Append(" ");
				rs.Append(el);
			}
			return rs.ToString();
		}

		public string LoginStructureError() {
			var rs = new StringBuilder();

			rs.Append("Длина логина должна составлять от ");
			rs.Append(DataSource.LengthLoginMin);
			rs.Append(" до ");
			rs.Append(DataSources.LengthLoginMax);
			rs.Append(" символов \nи может содержать только символы:\n");
			foreach (var el in DataSource.LoginTryeSymbols) {
				rs.Append(" ");
				rs.Append(el);
			}

			return rs.ToString();
		}

		public string ExpiredDateStructureError() {
			return "Небходимо корректно ввести дату со сроком от текущего времени.";
		}

		public string CurrencyCoefStructureError() {


			return "Неправильно введен коэффициент.";
		}

		public string BankAccountNumberStructureError() {
			var rs = new StringBuilder();

			rs.Append("Номер банковского счета должен составлять ");
			rs.Append(DataSource.LengthBankAccountNumber);
			rs.Append(" символов и может содержать только символы: \n\t ");
			foreach (var el in DataSource.BankAccountNumberTryeSymbols) {
				rs.Append(" ");
				rs.Append(el);
			}

			return rs.ToString();
		}
		public string CardAccountNumberStructureError() {
			var rs = new StringBuilder();

			rs.Append("Номер карт-счета должен составлять ");
			rs.Append(DataSource.LengthBankAccountNumber);
			rs.Append(" символов и может содержать только: ");
			foreach (var el in DataSource.CardAccountNumberTryeSymbols) {
				rs.Append(" ");
				rs.Append(el);
			}

			return rs.ToString();
		}

		public string PasswordStructureError() {
			var rs = new StringBuilder();

			rs.Append("Длина пароля должна составлять от ");
			rs.Append(DataSource.LengthPasswordMin);
			rs.Append(" до ");
			rs.Append(DataSource.LengthPasswordMax);
			rs.Append(" символов \nи может содержать только символы:\n");
			foreach (var el in DataSource.PasswordTryeSymbols) {
				rs.Append(" ");
				rs.Append(el);
			}

			return  rs.ToString();
		}

		public string PasswordNotIndentical() {
			return "Пароли не идентичны.";
		}

		public string PasswordError() {
			return "Неверный пароль.";
		}

		public string LoginIsContains() {
			return "Такой логин уже существует.";
		}

		public string NoCorrectId() {
			return "Некорректно введен Id.";
		}
		
		public string EditedIsTrue() {
			return "Изменения успешно внесены.";
		}

		public string BankAccountCreated(BankAccount bankaccount) {
			return "Банковский счет номер " + bankaccount.AcountNumber + " успешно создан.";
		}

		public string CurrencyNotFound() {
			return "Валюта не найдена.";	
		}

		public string CurrenciesInfo(List<string> names, List<string> shortnames, List<string> rates,DateTime date) {
			var rs = new StringBuilder();
			rs.Append("Национальный курс белорусского рубля по отношению к иностранным валютам, \nустанавлеваемый Национальным банком Республики Беларусь на ");
			rs.Append(date.ToLocalTime());
			rs.Append(".");
			for(int i=0; i<names.Count;i++){
				rs.Append("\n");
				var nm = names.ElementAt(i).ToArray();
				nm[0] = ' ';
				var nm2 = nm.Concat(nm);
				rs.Append(names.ElementAt(i));
				rs.Append(" (");
				rs.Append(shortnames.ElementAt(i));
				rs.Append(") состовляет ");
				rs.Append(rates.ElementAt(i));
				rs.Append(" бел. руб.");
			}
			rs.Append("\nДанные получены веб-сервисом http://www.nbrb.by/Services/ExRates.asmx.");

			return rs.ToString();
		}


		public string AddressStructureError() {
			var rs = new StringBuilder();

			rs.Append("Адрес может содержать только символы:\n ");
			foreach (var el in DataSource.AddressTryeSymbols) {
				rs.Append(" ");
				rs.Append(el);
			}

			return rs.ToString();
		}

		public string EMailStructureError() {
			return "Неверно введен электронный адрес почты.";
		}

		public string BankAccountRemoved() {
			return "Банк-счет успешно удален.";
		}

		public string BankAccountBalanceUp() {
			return "Баланс успешно пополнен.";
		}

		public string Successfully() {
			return "Операция успешно завершена.";
		}

		public string AmmountElementsStructureError() {
			return "Значение количества элементов должно быть положителным. И может содержать не более "+DataSource.MaxAmmountElements+" элементов.";
		}

		public string EndList() {
			return "Конец списка.";
		}

		public string BankAccountEdited() {
			return "Банковский счет  изменен.";
		}

		public string BankAccountNumberIsContains() {
			return "Такой номер банк-счета уже существует.";
		}

		public string CardAccountEdited() {
			return "Карт-счет изменен.";
		}

		public string ClientEdited() {
			return "Данные клиента изменены.";
		}

		public string OperatorEdited() {
			return "Оператор изменен.";
		}

		public string BankAccountBelongsOtherCliente() {
			return "Банк-счет принадлежит другому клиенту.";
		}

		public string YouWantToRegistredBankAccount() {
			return "Нужно зарегистрировать банк-счет?";
		}

		public string YouWantToNextRegistredCardAccount() {
			return "Нужно еще зарегистрировать другой карт-счет?";
		}
	}
}
