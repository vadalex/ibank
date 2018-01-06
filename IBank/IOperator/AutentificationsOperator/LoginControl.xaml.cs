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
using UIControlLibrary.Components;

namespace IOperator.AutentificationsOperator {
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class LoginControl : UserControl,IGetBLL,IDataErrorInfo {

        public LoginControl() {
            InitializeComponent();
        }



		public Message MessageUI { get { return UIMessage; } }


		public String Password { get { return UIPassword.Password; } }


		public event RoutedEventHandler EnterClick {
			add { UIButtonEnter.Click += value; }
			remove { UIButtonEnter.Click -= value; }
		}

		public void UIButtonEnter_Click(object sender, RoutedEventArgs e) {
			OpOp.CheckAutentificationOperator.Login=UILogin.Text;
			OpOp.CheckAutentificationOperator.Password = UIPassword.Password;
			if (!OpOp.CheckAutentificationOperator.Execute()) {
				MessageUI.TextMessage = OpOp.CheckAutentificationOperator.Information;
			} else {
				(Application.Current.MainWindow as UIControlLibrary.Interfaces.IFrame).SetFrame(new IOperator.Path.Root());
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


		public String Login { get; set; }
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
