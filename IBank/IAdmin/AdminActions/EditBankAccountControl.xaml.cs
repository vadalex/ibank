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
	/// Логика взаимодействия для EditBankAccountControl.xaml
	/// </summary>
	public partial class EditBankAccountControl : UserControl,IGetBLL,IClearUI {

		public void CollapsedCreate(){
			UIFindAB.Visibility = Visibility.Collapsed; ;
			UIBankAccountForm.CollapsedBankAccountNumber();

			UIEditBt.Visibility = Visibility.Collapsed;
			UIButtonRemoveBankAccount.Visibility = Visibility.Collapsed; 
		}

		public void CollapsedRemove() {
			UIFindClient.Visibility = Visibility.Collapsed; ;
			UIBankAccountForm.Visibility = Visibility.Collapsed;
			
			UIButtonAddBA.Visibility = Visibility.Collapsed; ;
			UIEditBt.Visibility = Visibility.Collapsed; 
		}

		public void CollapsedEdit() {
			UIButtonAddBA.Visibility = Visibility.Collapsed; 
			UIButtonRemoveBankAccount.Visibility = Visibility.Collapsed; 
		}


		public EditBankAccountControl() {
			InitializeComponent();
			UIFindAB.Finded = new Action(Finded);
		}

		public void Finded() {
			OpOp.FindBankAccountById.IdBankAccount=UIFindAB.IdBankAccount;
			if(OpOp.FindBankAccountById.Execute()&&OpOp.FindBankAccountById.BankAccount!=null){
				UIBankAccountForm.Balance= OpOp.FindBankAccountById.BankAccount.Balance.ToString();
				UIBankAccountForm.BankAccountNumber = OpOp.FindBankAccountById.BankAccount.AcountNumber.ToString();
				if(OpOp.FindBankAccountById.Customer!=null) 
					UIFindClient.FindByLogin(OpOp.FindBankAccountById.Customer.Login);

				UIBankAccountForm.IdCurrency=OpOp.FindBankAccountById.BankAccount.CurrencyID;

			}else{
				UIMessage.TextMessage=OpOp.FindBankAccountById.Information;
			}
		}

		public void ClearUI() {
			UIFindAB.UpdateFullList();
			UIFindClient.ClearUI();
			UIBankAccountForm.ClearUI();
		}

		private void UIEditBt_Click(object sender, RoutedEventArgs e) {
			OpOp.EditBankAccount.IdBankAccount = UIFindAB.IdBankAccount;
			OpOp.EditBankAccount.Balance = UIBankAccountForm.Balance;
			OpOp.EditBankAccount.BankAccountNumber = UIBankAccountForm.BankAccountNumber;
			//OpOp.EditBankAccount.IsLocked = IsLocked;
			OpOp.EditBankAccount.IdCurrency = UIBankAccountForm.IdCurrency;
			OpOp.EditBankAccount.IdCustomer=UIFindClient.IdClient;
			if (OpOp.EditBankAccount.Execute()) {
				ClearUI();
			}
			UIMessage.TextMessage = OpOp.EditBankAccount.Information;
		}

		private void UIButtonAddBA_Click(object sender, RoutedEventArgs e) {
			OpOp.AddBankAccount.Balance = UIBankAccountForm.Balance;
			OpOp.AddBankAccount.IdCurrency = UIBankAccountForm.IdCurrency;
			OpOp.AddBankAccount.IdCustomer = UIFindClient.IdClient;
			if (OpOp.AddBankAccount.Execute()) {
				UIBankAccountForm.ClearUI();
			}
			UIMessage.TextMessage = OpOp.AddBankAccount.Information;
		}

		private void UIButtonRemoveBankAccount_Click(object sender, RoutedEventArgs e) {
			OpOp.RemoveBankAccount.IdBankAccount = UIFindAB.IdBankAccount;
			if (OpOp.RemoveBankAccount.Execute()) {
				ClearUI();
			}
			UIMessage.TextMessage = OpOp.RemoveBankAccount.Information;
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
