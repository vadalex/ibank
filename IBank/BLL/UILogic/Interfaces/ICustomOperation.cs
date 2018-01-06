using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.UILogic.Interfaces {
	public interface ICustomOperation:IGetStringSources,IUIInformer,IErrorInformer {
		bool Execute();
	}
}
