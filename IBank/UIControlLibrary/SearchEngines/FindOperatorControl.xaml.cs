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
using BLL.ServiceReference1;
using UIControlLibrary.Interfaces;
using System.ComponentModel;

namespace UIControlLibrary.SearchEngines {
    /// <summary>
    /// Логика взаимодействия для FindOperator.xaml
    /// </summary>
	public partial class FindOperatorControl : UserControl, UIControlLibrary.Interfaces.IGetBLL,IDataErrorInfo {

		public Action Finded { get; set; }

		public FindOperatorControl() {
            InitializeComponent();

			UIFullList.FindPrev += UIFullList_FindPrev; ;
			UIFullList.FindNext += UIFullList_FindNext; ;
			UIFullList.SelectedClient += UIFullList_SelectedClient; 
        }


		public void UpdateFullList() {
			OpOp.FindOperatorFullList.IsUpdate = true;
			OpOp.FindOperatorFullList.IsNext = true;
			OpOp.FindOperatorFullList.AmountElements = UIFullList.AmmountElements;
			OpOp.FindOperatorFullList.Execute();
			UIMessage.TextMessage = OpOp.FindOperatorFullList.Information;
			UIFullList.ItemsSource = OpOp.FindOperatorFullList.EntitiesInformation;
		}


		void UIFullList_SelectedClient(object sender, SelectionChangedEventArgs e) {
			int ind = UIFullList.UIClientsList.SelectedIndex;
			if (ind >= 0) {
				UIMessage.TextMessage = OpOp.FindOperatorFullList.EntitiesInformation.ElementAt(ind);
				IdOperator = OpOp.FindOperatorFullList.IdsEntities.ElementAt(ind);
				if (Finded != null) Finded();
			}
		}

		void UIFullList_FindNext(object sender, RoutedEventArgs e) {
			OpOp.FindOperatorFullList.IsUpdate = false;
			OpOp.FindOperatorFullList.IsNext = true;
			OpOp.FindOperatorFullList.AmountElements = UIFullList.AmmountElements;
			OpOp.FindOperatorFullList.Execute();
			UIMessage.TextMessage = OpOp.FindOperatorFullList.Information;
			UIFullList.ItemsSource = OpOp.FindOperatorFullList.EntitiesInformation;
		}

		void UIFullList_FindPrev(object sender, RoutedEventArgs e) {
			OpOp.FindOperatorFullList.IsUpdate = false;
			OpOp.FindOperatorFullList.IsNext = false;
			OpOp.FindOperatorFullList.AmountElements = UIFullList.AmmountElements;
			OpOp.FindOperatorFullList.Execute();
			UIMessage.TextMessage = OpOp.FindOperatorFullList.Information;
			UIFullList.ItemsSource = OpOp.FindOperatorFullList.EntitiesInformation;
		}



        public void ClearUI() {
			IdOperator = -1;
            UIClientLogin.Clear();
			UIMessage.ClearUI();
        }
		
		public int IdOperator { get; protected set; }

        public void UIButtonFind_Click(object sender, RoutedEventArgs e) {
			OpOp.FindOperator.Login=UIClientLogin.Text;
			if (OpOp.FindOperator.Execute()) {
				if (Finded != null) Finded();
			}
			IdOperator = OpOp.FindOperator.IdOperator;
			UIMessage.TextMessage = OpOp.FindOperator.Information;
        }


		public event RoutedEventHandler FindButtonEvent {
			add { UIButtonFind.Click += value; }
			remove { UIButtonFind.Click += value; }
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


		public string Login { get; protected set; }

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
