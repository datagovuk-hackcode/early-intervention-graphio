#region

using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

#endregion

namespace dataprod
{
	internal static class dataGrabber
	{
		private static async Task<string> HttpGet(string urlIn)
		{
			var request = (HttpWebRequest) WebRequest.Create(urlIn);
			request.Accept = "application/json";

			WebResponse response = await request.GetResponseAsync();

			string temp;

			using (Stream stream = response.GetResponseStream())
			using (var reader = new StreamReader(stream))
				temp = reader.ReadToEnd();
			return temp;
		}

		public static class LMI
		{
			public static async Task<jsonTemplates.wfpredict> wfpredict(string soc, string minYear, string maxYear)
			{
				string y =
					await
						HttpGet("http://api.lmiforall.org.uk/api/v1/wf/predict?soc=" + soc + "&minYear=" + minYear + "&maxYear=" + maxYear);
				var z = JsonConvert.DeserializeObject<jsonTemplates.wfpredict>(y);
				return z;
			}

			public static async Task<jsonTemplates.wffilterpredict> wffilterpredict(string soc, string filter, string minYear,
				string maxYear)
			{
				string url = "http://api.lmiforall.org.uk/api/v1/wf/predict/breakdown/" + filter + "?soc=" + soc + "&minYear=" +
				             minYear +
				             "&maxYear=" + maxYear;
				string y = await HttpGet(url);
				var z = JsonConvert.DeserializeObject<jsonTemplates.wffilterpredict>(y);
				return z;
			}

			public static async Task<List<jsonTemplates.socSearch>> socSearch(string profession)
			{
				string y = await HttpGet("http://api.lmiforall.org.uk/api/v1/soc/search?q=" + profession);
				var z = JsonConvert.DeserializeObject<List<jsonTemplates.socSearch>>(y);
				return z;
			}

			public static async Task<jsonTemplates.essRegion> essRegion(int regionNumber, string soc)
			{
				string url = "http://api.lmiforall.org.uk/api/v1/ess/region/" + regionNumber + "/" + soc;
				string y = await HttpGet(url);
				var z = JsonConvert.DeserializeObject<jsonTemplates.essRegion>(y);
				return z;
			}

			public static async Task<string> essRegionJason(int regionNumber, string soc)
			{
				string url = "http://api.lmiforall.org.uk/api/v1/ess/region/" + regionNumber + "/" + soc + "?coarse=true";
				string y = await HttpGet(url);
				dynamic z = JsonConvert.DeserializeObject(y);
				return JsonConvert.SerializeObject(z, Formatting.Indented);
			}

			public static async Task<string> reverseSOC(string soc)
			{
				string url = "http://api.lmiforall.org.uk/api/v1/soc/code/" + soc;
				string y = await HttpGet(url);
				var z = JsonConvert.DeserializeObject<jsonTemplates.reverseSOC>(y);
				return z.title;
			}
		}

		public static class ToastWaffle
		{
			public static async Task<jsonTemplates.ToastWaffle> GetFinancial()
			{
				string url = "http://unitcost.toastwaffle.com/api/";
				string y = await HttpGet(url);
				var z = JsonConvert.DeserializeObject<jsonTemplates.ToastWaffle>(y);
				return z; //untested
			}
		}
	}
}