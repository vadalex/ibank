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
using UIControlLibrary.Interfaces;
using System.ComponentModel;

namespace IOperator.OperatorActions {
    /// <summary>
    /// Логика взаимодействия для RegClient.xaml
    /// </summary>
    public partial class RegClient : UserControl,IGetBLL {
		
        public RegClient() {
            InitializeComponent();

			Loaded += RegClient_Loaded;			
        }

		void RegClient_Loaded(object sender, RoutedEventArgs e) {
			UIClientForm.CollapsedAdminEdition();
		}
       

        private void UIButtonReg_Click(object sender, RoutedEventArgs e) {
			OpOp.RegistrationClient.EMail = UIClientForm.EMail;
			OpOp.RegistrationClient.Address = UIClientForm.Address;
			OpOp.RegistrationClient.PassportNumber = UIClientForm.PassportNum;
			OpOp.RegistrationClient.FirstName = UIClientForm.FirstName;
			OpOp.RegistrationClient.LastName = UIClientForm.LastName;
			OpOp.RegistrationClient.MiddleName = UIClientForm.MiddleName;
			OpOp.RegistrationClient.Country = UIClientForm.LandName;
			if (OpOp.RegistrationClient.Execute()) {

				var mbrs = MessageBox.Show(OpOp.RegistrationClient.Information + "\n" + OpOp.StringSource.YouWantToRegistredBankAccount(), OpOp.StringSource.Successfully(), MessageBoxButton.YesNo, MessageBoxImage.Information);
				if (mbrs == MessageBoxResult.Yes) {
					var rb = new RegBankAccount { };
					rb.UIFindClient.FindById(OpOp.RegistrationClient.IdClient);
					(Application.Current.MainWindow as IFrame).SetFrame(rb);
					return;
				}

				var ci = new GetClientInfo { };
				ci.GetClientInfo_Loaded(null, null);
				ci.UIFindClient.FindByLogin(OpOp.RegistrationClient.Login);
				ci.FindClient(null, null);				
				(Application.Current.MainWindow as IFrame).SetFrame(ci);
				return;
			}

			UIMessage.TextMessage = OpOp.RegistrationClient.Information;
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
