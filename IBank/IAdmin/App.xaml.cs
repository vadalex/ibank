using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;


namespace IAdmin {
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application {

		void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) {
			// Process unhandled exception
			new UIControlLibrary.Sources.ExeptionHandler().Handler(e.Exception);
			// Prevent default unhandled exception processing
			e.Handled = true;
		}

		//private void Application_Startup(object sender, StartupEventArgs e) {
		//	AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
		//}

		//void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
		//	Exception ex = e.ExceptionObject as Exception;
		//	MessageBox.Show(ex.Message, "Uncaught Thread Exception", MessageBoxButton.OK, MessageBoxImage.Error);
		//}


	}



}
