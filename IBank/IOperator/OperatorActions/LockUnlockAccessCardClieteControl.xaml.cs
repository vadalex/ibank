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

namespace IOperator.OperatorActions {
	/// <summary>
	/// Логика взаимодействия для LockUnlockAccessCardClieteControl.xaml
	/// </summary>
	public partial class LockUnlockAccessCardClieteControl : UserControl,IGetBLL  {
		public LockUnlockAccessCardClieteControl() {
			InitializeComponent();
			UIFindClient.Finded = Finded;
		}

		public void Finded() {
			UIMessage.ClearUI();
		}

		private void UIButtonLock_Click(object sender, RoutedEventArgs e) {
			OpOp.LockUnlockAccessCardCliete.IdClient = UIFindClient.IdClient;
			if (OpOp.LockUnlockAccessCardCliete.Execute()) {
				UIFindClient.UpdateFullList();
			}
			UIMessage.TextMessage = OpOp.LockUnlockAccessCardCliete.Information;
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
