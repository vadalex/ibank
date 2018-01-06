using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLL;
using BLL.ServiceReference1;
using UIControlLibrary.Interfaces;
using System.ComponentModel;

namespace IAdmin.AdminActions {
	/// <summary>
	/// Логика взаимодействия для ListDefaultCurrenciesAccountsControl.xaml
	/// </summary>
	public partial class ListDefaultCurrenciesAccountsControl : UserControl, IGetBLL {
		public ListDefaultCurrenciesAccountsControl() {
			InitializeComponent();
			Loaded += ListDefaultCurrenciesAccountsControl_Loaded;

		}

		void ListDefaultCurrenciesAccountsControl_Loaded(object sender, RoutedEventArgs e) {
			OpOp.FindBankAccount.BankAccountNumber = "1111111111111111";
			OpOp.FindBankAccount.Execute();
			Addedtodeflist();
			OpOp.FindBankAccount.BankAccountNumber = "1111111111111112";
			OpOp.FindBankAccount.Execute();
			Addedtodeflist();
			OpOp.FindBankAccount.BankAccountNumber = "1111111111111113";
			OpOp.FindBankAccount.Execute();
			Addedtodeflist();
			OpOp.FindBankAccount.BankAccountNumber = "1111111111111114";
			OpOp.FindBankAccount.Execute();
			Addedtodeflist();

			UIListDCA.ItemsSource = DefList;
		}

		protected List<string> DefList = new List<string>();

		protected void Addedtodeflist() {
			DefList.Add(
				DefListInfo(
					OpOp.FindBankAccount.BankAccountNumber,
					OpOp.FindBankAccount.CurrencyName,
					OpOp.FindBankAccount.CurrencyShortName,
					OpOp.FindBankAccount.Balance
				)
			);
		}

		protected string DefListInfo(string number,  string name,  string shname,  string balance) {
			return "Номер банк счета "+number+
				" баланс "+balance+
				" "+shname+" ("+name+")."
				;
		}


		public BLL.UILogic.RepositoryOperations OpOp {
			get {
				if ((Application.Current.MainWindow as UIControlLibrary.Interfaces.IGetBLL) != null)
					return (Application.Current.MainWindow as UIControlLibrary.Interfaces.IGetBLL).OpOp;
				else {
					return new BLL.UILogic.RepositoryOperations();
				}
			}
		}

	}
}
