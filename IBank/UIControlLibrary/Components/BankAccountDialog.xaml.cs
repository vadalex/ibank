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
using System.Windows.Shapes;

namespace UIControlLibrary.Components {
	/// <summary>
	/// Логика взаимодействия для BankAccountDialog.xaml
	/// </summary>
	public partial class BankAccountDialog : Window {
		public BankAccountDialog() {
			InitializeComponent();
		}

		public int Result { get; set; }

		public string TextMessage { get { return UIMessage.TextMessage; } set { UIMessage.TextMessage = value; } }

		private void Button_Click(object sender, RoutedEventArgs e) {
			Result = 1;
			Close();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e) {
			Result = 2;
			Close();
		}

		private void Button_Click_2(object sender, RoutedEventArgs e) {
			Result = 3;
			Close();
		}

		private void Button_Click_3(object sender, RoutedEventArgs e) {
			Result = 4;
			Close();
		}
	}
}
