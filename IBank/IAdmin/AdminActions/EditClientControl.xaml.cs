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
	/// Логика взаимодействия для EditClientControl.xaml
	/// </summary>
	public partial class EditClientControl : UserControl,IGetBLL {


		public void CollapsedCreate() {
			UIFindClient.Visibility = Visibility.Collapsed; ;
			UIClientForm.CollapsedAdminEdition();

			UIEditBt.Visibility = Visibility.Collapsed;
			UIButtonRemoveClient.Visibility = Visibility.Collapsed;
		}

		public void CollapsedRemove() {
			//UIFindClient.Visibility = Visibility.Collapsed;
			UIClientForm.Visibility = Visibility.Collapsed;
			//UIFindBankAccount.Visibility = Visibility.Collapsed;

			UIButtonAddClient.Visibility = Visibility.Collapsed; ;
			UIEditBt.Visibility = Visibility.Collapsed;
		}

		public void CollapsedEdit() {
			UIButtonAddClient.Visibility = Visibility.Collapsed;
			UIButtonRemoveClient.Visibility = Visibility.Collapsed;
		}


		public EditClientControl() {
			InitializeComponent();
			UIFindClient.Finded = Finded;
		}

		public void Finded() {
			OpOp.FindClientById.IdClient = UIFindClient.IdClient;
			if (OpOp.FindClientById.Execute() && OpOp.FindClientById.Client != null) {
				UIClientForm.Address=OpOp.FindClientById.Client.Address;
				UIClientForm.LandName = OpOp.FindClientById.Client.Country;
				UIClientForm.EMail = OpOp.FindClientById.Client.Email;
				UIClientForm.FirstName = OpOp.FindClientById.Client.FirstName;
				UIClientForm.LastName = OpOp.FindClientById.Client.LastName;
				UIClientForm.Login = OpOp.FindClientById.Client.Login;
				UIClientForm.MiddleName = OpOp.FindClientById.Client.MiddleName;
				UIClientForm.Password = OpOp.FindClientById.Client.Passoword;
				UIClientForm.PassportNum = OpOp.FindClientById.Client.PassportNumber;
				UIClientForm.IsLock = OpOp.FindClientById.Client.IsLocked;								

			} else {
				UIMessage.TextMessage = OpOp.FindBankAccountById.Information;
			}
		}

		private void UIEditBt_Click(object sender, RoutedEventArgs e) {
			OpOp.EditClient.IdClient = UIFindClient.IdClient;
			OpOp.EditClient.Address = UIClientForm.Address;
			OpOp.EditClient.Country = UIClientForm.LandName;
			OpOp.EditClient.EMail = UIClientForm.EMail;
			OpOp.EditClient.FirstName = UIClientForm.FirstName;
			OpOp.EditClient.LastName = UIClientForm.LastName;
			OpOp.EditClient.MiddleName = UIClientForm.MiddleName;
			OpOp.EditClient.PassportNumber = UIClientForm.PassportNum;
			OpOp.EditClient.IsLocked = UIClientForm.IsLock;
			OpOp.EditClient.Login = UIClientForm.Login;
			OpOp.EditClient.Password = UIClientForm.Password;

			if (OpOp.EditClient.Execute()) {
				UIClientForm.ClearUI();
				UIFindClient.ClearUI();
			}
			UIMessage.TextMessage = OpOp.EditClient.Information;
		}

		private void UIButtonRemoveClient_Click(object sender, RoutedEventArgs e) {
			OpOp.RemoveClient.IdClient = UIFindClient.IdClient;
			if (OpOp.RemoveClient.Execute()) {
				UIClientForm.ClearUI();
				UIFindClient.ClearUI();
			}
			UIMessage.TextMessage = OpOp.RemoveClient.Information;
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

		private void UIButtonAddClient_Click(object sender, RoutedEventArgs e) {
			OpOp.RegistrationClient.IdClient = UIFindClient.IdClient;
			OpOp.RegistrationClient.Address = UIClientForm.Address;
			OpOp.RegistrationClient.Country = UIClientForm.LandName;
			OpOp.RegistrationClient.EMail = UIClientForm.EMail;
			OpOp.RegistrationClient.FirstName = UIClientForm.FirstName;
			OpOp.RegistrationClient.LastName = UIClientForm.LastName;
			OpOp.RegistrationClient.MiddleName = UIClientForm.MiddleName;
			OpOp.RegistrationClient.PassportNumber = UIClientForm.PassportNum;

			if (OpOp.RegistrationClient.Execute()) {
				UIClientForm.ClearUI();
				UIFindClient.ClearUI();
			}
			UIMessage.TextMessage = OpOp.RegistrationClient.Information;
		}

	}
}
