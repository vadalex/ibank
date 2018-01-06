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
using UIControlLibrary.Interfaces;

namespace UIControlLibrary.Components {
    /// <summary>
    /// Логика взаимодействия для Message.xaml
    /// </summary>
    public partial class Message : UserControl, UIControlLibrary.Interfaces.IClearUI,IGetBLL {



        public Message() {
            InitializeComponent();
        }

        public string TextMessage { get { return UIMessage.Text; } set {
			UIMessage.Text = value; 
		} }



		public void ClearUI() {
			UIMessage.Clear();
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
