using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace UIControlLibrary.Sources {
	public class ExeptionHandler {

		public void Handler(Exception ex) {

			if (ex is System.ServiceModel.EndpointNotFoundException) {
				MessageBox.Show(ex.Message + "\nВозможно нет доступа к интернету.", "Сервис недоступен.",
				MessageBoxButton.OK, MessageBoxImage.Exclamation);
			} else if (ex is System.ComponentModel.Win32Exception) {
				MessageBox.Show(ex.Message + "\nВозможно служба печати не запущена.", "Сервис печати недоступен.",
				MessageBoxButton.OK, MessageBoxImage.Information);
			} else if (ex is System.ServiceModel.FaultException) {
				MessageBox.Show(ex.Message , "Не удалось открыть соединение с веб-сервером.",
				MessageBoxButton.OK, MessageBoxImage.Information);
			} else {
				var ss = new BLL.UILogic.StringSources();
				MessageBox.Show(ex.Message, ss.HandlAllExeptions(),
					MessageBoxButton.OK, MessageBoxImage.Error);
			}
			

		}

	}
}
