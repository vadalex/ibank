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
using BLL.UILogic;
using BLL.UILogic.Operations;
using BLL.ServiceReference1;
using UIControlLibrary.Interfaces;
using System.ComponentModel;

namespace UIControlLibrary.SearchEngines {
    /// <summary>
    /// Логика взаимодействия для FindAC.xaml
    /// </summary>
	public partial class FindAC : UserControl, IGetBLL,IClearUI,IDataErrorInfo {


		public void UpdateFullList() {
			OpOp.FindCardAccountFullList.IsUpdate = true;
			OpOp.FindCardAccountFullList.IsNext = true;
			OpOp.FindCardAccountFullList.AmountElements = UIFullList.AmmountElements;
			OpOp.FindCardAccountFullList.Execute();
			UIMessage.TextMessage = OpOp.FindCardAccountFullList.Information;
			UIFullList.ItemsSource = OpOp.FindCardAccountFullList.EntitiesInformation;
		}

		public Action Finded { get; set; }

        public FindAC() {
            InitializeComponent();

			UIFullList.FindPrev += UIFullList_FindPrev; ;
			UIFullList.FindNext += UIFullList_FindNext; ;
			UIFullList.SelectedClient += UIFullList_SelectedClient; 
        }

		void UIFullList_SelectedClient(object sender, SelectionChangedEventArgs e) {
			int ind = UIFullList.UIClientsList.SelectedIndex;
			if (ind >= 0) {
				UIMessage.TextMessage = OpOp.FindCardAccountFullList.EntitiesInformation.ElementAt(ind);
				IdCardAccount = OpOp.FindCardAccountFullList.IdsEntities.ElementAt(ind);
				if (Finded != null) Finded();
			}
		}

		void UIFullList_FindNext(object sender, RoutedEventArgs e) {
			OpOp.FindCardAccountFullList.IsUpdate = false;
			OpOp.FindCardAccountFullList.IsNext = true;
			OpOp.FindCardAccountFullList.AmountElements = UIFullList.AmmountElements;
			OpOp.FindCardAccountFullList.Execute();
			UIMessage.TextMessage = OpOp.FindCardAccountFullList.Information;
			UIFullList.ItemsSource = OpOp.FindCardAccountFullList.EntitiesInformation;
		}

		void UIFullList_FindPrev(object sender, RoutedEventArgs e) {
			OpOp.FindCardAccountFullList.IsUpdate = false;
			OpOp.FindCardAccountFullList.IsNext = false;
			OpOp.FindCardAccountFullList.AmountElements = UIFullList.AmmountElements;
			OpOp.FindCardAccountFullList.Execute();
			UIMessage.TextMessage = OpOp.FindCardAccountFullList.Information;
			UIFullList.ItemsSource = OpOp.FindCardAccountFullList.EntitiesInformation;
		}



        public int IdCardAccount { get; set; }

        public void UIButtonFind_Click(object sender, RoutedEventArgs e) {
			OpOp.FindCardAccount.CardAccountNumber=UIIDAC.Text;
			if (OpOp.FindCardAccount.Execute()) {
				if (Finded != null) Finded();
			}
			IdCardAccount = OpOp.FindCardAccount.IdCardAccount;
			UIMessage.TextMessage = OpOp.FindCardAccount.Information;
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
			IdCardAccount = -1;
			UIIDAC.Clear();
			UIMessage.ClearUI();
		}


		public string CANumber { get; protected set; }

		public string Error {
			get { return "Error"; }
		}

		public string this[string columnName] {
			get {
				string error = String.Empty;
				switch (columnName) {
					case "CANumber":
						if (!OpOp.Verifier.CheckCardAccountNumber(CANumber)) {
							error = OpOp.StringSource.CardAccountNumberStructureError();
						}
						break;
				}

				return error;
			}
		}


	}
}
