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

namespace UIControlLibrary.SearchEngines {
    /// <summary>
    /// Логика взаимодействия для FindClient.xaml
    /// </summary>
    public partial class FindClient : UserControl,IGetBLL,IClearUI,IDataErrorInfo {

		public void UpdateFullList() {
			OpOp.FindClientFullList.IsUpdate = true;
			OpOp.FindClientFullList.IsNext = true;
			OpOp.FindClientFullList.AmountElements = UIFullList.AmmountElements;
			OpOp.FindClientFullList.Execute();
			UIMessage.TextMessage = OpOp.FindClientFullList.Information;
			UIFullList.ItemsSource = OpOp.FindClientFullList.EntitiesInformation;
		}


		public Action Finded { get; set; }

        public FindClient() {
            InitializeComponent();
			UIListByLastName.FindPrev += UIListByLastName_FindPrev;
			UIListByLastName.FindNext += UIListByLastName_FindNext;
			UIListByLastName.SelectedClient += UIListByLastName_SelectedClient;

			UIFullList.FindPrev +=UIFullList_FindPrev; ;
			UIFullList.FindNext +=UIFullList_FindNext; ;
			UIFullList.SelectedClient += UIFullList_SelectedClient; ;
        }

		void UIFullList_SelectedClient(object sender, SelectionChangedEventArgs e) {
			int ind = UIFullList.UIClientsList.SelectedIndex;
			if (ind >= 0) {
				UIMessage.TextMessage = OpOp.FindClientFullList.EntitiesInformation.ElementAt(ind);
				IdClient = OpOp.FindClientFullList.IdsEntities.ElementAt(ind);
				if (Finded != null) Finded();
			}
		}

		void UIFullList_FindNext(object sender, RoutedEventArgs e) {
			OpOp.FindClientFullList.IsUpdate = false;
			OpOp.FindClientFullList.IsNext = true;
			OpOp.FindClientFullList.AmountElements = UIFullList.AmmountElements;
			OpOp.FindClientFullList.Execute();
			UIMessage.TextMessage = OpOp.FindClientFullList.Information;
			UIFullList.ItemsSource = OpOp.FindClientFullList.EntitiesInformation;
		}

		void UIFullList_FindPrev(object sender, RoutedEventArgs e) {
			OpOp.FindClientFullList.IsUpdate = false;
			OpOp.FindClientFullList.IsNext = false;
			OpOp.FindClientFullList.AmountElements = UIFullList.AmmountElements;
			OpOp.FindClientFullList.Execute();
			UIMessage.TextMessage = OpOp.FindClientFullList.Information;
			UIFullList.ItemsSource = OpOp.FindClientFullList.EntitiesInformation;
		}

		void UIListByLastName_SelectedClient(object sender, SelectionChangedEventArgs e) {
			int ind=UIListByLastName.UIClientsList.SelectedIndex;
			if (ind >= 0) {
				UIMessage.TextMessage = OpOp.FindClientsByLastName.ClientsInformation.ElementAt(ind);
				IdClient = OpOp.FindClientsByLastName.IdsClients.ElementAt(ind);
				if (Finded != null) Finded();
			}
		}

		void UIListByLastName_FindNext(object sender, RoutedEventArgs e) {
			

			OpOp.FindClientsByLastName.IsNext = true;
			OpOp.FindClientsByLastName.AmmountElements = UIListByLastName.AmmountElements;
			OpOp.FindClientsByLastName.LastName = LastName;
			OpOp.FindClientsByLastName.Execute();
			UIMessage.TextMessage = OpOp.FindClientsByLastName.Information;
			UIListByLastName.ItemsSource = OpOp.FindClientsByLastName.ClientsInformation;
		}

		void UIListByLastName_FindPrev(object sender, RoutedEventArgs e) {
			OpOp.FindClientsByLastName.IsNext = false;
			OpOp.FindClientsByLastName.AmmountElements = UIListByLastName.AmmountElements;
			OpOp.FindClientsByLastName.LastName = LastName;
			OpOp.FindClientsByLastName.Execute();
			UIMessage.TextMessage = OpOp.FindClientsByLastName.Information;
			UIListByLastName.ItemsSource = OpOp.FindClientsByLastName.ClientsInformation;
		}

        public void ClearUI(){
			IdClient = -1;
            UIClientLogin.Clear();
			UIMessage.ClearUI();
        }

        public int IdClient { get; protected set; }

        public void UIButtonFind_Click(object sender, RoutedEventArgs e) {
			OpOp.FindClient.PassportNumber = null;
			OpOp.FindClient.Login=Login;
			if (OpOp.FindClient.Execute()) {
				UIClientLogin.Text = OpOp.FindClient.Login;
				UIClientPassportNum.Text = OpOp.FindClient.PassportNumber;
				if (Finded != null) Finded();
			}
			IdClient = OpOp.FindClient.IdClient;
			UIMessage.TextMessage = OpOp.FindClient.Information;
        }

		public void FindById(int id) {
			OpOp.FindClientById.IdClient = id;
			OpOp.FindClientById.Execute();
			if (OpOp.FindClientById.Client!=null) FindByLogin(OpOp.FindClientById.Client.Login);
		}

		public void FindByLogin(string login) {
			UIClientLogin.Text=login;
			UIFinder.SelectedIndex = 0;
			UIButtonFind_Click(null, null);
		}

		private void UIButtonFindByPassportNum_Click(object sender, RoutedEventArgs e) {
			OpOp.FindClient.Login = null;
			OpOp.FindClient.PassportNumber = UIClientPassportNum.Text;
			if (OpOp.FindClient.Execute()) {
				if (Finded != null) Finded();
			}
			IdClient = OpOp.FindClient.IdClient;

			UIClientLogin.Text=OpOp.FindClient.Login;
			UIClientPassportNum.Text = OpOp.FindClient.PassportNumber;

			UIMessage.TextMessage = OpOp.FindClient.Information;
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


		public string Login { get; set; }
		public string LastName { get; protected  set; }
		public string PassportNumber { get; protected set; }

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
					case "PassportNumber":
						if (!OpOp.Verifier.CheckLogin(PassportNumber)) {
							error = OpOp.StringSource.PassportNumberStructureError();
						}
						break;
					case "LastName":
						if (!OpOp.Verifier.CheckLogin(LastName)) {
							error = OpOp.StringSource.NameStructureError();
						}
						break;
				}

				return error;
			}
		}




		public Visibility UIMessageVisibility { get { return UIMessage.Visibility; } set { UIMessage.Visibility = value; } }

		public event RoutedEventHandler UIButtonFindClick { add { UIButtonFind.Click += value; }
			remove { UIButtonFind.Click -= value; }
		}

		public event RoutedEventHandler UIButtonFindPassprortNumClick { add { UIButtonFindPassprortNum.Click += value; }
			remove { UIButtonFindPassprortNum.Click -= value; }
		}

		public event SelectionChangedEventHandler UIListByLastNameSelectedClient { add { UIListByLastName.SelectedClient += value; }
			remove { UIListByLastName.SelectedClient -= value; }
		}

		public event SelectionChangedEventHandler UIFullListSelectedClient {
			add {
				UIFullList.SelectedClient += value;
			}
			remove { UIFullList.SelectedClient -= value; }
		}
	}
}
