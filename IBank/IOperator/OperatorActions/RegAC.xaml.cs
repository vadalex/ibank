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
    /// Логика взаимодействия для RegAC.xaml
    /// </summary>
    public partial class RegAC : UserControl,IClearUI,IGetBLL {
        public RegAC() {
            InitializeComponent();
			Loaded += RegAC_Loaded;
			UICardAccountForm.CollapsedAdminEdition();
			UIFindAB.Finded = Finded;
		}

		public void Finded() {
			UIMessage.ClearUI();
		}
		
		void RegAC_Loaded(object sender, RoutedEventArgs e) {
			UICardAccountForm.ExpiredDate = OpOp.DataSource.CardAccountDefaultExpiredDate;
			UICardAccountForm.Status = OpOp.DataSource.CardAccountStatuses.ElementAt(0);
			UICardAccountForm.Namee = OpOp.DataSource.CardAccountDefaultNames.ElementAt(0);
		}

        public void ClearUI() {
			UICardAccountForm.ClearUI();
			ClearUIThis();
        }
		public void ClearUIThis() {
			//UIFindAB.ClearUI();
			UICardAccountForm.Type = "";
			UICardAccountForm.Status = OpOp.DataSource.CardAccountStatuses.ElementAt(0);
			UICardAccountForm.Name = OpOp.DataSource.CardAccountDefaultNames.ElementAt(0);
			UICardAccountForm.ExpiredDate = OpOp.DataSource.CardAccountDefaultExpiredDate;
		}

        private void UIButtonRegAC_Click(object sender, RoutedEventArgs e) {
			OpOp.RegistrationCardAccount.CardAccountStatus = OpOp.DataSource.CardStatusUnLocked;
			OpOp.RegistrationCardAccount.CardAccountName = UICardAccountForm.Namee;
			OpOp.RegistrationCardAccount.CardType = UICardAccountForm.Type;
			OpOp.RegistrationCardAccount.IdBankAccount = UIFindAB.IdBankAccount;
			OpOp.RegistrationCardAccount.ExpiredDate = UICardAccountForm.ExpiredDate;
			if (OpOp.RegistrationCardAccount.Execute()) {

				var rsmb=MessageBox.Show(OpOp.RegistrationCardAccount.Information+"\n"+
					OpOp.StringSource.YouWantToNextRegistredCardAccount(), 
				OpOp.StringSource.Successfully(), MessageBoxButton.YesNo, MessageBoxImage.Information);

				if (rsmb == MessageBoxResult.Yes) {
					ClearUIThis();
					UIMessage.TextMessage = "";
					return;
				} else {
					var ci = new GetClientInfo { };
					ci.GetClientInfo_Loaded(null, null);
					ci.UIFindClient.FindById(OpOp.RegistrationCardAccount.IdClient);
					ci.FindClient(null, null);
					(Application.Current.MainWindow as IFrame).SetFrame(ci);
					return;
				}

			}
			UIMessage.TextMessage = OpOp.RegistrationCardAccount.Information;
			
        }

        private void UIToInfoClient_Click(object sender, RoutedEventArgs e) {
            var ci = new GetClientInfo { };
			ci.UIFindClient.FindById(OpOp.RegistrationCardAccount.IdClient);
            ci.FindClient(sender, e);
            (Application.Current.MainWindow as IFrame).SetFrame(ci);
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
