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
    /// Логика взаимодействия для EditCurrency.xaml
    /// </summary>
	public partial class EditCurrency : UserControl, UIControlLibrary.Interfaces.IGetBLL,IClearUI {


        public EditCurrency() {
            InitializeComponent();
			this.Loaded += EditCurrency_Loaded;
        }

		void EditCurrency_Loaded(object sender, RoutedEventArgs e) {
			OpOp.GetAllCurrencies.Execute();
			for (int i = 0; i < OpOp.GetAllCurrencies.CurrenciesIds.Count; i++) {
					var ci = new CurrencyItem();
					ci.Coef = OpOp.GetAllCurrencies.CurrenciesRates.ElementAt(i);
					ci.NameCurrency = OpOp.GetAllCurrencies.CurrenciesNames.ElementAt(i);
					ci.ShortNameCurrency = OpOp.GetAllCurrencies.CurrenciesShortNames.ElementAt(i);
					ci.IdCurrency = OpOp.GetAllCurrencies.CurrenciesIds.ElementAt(i);
					ci.ButtonRemove += UIButtonRemoveCurrency_Click;
					UICurrences.Children.Add(ci);
			}
			OpOp.GetNBRBCurrencies.Execute();
			UIMesCurrenciesNBRB.TextMessage=OpOp.GetNBRBCurrencies.Information;
		}


		private void UIButtonRemoveCurrency_Click(object sender, RoutedEventArgs e) {
			var ba = sender as Button;
			var sp=ba.Parent as	StackPanel;
			var ci = sp.Parent as CurrencyItem;
			UICurrences.Children.Remove(ci);
		}

        private void UIButtonAddCurrencyItem_Click(object sender, RoutedEventArgs e) {
			var ci = new CurrencyItem { };
			ci.ButtonRemove += UIButtonRemoveCurrency_Click;
            UICurrences.Children.Add(ci);
        }

        private void UIButtonSave_Click(object sender, RoutedEventArgs e) {
			var currencyRates = new List<string>();
			var currencyNames = new List<string>();
			var currencyShortNames = new List<string>();
			var currencyIds = new List<string>();
			CurrencyItem ci = null;
			foreach (var el in UICurrences.Children) {
				ci=(el as CurrencyItem);
				if (!OpOp.Verifier.CheckCurrencyCoef(ci.Coef)) {
					UIMessage.TextMessage = OpOp.StringSource.CurrencyCoefStructureError();
					return;
				}
				currencyRates.Add(ci.Coef);
				currencyNames.Add(ci.NameCurrency);
				currencyShortNames.Add(ci.ShortNameCurrency);
				currencyIds.Add(ci.IdCurrency);
			}

			OpOp.EditCurrencies.CurrenciesId=currencyIds;
			OpOp.EditCurrencies.CurrenciesRates=currencyRates;
			OpOp.EditCurrencies.CurrenciesNames=currencyNames;
			OpOp.EditCurrencies.CurrenciesShortNames = currencyShortNames;
			OpOp.EditCurrencies.Execute();
			UIMessage.TextMessage = OpOp.EditCurrencies.Information;
			UICurrences.Children.Clear();
			EditCurrency_Loaded(null, null);
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
			UIMessage.ClearUI();
			UICurrences.Children.Clear();
		}
	}
}
