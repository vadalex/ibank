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
using BLL;
using System.ComponentModel;

namespace UIControlLibrary.Forms {
	/// <summary>
	/// Логика взаимодействия для OperatorFormControl.xaml
	/// </summary>
	public partial class OperatorFormControl : UserControl, IForm {
		public OperatorFormControl() {
			InitializeComponent();
		}

		static OperatorFormControl() {
			LoginProperty = DependencyProperty.Register("Login", typeof(string), typeof(OperatorFormControl));
		}
		public static readonly DependencyProperty LoginProperty;


		public void ClearUI() {
			UIPassword.Clear();
			UILogin.Clear();
		}


		public string Login { get { return (string)GetValue(LoginProperty); } set { SetValue(LoginProperty, value); } }

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


		public BLL.UILogic.RepositoryOperations OpOp {
			get {
				if ((Application.Current.MainWindow as UIControlLibrary.Interfaces.IGetBLL) != null)
					return (Application.Current.MainWindow as UIControlLibrary.Interfaces.IGetBLL).OpOp;
				else {
					return new BLL.UILogic.RepositoryOperations();
				}
			}
		}

		public string UIPasswordPassword { get { return UIPassword.Password; } set { UIPassword.Password = value; } }
	}
}
