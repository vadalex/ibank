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
using System.Drawing.Printing;
using System.Drawing;

namespace IOperator.OperatorActions {
    /// <summary>
    /// Логика взаимодействия для GetClietInfo.xaml
    /// </summary>
    public partial class GetClientInfo : UserControl,IGetBLL{
        public GetClientInfo() {
			Loaded += GetClientInfo_Loaded;
            InitializeComponent();
        }

		private PrintDocument PD = new PrintDocument();
		private PrinterSettings PS=new PrinterSettings();

		//private bool IsErrorPrinting = false;

		public void GetClientInfo_Loaded(object sender, RoutedEventArgs e) {
			UIFindClient.UIMessageVisibility = Visibility.Collapsed;
			UIFindClient.UIButtonFindClick += FindClient;
			UIFindClient.UIButtonFindPassprortNumClick += FindClient;
			UIFindClient.UIListByLastNameSelectedClient += FindClient;
			UIFindClient.UIFullListSelectedClient += FindClient;

			
			PD.PrinterSettings = PS;
			Button bt = null;
			UIPrinters.Children.Clear();
			try {
				foreach (var el in PrinterSettings.InstalledPrinters) {
					bt = new Button { Content = el.ToString(), };
					bt.Click += UIbtPrint_Click;
					UIPrinters.Children.Add(bt);
				}
				PD.PrintPage += PD_PrintPage;
			} catch (System.ComponentModel.Win32Exception ex) {
				MessageBox.Show(ex.Message + "\nВозможно служба печати не запущена.", "Сервис печати недоступен.",
				MessageBoxButton.OK, MessageBoxImage.Information);	
			}
		}

		void PD_PrintPage(object sender, PrintPageEventArgs e) {
			  Font PrintFont = new Font(
				  "Times New Roman", 3, 
				  System.Drawing.FontStyle.Regular, 
				  GraphicsUnit.Millimeter
				  );
			  e.Graphics.DrawString(
				  OpOp.GetClientInformation.Information,
				  PrintFont, 
				  System.Drawing.Brushes.Black, 
				  new PointF(10, 10)
			);

		}


        public void FindClient(object sender, RoutedEventArgs e) {

			OpOp.GetClientInformation.IdClient = UIFindClient.IdClient;
			if (OpOp.GetClientInformation.Execute()) {
				if (UIPrinters.Children.Count>0) {
					UIPrinters.Visibility=Visibility.Visible;
					UItbPrinters.Visibility=Visibility.Visible;
				}
			}else{
					UIPrinters.Visibility = Visibility.Collapsed;
					UItbPrinters.Visibility = Visibility.Collapsed;
			}
			UIMessage.TextMessage = OpOp.GetClientInformation.Information;
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

		private void UIbtPrint_Click(object sender, RoutedEventArgs e) {

			var bt = sender as Button;

			PS.PrinterName = bt.Content as string;
			UIMessage.TextMessage += bt.Content;
			PD.Print();


		}


		

    }
}
