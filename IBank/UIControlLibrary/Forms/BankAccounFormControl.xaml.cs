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

using UIControlLibrary.Components;
using BLL;
using UIControlLibrary.Interfaces;
using System.ComponentModel;
using BLL.ServiceReference1;


namespace UIControlLibrary.Forms {
	/// <summary>
	/// Логика взаимодействия для BankAccounFormControl.xaml
	/// </summary>
	public partial class BankAccounFormControl : UserControl,IForm {
		public BankAccounFormControl() {
			InitializeComponent();
		}

		static BankAccounFormControl() {
			BankAccountNumberProperty=DependencyProperty.Register("BankAccountNumber",typeof(string),typeof(BankAccounFormControl));
			BalanceProperty = DependencyProperty.Register("Balance", typeof(string), typeof(BankAccounFormControl));
		}
		public static readonly DependencyProperty BalanceProperty;
		public static readonly DependencyProperty BankAccountNumberProperty;

		
		public void CollapsedBankAccountNumber() {
			UIBankAccountNumber.Visibility = Visibility.Collapsed;
			UIBankAccountNumberTitle.Visibility = Visibility.Collapsed;
		}

		public void CollapsedBankAccountAdminEdition() {
			CollapsedBankAccountNumber();
			UIBalanceTB.Visibility = Visibility.Collapsed;
			UIBalance.Visibility = Visibility.Collapsed;
		}

		public void ClearUI() {
			UIBalance.Clear();
			UICurrencies.ClearUI();
			UIBankAccountNumber.Clear();
		}

		public int IdCurrency { get { return UICurrencies.IdCurrency; } set { UICurrencies.SelectCurrencyById(value); } }


		public string Balance { get { return (string)GetValue(BalanceProperty); } set { SetValue(BalanceProperty, value); } }
		public string BankAccountNumber { get { return (string)GetValue(BankAccountNumberProperty); } set { SetValue(BankAccountNumberProperty, value); } }

		public string Error {
			get { return "Error"; }
		}

		public string this[string columnName] {
			get {
				string error = String.Empty;
				switch (columnName) {
					case "Balance":
						if (!OpOp.Verifier.CheckBalance(Balance)) {
							error = OpOp.StringSource.BalanceStructureError();
						}
						break;
					case "BankAccountNumber":
						if (!OpOp.Verifier.CheckBankAccountNumber(BankAccountNumber)) {
							error = OpOp.StringSource.BankAccountNumberStructureError();
						}
						break;
				}
				return error;
			}
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
