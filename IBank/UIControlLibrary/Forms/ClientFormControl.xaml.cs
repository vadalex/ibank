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

namespace UIControlLibrary.Forms {
	/// <summary>
	/// Логика взаимодействия для ClientFormControl.xaml
	/// </summary>
	public partial class ClientFormControl : UserControl, IForm {
		public ClientFormControl() {
			InitializeComponent();
			Loaded += ClientFormControl_Loaded;

		    UILockTB.Visibility = Visibility.Collapsed;
            UILock.Visibility = Visibility.Collapsed;
		}

		void ClientFormControl_Loaded(object sender, RoutedEventArgs e) {
			LandNames = OpOp.DataSource.LandNames;
			Emails = new List<string>() { OpOp.DataSource.NotEMail };
		}

		public void CollapsedAdminEdition() {
			UILogin.Visibility = Visibility.Collapsed;
			UILoginTB.Visibility = Visibility.Collapsed;
			UIPassword.Visibility = Visibility.Collapsed;
			UIPasswordTB.Visibility = Visibility.Collapsed;
			UILock.Visibility = Visibility.Collapsed;
			UILockTB.Visibility = Visibility.Collapsed;
		}

		public void ClearUI() {
			UILandName.Text = string.Empty;
			UIUserIDPasport.Clear();
			UIUserName.Clear();
			UIUserName2.Clear();
			UIUserName3.Clear();
			UIAddress.Clear();
			IUEMail.Text = "";
		}

		static ClientFormControl() {
			LandNamesProperty = DependencyProperty.Register("LandNames", typeof(List<string>), typeof(ClientFormControl));
			EmailsProperty = DependencyProperty.Register("Emails", typeof(List<string>), typeof(ClientFormControl));
			LandNameProperty = DependencyProperty.Register("LandName", typeof(string), typeof(ClientFormControl));
			FirstNameProperty = DependencyProperty.Register("FirstName", typeof(string), typeof(ClientFormControl));
			LastNameProperty = DependencyProperty.Register("LastName", typeof(string), typeof(ClientFormControl));
			MiddleNameProperty = DependencyProperty.Register("MiddleName", typeof(string), typeof(ClientFormControl));
			AddressProperty = DependencyProperty.Register("Address", typeof(string), typeof(ClientFormControl));
			EmailProperty = DependencyProperty.Register("EMail", typeof(string), typeof(ClientFormControl));
			PassportNumProperty = DependencyProperty.Register("PassportNum", typeof(string), typeof(ClientFormControl));
			IsLockProperty = DependencyProperty.Register("IsLock", typeof(bool), typeof(ClientFormControl));
			PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(ClientFormControl));
			LoginlProperty = DependencyProperty.Register("Login", typeof(string), typeof(ClientFormControl));
			
		}
		public static readonly DependencyProperty LandNamesProperty;
		public static readonly DependencyProperty EmailsProperty;
		public static readonly DependencyProperty LandNameProperty;
		public static readonly DependencyProperty FirstNameProperty;
		public static readonly DependencyProperty LastNameProperty;
		public static readonly DependencyProperty MiddleNameProperty;
		public static readonly DependencyProperty AddressProperty;
		public static readonly DependencyProperty EmailProperty;
		public static readonly DependencyProperty PassportNumProperty;
		public static readonly DependencyProperty IsLockProperty;
		public static readonly DependencyProperty PasswordProperty;
		public static readonly DependencyProperty LoginlProperty;


		public List<string> LandNames { get { return (List<string>)GetValue(LandNamesProperty); } set { SetValue(LandNamesProperty, value); } }
		public List<string> Emails { get { return (List<string>)GetValue(EmailsProperty); } set { SetValue(EmailsProperty, value); } }

		public String FirstName { get { return (string)GetValue(FirstNameProperty); } set { SetValue(FirstNameProperty, value); } }
		public String LastName { get { return (string)GetValue(LastNameProperty); } set { SetValue(LastNameProperty, value); } }
		public String MiddleName { get { return (string)GetValue(MiddleNameProperty); } set { SetValue(MiddleNameProperty, value); } }
		public String PassportNum { get { return (string)GetValue(PassportNumProperty); } set { SetValue(PassportNumProperty, value); } }
		public String LandName { get { return (string)GetValue(LandNameProperty); } set { SetValue(LandNameProperty, value); } }
		public String Address { get { return (string)GetValue(AddressProperty); } set { SetValue(AddressProperty, value); } }
		public String EMail { get { return (string)GetValue(EmailProperty); } set { SetValue(EmailProperty, value); } }

		public bool IsLock { get { return (bool)GetValue(IsLockProperty); } set { SetValue(IsLockProperty, value); } }
		public String Login { get { return (string)GetValue(LoginlProperty); } set { SetValue(LoginlProperty, value); } }
		public String Password { get { return (string)GetValue(PasswordProperty); } set { SetValue(PasswordProperty, value); } }

		public string Error {
			get { return "Error"; }
		}

		public string this[string columnName] {
			get {
				string error = String.Empty;
				switch (columnName) {
					case "FirstName":
						if (!OpOp.Verifier.CheckName(FirstName)) {
							error = OpOp.StringSource.NameStructureError();
						} else {
							var nm = FirstName.ToArray();
							if (Char.IsLower(nm[0])) {
								nm[0] = Char.ToUpper(nm[0]);
								UIUserName.Text = string.Concat(nm);
							}
						}
						break;
					case "LastName":
						if (!OpOp.Verifier.CheckName(LastName)) {
							error = OpOp.StringSource.NameStructureError();
						} else {

							var nm = LastName.ToArray();
							if (Char.IsLower(nm[0])) {
								nm[0] = Char.ToUpper(nm[0]);
								UIUserName2.Text = string.Concat(nm);
							}
						}
						break;
					case "MiddleName":
						if (!OpOp.Verifier.CheckName(MiddleName)) {
							error = OpOp.StringSource.NameStructureError();
						} else {
							var nm = MiddleName.ToArray();
							if (Char.IsLower(nm[0])) {
								nm[0] = Char.ToUpper(nm[0]);
								UIUserName3.Text = string.Concat(nm);
							}

						}
						break;
					case "PassportNum":
						if (!OpOp.Verifier.CheckPassportNumber(PassportNum)) {
							error = OpOp.StringSource.PassportNumberStructureError();
						}
						break;
					case "LandName":
						if (!OpOp.Verifier.CheckName(LandName)) {
							error = OpOp.StringSource.NameStructureError();
						}
						break;
					case "Address":
						if (!OpOp.Verifier.CheckAddress(Address)) {
							error = OpOp.StringSource.AddressStructureError();
						}
						break;
					case "EMail":
						if (!OpOp.Verifier.CheckEMail(EMail)) {
							error = OpOp.StringSource.EMailStructureError();
						}
						break;
					case "Login":
						if (!OpOp.Verifier.CheckLogin(Login)) {
							error = OpOp.StringSource.LoginStructureError();
						}
						break;
					case "Password":
						if (!OpOp.Verifier.CheckPassword(Password)) {
							error = OpOp.StringSource.PasswordStructureError();
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
