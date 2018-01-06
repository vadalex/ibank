using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIControlLibrary.Interfaces {
	public interface IGetBLL {
		BLL.UILogic.RepositoryOperations OpOp { get; }
	}
}
