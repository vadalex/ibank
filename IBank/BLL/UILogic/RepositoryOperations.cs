using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Entities;
using DAL;

using BLL.ServiceReference1;
using BLL.UILogic;
using BLL.UILogic.Operations;
using BLL.UILogic.Interfaces;

namespace BLL.UILogic {
	public class RepositoryOperations : IGetStringSources, IGetDataSources {

		private Verifier _Verifier;
		public Verifier Verifier {
			get {
				if (_Verifier == null) {
					_Verifier = new Verifier { DataSource=DataSource};
				}
				return _Verifier;
			}
			set { _Verifier = value; }
		}
		
		private DataSources _DataSources;
		public DataSources DataSource {
			get {
				if (_DataSources == null) {
					_DataSources = new DataSources { };
				}
				return _DataSources;
			}
			set { _DataSources = value; }
		}

		private StringSources _StringSource;
		public StringSources StringSource {
			get {
				if (_StringSource == null) {
					_StringSource = new StringSources { DataSource=DataSource};
				}
				return _StringSource;
			}
			set { _StringSource = value; }
		}



		private EditClient _EditClient;
		public EditClient EditClient {
			get {
				if (_EditClient == null) {
					_EditClient = new EditClient { DataSource = DataSource, StringSource = StringSource };
				}
				return _EditClient;
			}
			set { value = _EditClient; }
		}

		private FindCardAccountById _FindCardAccountById;
		public FindCardAccountById FindCardAccountById {
			get {
				if (_FindCardAccountById == null) {
					_FindCardAccountById = new FindCardAccountById { DataSource = DataSource, StringSource = StringSource };
				}
				return _FindCardAccountById;
			}
			set { value = _FindCardAccountById; }
		}

		private FindOperatorById _FindOperatorById;
		public FindOperatorById FindOperatorById {
			get {
				if (_FindOperatorById == null) {
					_FindOperatorById = new FindOperatorById { DataSource = DataSource, StringSource = StringSource };
				}
				return _FindOperatorById;
			}
			set { value = _FindOperatorById; }
		}

		private EditOperator _EditOperator;
		public EditOperator EditOperator {
			get {
				if (_EditOperator == null) {
					_EditOperator = new EditOperator { DataSource = DataSource, StringSource = StringSource };
				}
				return _EditOperator;
			}
			set { value = _EditOperator; }
		}

		private EditCardAccount _EditCardAccount;
		public EditCardAccount EditCardAccount {
			get {
				if (_EditCardAccount == null) {
					_EditCardAccount = new EditCardAccount { DataSource = DataSource, StringSource = StringSource };
				}
				return _EditCardAccount;
			}
			set { value = _EditCardAccount; }
		}

		private EditBankAccount _EditBankAccount;
		public EditBankAccount EditBankAccount {
			get {
				if (_EditBankAccount == null) {
					_EditBankAccount = new EditBankAccount { DataSource = DataSource, StringSource = StringSource };
				}
				return _EditBankAccount;
			}
			set { value = _EditBankAccount; }
		}

		private CheckAutentificationOperator _CheckAutentificationOperator;
		public CheckAutentificationOperator CheckAutentificationOperator {
			get {
				if (_CheckAutentificationOperator == null) {
					_CheckAutentificationOperator = new CheckAutentificationOperator { DataSource = DataSource, StringSource = StringSource };
				}
				return _CheckAutentificationOperator;
			}
			set { value = _CheckAutentificationOperator; }
		}

		private FindClientsByLastName _FindClientsByLastName;
		public FindClientsByLastName FindClientsByLastName {
			get {
				if (_FindClientsByLastName == null) {
					_FindClientsByLastName = new FindClientsByLastName { DataSource = DataSource, StringSource = StringSource };
				}
				return _FindClientsByLastName;
			}
			set { value = _FindClientsByLastName; }
		}

		private FindClientFullList _FindClientFullList;
		public FindClientFullList FindClientFullList {
			get {
				if (_FindClientFullList == null) {
					_FindClientFullList = new FindClientFullList { DataSource = DataSource, StringSource = StringSource };
				}
				return _FindClientFullList;
			}
			set { value = _FindClientFullList; }
		}

		private FindOperatorFullList _FindOperatorFullList;
		public FindOperatorFullList FindOperatorFullList {
			get {
				if (_FindOperatorFullList == null) {
					_FindOperatorFullList = new FindOperatorFullList { DataSource = DataSource, StringSource = StringSource };
				}
				return _FindOperatorFullList;
			}
			set { value = _FindOperatorFullList; }
		}

		private FindCardAccountFullList _FindCardAccountFullList;
		public FindCardAccountFullList FindCardAccountFullList {
			get {
				if (_FindCardAccountFullList == null) {
					_FindCardAccountFullList = new FindCardAccountFullList { DataSource = DataSource, StringSource = StringSource };
				}
				return _FindCardAccountFullList;
			}
			set { value = _FindCardAccountFullList; }
		}

		private FindBankAccountFullList _FindBankAccountFullList;
		public FindBankAccountFullList FindBankAccountFullList {
			get {
				if (_FindBankAccountFullList == null) {
					_FindBankAccountFullList = new FindBankAccountFullList { DataSource = DataSource, StringSource = StringSource };
				}
				return _FindBankAccountFullList;
			}
			set { value = _FindBankAccountFullList; }
		}

		private LockUnlockAccessCardCliete _LockUnlockAccessCardCliete;
		public LockUnlockAccessCardCliete LockUnlockAccessCardCliete {
			get {
				if (_LockUnlockAccessCardCliete == null) {
					_LockUnlockAccessCardCliete = new LockUnlockAccessCardCliete { DataSource = DataSource, StringSource = StringSource };
				}
				return _LockUnlockAccessCardCliete;
			}
			set { value = _LockUnlockAccessCardCliete; }
		}

		private FindCardAccountByNumber _FindCardAccount;
		public FindCardAccountByNumber FindCardAccount {
			get {
				if (_FindCardAccount == null) {
					_FindCardAccount = new FindCardAccountByNumber { DataSource = DataSource, StringSource = StringSource };
				}
				return _FindCardAccount;
			}
			set { value = _FindCardAccount; }
		}

		private FindBankAccountByNumber _FindBankAccount;
		public FindBankAccountByNumber FindBankAccount {
			get {
				if (_FindBankAccount == null) {
					_FindBankAccount = new FindBankAccountByNumber { DataSource = DataSource, StringSource = StringSource };
				}
				return _FindBankAccount;
			}
			set { value = _FindBankAccount; }
		}

		private FindClientByLogin _FindClient;
		public FindClientByLogin FindClient {
			get {
				if (_FindClient == null) {
					_FindClient = new FindClientByLogin { DataSource = DataSource, StringSource = StringSource };
				}
				return _FindClient;
			}
			set { value = _FindClient; }
		}

		private FindClientById _FindClientById;
		public FindClientById FindClientById {
			get {
				if (_FindClientById == null) {
					_FindClientById = new FindClientById { DataSource = DataSource, StringSource = StringSource };
				}
				return _FindClientById;
			}
			set { value = _FindClientById; }
		}

		private FindBankAccountById _FindBankAccountById;
		public FindBankAccountById FindBankAccountById {
			get {
				if (_FindBankAccountById == null) {
					_FindBankAccountById = new FindBankAccountById { DataSource = DataSource, StringSource = StringSource };
				}
				return _FindBankAccountById;
			}
			set { value = _FindBankAccountById; }
		}

		private GetClientInformation _GetClientInformation;
		public GetClientInformation GetClientInformation {
			get {
				if (_GetClientInformation == null) {
					_GetClientInformation = new GetClientInformation { DataSource = DataSource, StringSource = StringSource };
				}
				return _GetClientInformation;
			}
			set { value = _GetClientInformation; }
		}


		private LockUnlockCardAccount _LockUnlockCardAccount;
		public LockUnlockCardAccount LockUnlockCardAccount {
			get {
				if (_LockUnlockCardAccount == null) {
					_LockUnlockCardAccount = new LockUnlockCardAccount { DataSource = DataSource, StringSource = StringSource };
				}
				return _LockUnlockCardAccount;
			}
			set { value = _LockUnlockCardAccount; }
		}


		private AddCardAccount _RegistrationCardAccount;
		public AddCardAccount RegistrationCardAccount {
			get {
				if (_RegistrationCardAccount == null) {
					_RegistrationCardAccount = new AddCardAccount { DataSource = DataSource, StringSource = StringSource };
				}
				return _RegistrationCardAccount;
			}
			set { value = _RegistrationCardAccount; }
		}


		private AddClient _RegistrationClient;
		public AddClient RegistrationClient {
			get {
				if (_RegistrationClient == null) {
					_RegistrationClient = new AddClient { DataSource = DataSource, StringSource = StringSource };
				}
				return _RegistrationClient;
			}
			set { value = _RegistrationClient; }
		}


		private RemoveCardAccount _RemoveCardAccount;
		public RemoveCardAccount RemoveCardAccount {
			get {
				if (_RemoveCardAccount == null) {
					_RemoveCardAccount = new RemoveCardAccount { DataSource = DataSource, StringSource = StringSource };
				}
				return _RemoveCardAccount;
			}
			set { value = _RemoveCardAccount; }
		}












		private GetNBRBCurrencies _GetNBRBCurrencies;
		public GetNBRBCurrencies GetNBRBCurrencies {
			get {
				if (_GetNBRBCurrencies == null) {
					_GetNBRBCurrencies = new GetNBRBCurrencies { DataSource = DataSource, StringSource = StringSource };
				}
				return _GetNBRBCurrencies;
			}
			set { value = _GetNBRBCurrencies; }
		}


		private AddBankAccount _RegistrationBankAccount;
		public AddBankAccount AddBankAccount {
			get {
				if (_RegistrationBankAccount == null) {
					_RegistrationBankAccount = new AddBankAccount { DataSource = DataSource, StringSource = StringSource };
				}
				return _RegistrationBankAccount;
			}
			set { _RegistrationBankAccount = value; }
		}

		private IncreaceBalanceBankAccount _IncreaceBalanceBankAccount;
		public IncreaceBalanceBankAccount IncreaceBalanceBankAccount {
			get {
				if (_IncreaceBalanceBankAccount == null) {
					_IncreaceBalanceBankAccount = new IncreaceBalanceBankAccount { DataSource = DataSource, StringSource = StringSource };
				}
				return _IncreaceBalanceBankAccount;
			}
			set { _IncreaceBalanceBankAccount = value; }
		}

		private RemoveBankAccount _RemoveBankAccount;
		public RemoveBankAccount RemoveBankAccount {
			get {
				if (_RemoveBankAccount == null) {
					_RemoveBankAccount = new RemoveBankAccount { DataSource = DataSource, StringSource = StringSource };
				}
				return _RemoveBankAccount;
			}
			set { _RemoveBankAccount = value; }
		}






		private CheckAutentificationAdmin _CheckAutentificationAdmin;
		public CheckAutentificationAdmin CheckAutentificationAdmin {
			get {
				if (_CheckAutentificationAdmin == null) {
					_CheckAutentificationAdmin = new CheckAutentificationAdmin { DataSource = DataSource, StringSource = StringSource };
				}
				return _CheckAutentificationAdmin;
			}
			set { value = _CheckAutentificationAdmin; }
		}


		private EditAdminPassword _EditAdminPassword;
		public EditAdminPassword EditAdminPassword {
			get {
				if (_EditAdminPassword == null) {
					_EditAdminPassword = new EditAdminPassword { DataSource = DataSource, StringSource = StringSource };
				}
				return _EditAdminPassword;
			}
			set { value = _EditAdminPassword; }
		}

		private EditCurrencies _EditCurrencies;
		public EditCurrencies EditCurrencies {
			get {
				if (_EditCurrencies == null) {
					_EditCurrencies = new EditCurrencies { DataSource = DataSource, StringSource = StringSource }; ;
				}
				return _EditCurrencies;
			}
			set { value = _EditCurrencies; }
		}

		private FindOperatorByLogin _FindOperator;
		public FindOperatorByLogin FindOperator {
			get {
				if (_FindOperator == null) {
					_FindOperator = new FindOperatorByLogin { DataSource = DataSource, StringSource = StringSource };
				}
				return _FindOperator;
			}
			set { value = _FindOperator; }
		}


		private GetAllCurrencies _GetAllCurrencies;
		public GetAllCurrencies GetAllCurrencies {
			get {
				if (_GetAllCurrencies == null) {
					_GetAllCurrencies = new GetAllCurrencies { DataSource = DataSource, StringSource = StringSource };
				}
				return _GetAllCurrencies;
			}
			set { value = _GetAllCurrencies; }
		}


		private AddOperator _RegistrationOperator;
		public AddOperator RegistrationOperator {
			get {
				if (_RegistrationOperator == null) {
					_RegistrationOperator = new AddOperator { DataSource = DataSource, StringSource = StringSource };
				}
				return _RegistrationOperator;
			}
			set { value = _RegistrationOperator; }
		}


		private RemoveClient _RemoveClient;
		public RemoveClient RemoveClient {
			get {
				if (_RemoveClient == null) {
					_RemoveClient = new RemoveClient { DataSource = DataSource, StringSource = StringSource };
				}
				return _RemoveClient;
			}
			set { value = _RemoveClient; }
		}


		private RemoveOperator _RemoveOperator;
		public RemoveOperator RemoveOperator {
			get {
				if (_RemoveOperator == null) {
					_RemoveOperator = new RemoveOperator { DataSource = DataSource, StringSource = StringSource };
				}
				return _RemoveOperator;
			}
			set { value = _RemoveOperator; }
		}



	


	}
}
