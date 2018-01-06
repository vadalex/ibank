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

namespace IAdmin.AutentificationsAdmin {
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
	public partial class LoginControl : UserControl, UIControlLibrary.Interfaces.IGetBLL,IDataErrorInfo {

        public LoginControl() {
            InitializeComponent();
        }


		private void UIButtonEnter_Click(object sender, RoutedEventArgs e) {
			OpOp.CheckAutentificationAdmin.Password=UIPassword.Password;
			OpOp.CheckAutentificationAdmin.Login = UILogin.Text;

			if (!OpOp.CheckAutentificationAdmin.Execute()) {
				UIMessage.TextMessage = OpOp.CheckAutentificationAdmin.Information;
				return;
			}

			(Application.Current.MainWindow as UIControlLibrary.Interfaces.IFrame).SetFrame(new IAdmin.Path.RootControl());
			
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


		public string Login { get; set; }

		public string Error {
			get { return "Error"; }
		}

		public string this[string columnName] {
			get {
				string error = String.Empty;
				switch (columnName) {
					case "Login":
						if (!OpOp.Verifier.CheckLogin(Login)) {
							error = OpOp.StringSource.LoginStructureError();
						}
						break;
				}
				return error;
			}
		}
    }
}
