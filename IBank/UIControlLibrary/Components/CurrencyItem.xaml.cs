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
using System.ComponentModel;
using UIControlLibrary.Interfaces;

namespace UIControlLibrary.Components {
    /// <summary>
    /// Логика взаимодействия для CurrencyItem.xaml
    /// </summary>
    public partial class CurrencyItem : UserControl,IDataErrorInfo,IGetBLL {
        public CurrencyItem() {
            InitializeComponent();
        }

		static CurrencyItem() {
			CoefProperty = DependencyProperty.Register("Coef", typeof(string), typeof(CurrencyItem));
		}

		public static readonly DependencyProperty CoefProperty;
		public string Coef {
			get { return (string)GetValue(CoefProperty); }
			set { SetValue(CoefProperty, value); }
		}


		public event RoutedEventHandler ButtonRemove {
			add { UIButtonRemoveCurrency.Click += value; }
			remove { UIButtonRemoveCurrency.Click -= value; }
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

		public string NameCurrency{get{return UINameCurrency.Text;}set{UINameCurrency.Text=value;}}

		

		public string Error {
			get { return "Error"; }
		}
		public string this[string columnName] {
			get {
				string error = String.Empty;
				switch (columnName) {
					case "Coef":
						if (!OpOp.Verifier.CheckCurrencyCoef(Coef)) {
							error = OpOp.StringSource.CurrencyCoefStructureError();
						}

						break;
				}
				return error;
			}
		}


		public string ShortNameCurrency { get { return UIShortNameCurrency.Text; } set { UIShortNameCurrency.Text = value; } }

		public string IdCurrency { get { return UIIdCurrency.Text; } set { UIIdCurrency.Text = value; } }
	}
}
