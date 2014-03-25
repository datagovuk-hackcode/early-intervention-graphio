using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataprod
{
	public class jsonTemplates
	{
		public class wfpredict
		{
			public int soc { get; set; }
			public List<PredictedEmployment> predictedEmployment { get; set; }

			public class PredictedEmployment
			{
				public int year { get; set; }
				public double employment { get; set; }
			}
		}

		public class socSearch
		{
			public int soc { get; set; }
			public string title { get; set; }
			public string description { get; set; }
			public string qualifications { get; set; }
		}

		public class wffilterpredict
		{
			public int soc { get; set; }
			public string breakdown { get; set; }
			public List<PredictedEmployment> predictedEmployment { get; set; }

			public class Breakdown
			{
				public int code { get; set; }
				public string name { get; set; }
				public double employment { get; set; }
			}

			public class PredictedEmployment
			{
				public int year { get; set; }
				public List<Breakdown> breakdown { get; set; }
			}
		}
	}
}
