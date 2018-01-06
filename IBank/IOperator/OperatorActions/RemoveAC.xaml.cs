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

namespace IOperator.OperatorActions {
    /// <summary>
    /// Логика взаимодействия для RemoveAC.xaml
    /// </summary>
    public partial class RemoveAC : UserControl,IGetBLL {
        public RemoveAC() {
            InitializeComponent();
        }


        private void UIRemoveAC_Click(object sender, RoutedEventArgs e) {
			OpOp.RemoveCardAccount.IdCardAccount=UIFindAC.IdCardAccount;
			if (OpOp.RemoveCardAccount.Execute()) {
				UIFindAC.UpdateFullList();
			}
			UIFindAC.ClearUI();
			UIMessage.TextMessage = OpOp.RemoveCardAccount.Information;
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
