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
using UIControlLibrary.Components;
using System.Threading;
using BLL;
using IAdmin.AdminActions;
using IAdmin.Path;

namespace IAdmin {
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
	public partial class MainWindow : Window, IFrame, UIControlLibrary.Interfaces.IGetBLL {
        public MainWindow() {
            InitializeComponent();
			
			//R///////////E//////M/////O///V///////E/
			//SetFrame(new RootControl { });
			/////////////////////////////////////////
        }

        public void SetFrame(UIElement el) {
            var mw = Application.Current.MainWindow as MainWindow;
            if (mw != null) {
                mw.UIMainFrame.Children.Clear();
                mw.UIMainFrame.Children.Add(el);
            }
        }


		protected BLL.UILogic.RepositoryOperations _OpOp =
			new BLL.UILogic.RepositoryOperations {
				StringSource = new BLL.UILogic.StringSources { },
				DataSource = new BLL.UILogic.DataSources { },
				Verifier = new BLL.UILogic.Verifier { }
			};
		public BLL.UILogic.RepositoryOperations OpOp {
			get { return _OpOp; }
		}
	}
}
