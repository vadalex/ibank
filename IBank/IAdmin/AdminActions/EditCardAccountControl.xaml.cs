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
using UIControlLibrary.Interfaces;

namespace IAdmin.AdminActions {
	/// <summary>
	/// Логика взаимодействия для EditCardAccountControl.xaml
	/// </summary>
	public partial class EditCardAccountControl : UserControl,IGetBLL {

		public void CollapsedCreate() {
			UIFindCardAccount.Visibility = Visibility.Collapsed; ;
			UICardAccountForm.CollapsedAdminEdition();

			UIEditBt.Visibility = Visibility.Collapsed;
			UIButtonRemoveCardAccount.Visibility = Visibility.Collapsed;
		}

		public void CollapsedRemove() {
			UIFindClient.Visibility = Visibility.Collapsed; 
			UICardAccountForm.Visibility = Visibility.Collapsed;
			UIFindBankAccount.Visibility = Visibility.Collapsed; 

			UIButtonAddCardAccount.Visibility = Visibility.Collapsed; ;
			UIEditBt.Visibility = Visibility.Collapsed;
		}

		public void CollapsedEdit() {
			UIButtonAddCardAccount.Visibility = Visibility.Collapsed;
			UIButtonRemoveCardAccount.Visibility = Visibility.Collapsed;
		}



		public EditCardAccountControl() {
			InitializeComponent();
			UIFindCardAccount.Finded = new Action(Finded);
		}

		public void Finded() {
			OpOp.FindCardAccountById.IdCardAccount = UIFindCardAccount.IdCardAccount;
			if (OpOp.FindCardAccountById.Execute() && OpOp.FindCardAccountById.CardAccount != null) {
				UICardAccountForm.Number = OpOp.FindCardAccountById.CardAccount.CardNumber;
				UICardAccountForm.Namee = OpOp.FindCardAccountById.CardAccount.Name;
				UICardAccountForm.Type = OpOp.FindCardAccountById.CardAccount.CardType;
				UICardAccountForm.ExpiredDate = OpOp.FindCardAccountById.CardAccount.ExpiredDate;
				UICardAccountForm.IsLock = OpOp.FindCardAccountById.CardAccount.IsLocked;
				UICardAccountForm.Status = OpOp.FindCardAccountById.CardAccount.Status;


				if (OpOp.FindCardAccountById.Customer != null) {
					UIFindClient.FindByLogin(OpOp.FindCardAccountById.Customer.Login);
				}
				if (OpOp.FindCardAccountById.BankAccount != null) {
					UIFindBankAccount.FindByNumber(OpOp.FindCardAccountById.BankAccount.AcountNumber);
				}

			} else {
				UIMessage.TextMessage = OpOp.FindCardAccountById.Information;
			}
		}

		private void UIEditBt_Click(object sender, RoutedEventArgs e) {
			OpOp.EditCardAccount.CardAccountName = UICardAccountForm.Namee;
			OpOp.EditCardAccount.CardAccountNumber = UICardAccountForm.Number;
			OpOp.EditCardAccount.CardAccountStatus = UICardAccountForm.Status;
			OpOp.EditCardAccount.CardType = UICardAccountForm.Type;
			OpOp.EditCardAccount.ExpiredDate = UICardAccountForm.ExpiredDate;
			OpOp.EditCardAccount.IdBankAccount = UIFindBankAccount.IdBankAccount;
			OpOp.EditCardAccount.IdCardAccount = UIFindCardAccount.IdCardAccount;
			OpOp.EditCardAccount.IdClient = UIFindClient.IdClient;
			OpOp.EditCardAccount.IsLocked =UICardAccountForm.IsLock;

			if (OpOp.EditCardAccount.Execute()) {
				UICardAccountForm.ClearUI();

			}
			UIMessage.TextMessage = OpOp.EditCardAccount.Information;
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

		private void UIButtonRemoveCardAccount_Click(object sender, RoutedEventArgs e) {
			OpOp.RemoveCardAccount.IdCardAccount = UIFindCardAccount.IdCardAccount;

			if (OpOp.RemoveCardAccount.Execute()) {
				UICardAccountForm.ClearUI();

			}
			UIMessage.TextMessage = OpOp.RemoveCardAccount.Information;
		}
		

		private void UIButtonAddCardAccount_Click(object sender, RoutedEventArgs e) {
			OpOp.RegistrationCardAccount.CardAccountName = UICardAccountForm.Namee;
			OpOp.RegistrationCardAccount.CardAccountStatus = OpOp.DataSource.CardStatusUnLocked;
			OpOp.RegistrationCardAccount.CardType = UICardAccountForm.Type;
			OpOp.RegistrationCardAccount.ExpiredDate = UICardAccountForm.ExpiredDate;
			OpOp.RegistrationCardAccount.IdBankAccount = UIFindBankAccount.IdBankAccount;

			if (OpOp.RegistrationCardAccount.Execute()) {
				UICardAccountForm.ClearUI();

			}
			UIMessage.TextMessage = OpOp.RegistrationCardAccount.Information;
		}

	}
}
