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

namespace UIControlLibrary.SearchEngines {
	/// <summary>
	/// Логика взаимодействия для SelectCurrencyControl.xaml
	/// </summary>
	public partial class SelectCurrencyControl : UserControl,IGetBLL,IDataErrorInfo,IClearUI {
		public SelectCurrencyControl() {
			InitializeComponent();

			OpOp.GetAllCurrencies.Execute();
			UICurrencies.ItemsSource = OpOp.GetAllCurrencies.CurrenciesNames;

		}

		public void ClearUI() {
			UICurrencies.Text = "";
		}

		public void SelectCurrencyById(int id) {
			if (OpOp.GetAllCurrencies.AllCurrencies == null) {
				OpOp.GetAllCurrencies.Execute();
				UICurrencies.ItemsSource = OpOp.GetAllCurrencies.CurrenciesNames;
			}
			var a=OpOp.GetAllCurrencies.AllCurrencies.First(el=>el.CurrencyID==id);
			var b=OpOp.GetAllCurrencies.CurrenciesNames.FindIndex(el=>el==a.Name);
			if(b>=0) UICurrencies.SelectedIndex=b;
		}

		public int IdCurrency {
			get {
				if (UICurrencies.SelectedIndex >= 0)
					return int.Parse(OpOp.GetAllCurrencies.CurrenciesIds.ElementAt(UICurrencies.SelectedIndex));
				else return -1;
			}
		}

		public string CurrencyName { get; set; }

		public string Error {
			get { return "Error"; }
		}

		public string this[string columnName] {
			get {
				string error = String.Empty;
				switch (columnName) {
					case "CurrencyName":
						if (CurrencyName == null || CurrencyName == "") {
							error = OpOp.StringSource.NoSelectedCurrency();
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
