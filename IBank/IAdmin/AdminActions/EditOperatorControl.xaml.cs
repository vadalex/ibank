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
	/// Логика взаимодействия для EditOperatorControl.xaml
	/// </summary>
	public partial class EditOperatorControl : UserControl,IGetBLL {


		public void CollapsedCreate() {
			UIFindOperator.Visibility = Visibility.Collapsed; ;
			//UIOperatorForm;

			UIEditBt.Visibility = Visibility.Collapsed;
			UIButtonRemove.Visibility = Visibility.Collapsed;
		}

		public void CollapsedRemove() {
			//UIFindClient.Visibility = Visibility.Collapsed;
			UIOperatorForm.Visibility = Visibility.Collapsed;
			//UIFindBankAccount.Visibility = Visibility.Collapsed;

			UIButtonAddOperator.Visibility = Visibility.Collapsed; ;
			UIEditBt.Visibility = Visibility.Collapsed;
		}

		public void CollapsedEdit() {
			UIButtonAddOperator.Visibility = Visibility.Collapsed;
			UIButtonRemove.Visibility = Visibility.Collapsed;
		}



		public EditOperatorControl() {
			InitializeComponent();
			UIFindOperator.Finded = Finded;
		}

		public void Finded() {
			OpOp.FindOperatorById.IdOperator = UIFindOperator.IdOperator;
			if (OpOp.FindOperatorById.Execute() && OpOp.FindOperatorById.Operator != null) {
				UIOperatorForm.Login = OpOp.FindOperatorById.Operator.Login;
				UIOperatorForm.UIPasswordPassword = OpOp.FindOperatorById.Operator.Password;
			} else {
				UIMessage.TextMessage = OpOp.FindBankAccountById.Information;
			}
		}

		private void UIEditBt_Click(object sender, RoutedEventArgs e) {
			OpOp.EditOperator.IdOperator = UIFindOperator.IdOperator;
			OpOp.EditOperator.Login = UIOperatorForm.Login;
			OpOp.EditOperator.Password = UIOperatorForm.UIPasswordPassword;
			OpOp.EditOperator.PasswordCheck = UIOperatorForm.UIPasswordPassword;
			if (OpOp.EditOperator.Execute()) {
				UIOperatorForm.ClearUI();
				UIFindOperator.ClearUI();
			}
			UIMessage.TextMessage = OpOp.EditOperator.Information;
		}

		public void UIButtonRemove_Click(object sender, RoutedEventArgs e) {
			OpOp.RemoveOperator.IdOperator = UIFindOperator.IdOperator;
			if (OpOp.RemoveOperator.Execute()) {
				UIOperatorForm.ClearUI();
				UIFindOperator.ClearUI();
			}
			UIMessage.TextMessage = OpOp.RemoveOperator.Information;
		}

		private void UIButtonAddOperator_Click(object sender, RoutedEventArgs e) {
			OpOp.RegistrationOperator.Login = UIOperatorForm.Login;
			OpOp.RegistrationOperator.Password = UIOperatorForm.UIPasswordPassword;
			OpOp.RegistrationOperator.PasswordCheck = UIOperatorForm.UIPasswordPassword;
			if (OpOp.RegistrationOperator.Execute()) {
				UIOperatorForm.ClearUI();
				UIFindOperator.ClearUI();
			}
			UIMessage.TextMessage = OpOp.RegistrationOperator.Information;
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
