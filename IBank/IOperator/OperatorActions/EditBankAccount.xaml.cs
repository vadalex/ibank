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
using IOperator.Path;
using System.ComponentModel;

namespace IOperator.OperatorActions {
	/// <summary>
	/// Логика взаимодействия для EditBankAccount.xaml
	/// </summary>
	public partial class EditBankAccount : UserControl,IGetBLL,IDataErrorInfo {
		public EditBankAccount() {
			InitializeComponent();
		}

		private void UIButtonRemoveBankAccount_Click(object sender, RoutedEventArgs e) {
			OpOp.IncreaceBalanceBankAccount.Balance = Balance;
			OpOp.IncreaceBalanceBankAccount.IdBankAccount = UIFindBankAccount.IdBankAccount;
			if(OpOp.IncreaceBalanceBankAccount.Execute()){
				UIFindBankAccount.BankAccountNumber = OpOp.IncreaceBalanceBankAccount.BankAccountNumber;
				UIFindBankAccount.UpdateFullList();
				UIFindBankAccount.UIButtonFind_Click(null, null);
				UIBalance.Text = "";
			}
			UIMessage.TextMessage = OpOp.IncreaceBalanceBankAccount.Information;
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


		public string Balance { get; set; }

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
					
				}
				return error;
			}
		}


	}
}
