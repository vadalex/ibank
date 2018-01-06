using BLL.ServiceReference1;
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

namespace UIControlLibrary.Forms {
	/// <summary>
	/// Логика взаимодействия для CardAccountFormControl.xaml
	/// </summary>
	public partial class CardAccountFormControl : UserControl, UIControlLibrary.Interfaces.IForm {
		public CardAccountFormControl() {
			InitializeComponent();
			Loaded += CardAccountFormControl_Loaded;

            UILockTB.Visibility = Visibility.Collapsed;
            UILock.Visibility = Visibility.Collapsed;
		}

		public void CollapsedAdminEdition() {
			UICardNumberTB.Visibility = Visibility.Collapsed;
			UICardAccountNumber.Visibility = Visibility.Collapsed;
			UILock.Visibility = Visibility.Collapsed;
			UILockTB.Visibility = Visibility.Collapsed;
			UIStatusCA.Visibility = Visibility.Collapsed;
			UIStatusTB.Visibility = Visibility.Collapsed;
		}

		void CardAccountFormControl_Loaded(object sender, RoutedEventArgs e) {
			Types = OpOp.DataSource.CardAccountTypes;
			Statuses = OpOp.DataSource.CardAccountStatuses;
			Names = OpOp.DataSource.CardAccountDefaultNames;
			ExpiredDate = OpOp.DataSource.CardAccountDefaultExpiredDate;
		}
		
		static CardAccountFormControl() {
			NamesProperty = DependencyProperty.Register("Names", typeof(List<string>), typeof(CardAccountFormControl));
			StatusesProperty = DependencyProperty.Register("Statuses", typeof(List<string>), typeof(CardAccountFormControl));
			TypesProperty = DependencyProperty.Register("Types", typeof(List<string>), typeof(CardAccountFormControl));
			TypeProperty = DependencyProperty.Register("Type", typeof(string), typeof(CardAccountFormControl));
			NameeProperty = DependencyProperty.Register("Namee", typeof(string), typeof(CardAccountFormControl));
			ExpiredDateProperty = DependencyProperty.Register("ExpiredDate", typeof(DateTime), typeof(CardAccountFormControl));
			StatusProperty = DependencyProperty.Register("Status", typeof(string), typeof(CardAccountFormControl));
			NumberProperty = DependencyProperty.Register("Number", typeof(string), typeof(CardAccountFormControl));
			IsLockProperty = DependencyProperty.Register("IsLock", typeof(bool), typeof(CardAccountFormControl));
		}
		public static readonly DependencyProperty TypesProperty;
		public List<string> Types { get { return (List<string>)GetValue(TypesProperty); } 
			set{SetValue(TypesProperty,value);} }
		public static readonly DependencyProperty NamesProperty;
		public List<string> Names {
			get { return (List<string>)GetValue(NamesProperty); }
			set { SetValue(NamesProperty, value); }
		}
		public static readonly DependencyProperty StatusesProperty;
		public List<string> Statuses {
			get { return (List<string>)GetValue(StatusesProperty); }
			set { SetValue(StatusesProperty, value); }
		}
		public static readonly DependencyProperty ExpiredDateProperty;
		public static readonly DependencyProperty NameeProperty;
		public static readonly DependencyProperty TypeProperty;
		public static readonly DependencyProperty StatusProperty;
		public static readonly DependencyProperty NumberProperty;
		public static readonly DependencyProperty IsLockProperty;

		public void ClearUI() {
			Namee = "";
			Type = "";
			ExpiredDate = OpOp.DataSource.CardAccountDefaultExpiredDate;
			Status = "";
			Number = "";
			IsLock = false;
		}

		public DateTime ExpiredDate { get { return (DateTime)GetValue(ExpiredDateProperty); } set { SetValue(ExpiredDateProperty, value); } }
		public string Namee { get { return (string)GetValue(NameeProperty); } set { SetValue(NameeProperty, value); } }
		public string Type { get { return (string)GetValue(TypeProperty); } set { SetValue(TypeProperty, value); } }
		public string Status { get { return (string)GetValue(StatusProperty); } set { SetValue(StatusProperty, value); } }
		public string Number { get { return (string)GetValue(NumberProperty); } set { SetValue(NumberProperty, value); } }
		public bool IsLock { get { return (bool)GetValue(IsLockProperty); } set { SetValue(IsLockProperty, value); } }

		public string Error {
			get { return "Error"; }
		}

		public string this[string columnName] {
			get {
				string error = String.Empty;
				switch (columnName) {
					case "ExpiredDate":
						if (!OpOp.Verifier.CheckExpiredDate(ExpiredDate)) {
							error = OpOp.StringSource.ExpiredDateStructureError();
						}
						break;
					case "Namee":
						if (!OpOp.Verifier.CheckName(Namee)) {
							error = OpOp.StringSource.NameStructureError();
						}
						break;
					case "Type":
						if (!OpOp.Verifier.CheckName(Type)) {
							error = OpOp.StringSource.NameStructureError();
						}
						break;
					case "Status":
						if (!OpOp.Verifier.CheckName(Status)) {
							error = OpOp.StringSource.NameStructureError();
						}
						break;
					case "Number":
						if (!OpOp.Verifier.CheckCardAccountNumber(Number)) {
							error = OpOp.StringSource.CardAccountNumberStructureError();
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
