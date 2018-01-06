using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BLL.UILogic.Interfaces;

namespace BLL.UILogic {
	public abstract class BaseOperation : ICustomOperation, IGetStringSources, IGetDataSources {

		public BaseOperation(){
		}


		private Verifier _Verifier;
		public Verifier Verifier {
			get {
				if (_Verifier == null) {
					_Verifier = new Verifier { DataSource = DataSource };
				}
				return _Verifier;
			}
			set { _Verifier = value; }
		}

		public abstract bool Execute();


		private DataSources _DataSources;
		public DataSources DataSource {
			get {
				if (_DataSources == null) {
					_DataSources = new DataSources();
				}
				return _DataSources;
			}
			set { _DataSources = value; }
		}


		private StringSources _StringSource;
		public StringSources StringSource {
			get {
				if (_StringSource == null) {
					_StringSource = new StringSources { DataSource = DataSource };
				}
				return _StringSource;
			}
			set { _StringSource = value; }
		}


		public string Information { get; set; }

		public string ErrorInformation { get; set; }



	}
}
