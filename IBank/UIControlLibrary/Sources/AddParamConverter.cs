using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace UIControlLibrary.Sources {
	//[ValueConversion(typeof(double), typeof(double))]
	public class AddParamConverter : IValueConverter,IMultiValueConverter {
		
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return (double)value+double.Parse((string)parameter);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotSupportedException("Never used.");
		}

		public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			//double rs=(double)values[0];
			//for (int i = 1; i < values.Length; i++) {
			//	rs -= (double)values[i];
			//}
			var a = values[0];
			
			return a;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}
