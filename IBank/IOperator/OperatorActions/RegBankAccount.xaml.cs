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
using UIControlLibrary.Components;

namespace IOperator.OperatorActions {
	/// <summary>
	/// Логика взаимодействия для RegBankAccount.xaml
	/// </summary>
	public partial class RegBankAccount : UserControl,IGetBLL {
		public RegBankAccount() {
			InitializeComponent();
			UIBankAccountForm.CollapsedBankAccountAdminEdition();
			UIFindClient.Finded = Finded;
		}

		public void Finded() {
			UIMessage.ClearUI();
		}

		private void UIButtonRegBankAccount_Click(object sender, RoutedEventArgs e) {
			OpOp.AddBankAccount.Balance = "0";
			OpOp.AddBankAccount.IdCurrency = UIBankAccountForm.IdCurrency;
			OpOp.AddBankAccount.IdCustomer = UIFindClient.IdClient;
			if (OpOp.AddBankAccount.Execute()) {

				var dg = new BankAccountDialog();
				dg.TextMessage = OpOp.AddBankAccount.Information;
				dg.Owner = Application.Current.MainWindow;
				dg.ShowDialog();
				var rs = dg.Result;
				switch (rs) {
					case 1:
						var bb = new EditBankAccount();
						bb.UIFindBankAccount.FindByNumber(OpOp.AddBankAccount.AccountNumber);
						(Application.Current.MainWindow as IFrame).SetFrame(bb);
						return;

					case 2:
						var aa = new RegAC();
						aa.UIFindAB.FindByNumber(OpOp.AddBankAccount.AccountNumber);
						(Application.Current.MainWindow as IFrame).SetFrame(aa);
						return;

					case 3:
						var rb = new RegBankAccount { };
						rb.UIFindClient.FindById(OpOp.AddBankAccount.IdCustomer);
						(Application.Current.MainWindow as IFrame).SetFrame(rb);
						return;
				}
				var ci = new GetClientInfo();
				ci.GetClientInfo_Loaded(null, null);
				ci.UIFindClient.FindById(OpOp.AddBankAccount.IdCustomer);
				ci.FindClient(null, null);
				(Application.Current.MainWindow as IFrame).SetFrame(ci);

				UIBankAccountForm.ClearUI();

			}
			UIMessage.TextMessage = OpOp.AddBankAccount.Information;
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
