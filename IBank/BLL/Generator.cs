using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL {
	public class Generator  {

        public Generator() {
            Rand = new Random();
			CharCount = 4;
			NumberCount = 2;
        }

        protected Random Rand;

		public int CharCount { get; set; }
		public int NumberCount { get; set; }

        public string StringGenerate() {
            var rs = "";
            var startvalue=(int)'a';
            for (int i = 0; i < CharCount;i++ ) {
                rs += (char)(startvalue + Rand.Next(26));
            }

            return rs;
        }

		public long NumberGenerate(){
			long vl =(long)( Rand.NextDouble()*Math.Pow(10,NumberCount));
			while (vl.ToString().Length < NumberCount) {
				vl *= 10;
			}
			return vl;
		}

		public string NumberAndStringGenerate() {
			return NumberGenerate() + StringGenerate();
		}

    }
}
