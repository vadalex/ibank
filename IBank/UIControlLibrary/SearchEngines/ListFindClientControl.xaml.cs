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
using UIControlLibrary.Sources;

namespace UIControlLibrary.SearchEngines {
	/// <summary>
	/// Логика взаимодействия для ListFindClientControl.xaml
	/// </summary>
	public partial class ListFindClientControl : UserControl,IDataErrorInfo,IGetBLL {

		static ListFindClientControl() {
			ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(List<string>), typeof(ListFindClientControl));
		}
		public static readonly DependencyProperty ItemsSourceProperty;
		public List<string> ItemsSource {
			get { return (List<string>)GetValue(ItemsSourceProperty); }
			set { SetValue(ItemsSourceProperty, value); }
		}
		

		public int AmmountElements { get; set; }
		public int SelectedIndex { get { return UIClientsList.SelectedIndex; } }

		public ListFindClientControl() {
			InitializeComponent();
			UIAmmountElements.Text = OpOp.DataSource.DefaultAmmountElements.ToString();
			UIClientsList.DataContextChanged += UIClientsList_DataContextChanged;
		}

		void UIClientsList_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {
			UIClientsList.SelectedItem = null;
		}

		public event SelectionChangedEventHandler SelectedClient {
			add { UIClientsList.SelectionChanged += value; }
			remove { UIClientsList.SelectionChanged -= value; }
		}

		public event RoutedEventHandler FindPrev {
			add { UIFindPrevBt.Click += value; }
			remove { UIFindPrevBt.Click -= value; }
		}

		public event RoutedEventHandler FindNext {
			add { UIFindNextBt.Click += value; }
			remove { UIFindNextBt.Click -= value; }
		}


		public string Error {
			get { return "Error"; }
		}

		public string this[string columnName] {
			get {
				string error = String.Empty;
				switch (columnName) {
					case "AmmountElements":
						if (!OpOp.Verifier.CheckAmmountElements(AmmountElements)) {
							error = OpOp.StringSource.AmmountElementsStructureError();
						}
						break;
				}
				return error;
			}			
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
