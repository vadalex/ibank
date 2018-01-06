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


namespace IAdmin.AdminActions {
    /// <summary>
    /// Логика взаимодействия для EditAdminPass.xaml
    /// </summary>
	public partial class EditAdminPass : UserControl, IGetBLL,IClearUI {
        public EditAdminPass() {
            InitializeComponent();
        }

        private void UIButtonEditPassword_Click(object sender, RoutedEventArgs e) {
			OpOp.EditAdminPassword.Login = UIAdminForm.Login;
            OpOp.EditAdminPassword.NewPassword=UINewPassword.Password;
			OpOp.EditAdminPassword.NewPasswordCheck=UINew2Password.Password;
			OpOp.EditAdminPassword.Password = UIAdminForm.UIPasswordPassword;
			if (OpOp.EditAdminPassword.Execute()) {
				ClearUI();
			}
			UIMessage.TextMessage = OpOp.EditAdminPassword.Information;
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


		public void ClearUI() {
			UINew2Password.Clear();
			UINewPassword.Clear();
			UIAdminForm.ClearUI();
		}

	

	}
}
