using System;
using System.Collections.Generic;
using System.Dynamic;
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

		public class essRegion
		{
			public double percentSSV { get; set; }
			public double percentHTF { get; set; }
			public int soc { get; set; }
			public double percentHTFisSSV { get; set; }
			public int reliability { get; set; }
			public Region region { get; set; }

			public class Region
			{
				public string name { get; set; }
				public int code { get; set; }
			}
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

		public class ToastWaffle
		{
			public List<Datum> data { get; set; }
			public string status { get; set; }

			public class Level1Agency
			{
				public int id { get; set; }
				public int level { get; set; }
				public string name { get; set; }
			}

			public class Level2Agency
			{
				public int id { get; set; }
				public int level { get; set; }
				public string name { get; set; }
			}

			public class OutcomeCategory
			{
				public int id { get; set; }
				public string name { get; set; }
			}

			public class OutcomeDetail
			{
				public int id { get; set; }
				public string name { get; set; }
			}

			public class Unit
			{
				public int id { get; set; }
				public string name { get; set; }
			}

			public class Datum
			{
				public string code { get; set; }
				public string comment { get; set; }
				public string confidence { get; set; }
				public double cost { get; set; }
				public string details { get; set; }
				public int id { get; set; }
				public Level1Agency level_1_agency { get; set; }
				public Level2Agency level_2_agency { get; set; }
				public OutcomeCategory outcome_category { get; set; }
				public OutcomeDetail outcome_detail { get; set; }
				public string source { get; set; }
				public string source_url { get; set; }
				public Unit unit { get; set; }
				public int year { get; set; }
			}
		}
	}

	public class Templates
	{
		public class DetailedYear
		{
			public int year { get; set; }
			public Dictionary<int, double> employments { get; set; }
		}
	}
}
