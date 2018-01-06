using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UILogic {
	public abstract class BaseOperationGetFullList<T>:BaseOperation {

		public List<int> IdsEntities { get; protected set; }
		public List<string> EntitiesInformation { get; set; }

		public int AmountElements { get; set; }

		protected T FirstElement;
		protected T LastElement;
		protected List<T> CurrentList = new List<T>();
		public bool IsNext { get; set; }
		public bool IsUpdate { get; set; }

		protected IQueryable<T> Entities { get; set; }

		public override bool Execute() {
			if (!Verifier.CheckAmmountElements(AmountElements)) {
				Information = StringSource.AmmountElementsStructureError();
				return false;
			}
			Clear();

			return true;
		}

		protected virtual void Clear() {
			IdsEntities = new List<int>();
			EntitiesInformation = new List<string>();
			Information = "";
		}

	}
}
