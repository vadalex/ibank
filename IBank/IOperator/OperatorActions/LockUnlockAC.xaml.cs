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
    /// Логика взаимодействия для LockUnlockAC.xaml
    /// </summary>
    public partial class LockUnlockAC : UserControl,IGetBLL {
        public LockUnlockAC() {
            InitializeComponent();
			UIFindAC.Finded = Finded;
		}

		public void Finded() {
			UIMessage.ClearUI();
		}

        private void UIButtonLock_Click(object sender, RoutedEventArgs e) {
			OpOp.LockUnlockCardAccount.IdCardAccount=UIFindAC.IdCardAccount;
			if (OpOp.LockUnlockCardAccount.Execute()) {
				UIFindAC.UpdateFullList();
			}
			UIMessage.TextMessage = OpOp.LockUnlockCardAccount.Information;
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
