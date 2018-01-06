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

namespace UIControlLibrary.SearchEngines
{
    /// <summary>
    /// Логика взаимодействия для FindAB.xaml
    /// </summary>
    public partial class FindAB : UserControl,IGetBLL,IClearUI,IDataErrorInfo    {

		public Action Finded { get; set; }

        public FindAB()
        {
			InitializeComponent();

			UIFullList.FindPrev += UIFullList_FindPrev; ;
			UIFullList.FindNext += UIFullList_FindNext; ;
			UIFullList.SelectedClient += UIFullList_SelectedClient; 
        }

		public void UpdateFullList() {
			OpOp.FindBankAccountFullList.IsUpdate = true;
			OpOp.FindBankAccountFullList.IsNext = true;
			OpOp.FindBankAccountFullList.AmountElements = UIFullList.AmmountElements;
			OpOp.FindBankAccountFullList.Execute();
			UIMessage.TextMessage = OpOp.FindBankAccountFullList.Information;
			UIFullList.ItemsSource = OpOp.FindBankAccountFullList.EntitiesInformation;
		}


		void UIFullList_SelectedClient(object sender, SelectionChangedEventArgs e) {
			int ind = UIFullList.UIClientsList.SelectedIndex;
			if (ind >= 0) {
				UIMessage.TextMessage = OpOp.FindBankAccountFullList.EntitiesInformation.ElementAt(ind);
				IdBankAccount = OpOp.FindBankAccountFullList.IdsEntities.ElementAt(ind);
				if(Finded!=null) Finded();
			}
		}

		void UIFullList_FindNext(object sender, RoutedEventArgs e) {
			OpOp.FindBankAccountFullList.IsUpdate = false;
			OpOp.FindBankAccountFullList.IsNext = true;
			OpOp.FindBankAccountFullList.AmountElements = UIFullList.AmmountElements;
			OpOp.FindBankAccountFullList.Execute();
			UIMessage.TextMessage = OpOp.FindBankAccountFullList.Information;
			UIFullList.ItemsSource = OpOp.FindBankAccountFullList.EntitiesInformation;
		}

		void UIFullList_FindPrev(object sender, RoutedEventArgs e) {
			OpOp.FindBankAccountFullList.IsUpdate = false;
			OpOp.FindBankAccountFullList.IsNext = false;
			OpOp.FindBankAccountFullList.AmountElements = UIFullList.AmmountElements;
			OpOp.FindBankAccountFullList.Execute();
			UIMessage.TextMessage = OpOp.FindBankAccountFullList.Information;
			UIFullList.ItemsSource = OpOp.FindBankAccountFullList.EntitiesInformation;
		}




        public void ClearUI(){
			IdBankAccount = -1;
            UIIDAB.Clear();
			UIMessage.ClearUI();
        }

        public int IdBankAccount { get; set; }

        public void UIButtonFind_Click(object sender, RoutedEventArgs e)        {
			OpOp.FindBankAccount.BankAccountNumber=UIIDAB.Text;
			if (OpOp.FindBankAccount.Execute()) {
				if (Finded != null) Finded();
			}
			IdBankAccount = OpOp.FindBankAccount.IdBankAccount;
			UIMessage.TextMessage = OpOp.FindBankAccount.Information;
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


		public string BANumber { get; set; }

		public string Error {
			get { return "Error"; }
		}

		public string this[string columnName] {
			get {
				string error = String.Empty;
				switch (columnName) {
					case "BANumber":
						if (!OpOp.Verifier.CheckBankAccountNumber(BANumber)) {
							error = OpOp.StringSource.BankAccountNumberStructureError();
						}
						break;
				}

				return error;
			}
		}




		public string BankAccountNumber { get { return UIIDAB.Text; } set { UIIDAB.Text = value; } }

		public void FindByNumber(string number) {
			UIIDAB.Text = number;
			UIFinderTC.SelectedIndex = 0;
			UIButtonFind_Click(null, null);
		}
	}
}
