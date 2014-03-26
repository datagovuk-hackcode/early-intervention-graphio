using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Networking.Sockets;
using Newtonsoft.Json;

namespace dataprod
{
	static class dataGrabber
	{
		public static class LMI
		{
			public async static Task<jsonTemplates.wfpredict> wfpredict(string soc, string minYear, string maxYear)
			{
				var y = await HttpGet("http://api.lmiforall.org.uk/api/v1/wf/predict?soc=" + soc + "&minYear=" + minYear + "&maxYear=" + maxYear);
				var z = JsonConvert.DeserializeObject<jsonTemplates.wfpredict>(y);
				return z;
			}

			public async static Task<jsonTemplates.wffilterpredict> wffilterpredict(string soc, string filter, string minYear, string maxYear)
			{
				var url = "http://api.lmiforall.org.uk/api/v1/wf/predict/breakdown/" + filter + "?soc=" + soc + "&minYear=" + minYear +
				          "&maxYear=" + maxYear;
				var y = await HttpGet(url);
				var z = JsonConvert.DeserializeObject<jsonTemplates.wffilterpredict>(y);
				return z;
			}

			public async static Task<List<jsonTemplates.socSearch>> socSearch(string profession)
			{
				var y = await HttpGet("http://api.lmiforall.org.uk/api/v1/soc/search?q=" + profession);
				var z = JsonConvert.DeserializeObject<List<jsonTemplates.socSearch>>(y);
				return z;
			}

			public async static Task<jsonTemplates.essRegion> essRegion(int regionNumber, string soc)
			{
				var url = "http://api.lmiforall.org.uk/api/v1/ess/region/" + regionNumber + "/" + soc;
				var y = await HttpGet(url);
				var z = JsonConvert.DeserializeObject<jsonTemplates.essRegion>(y);
				return z;
			}
			public async static Task<string> essRegionJason(int regionNumber, string soc)
			{
				var url = "http://api.lmiforall.org.uk/api/v1/ess/region/" + regionNumber + "/" + soc + "?coarse=true";
				var y = await HttpGet(url);
				dynamic z = JsonConvert.DeserializeObject(y);
				return JsonConvert.SerializeObject(z, Formatting.Indented);
			}
		}

		public static class ToastWaffle
		{
			public static async Task<jsonTemplates.ToastWaffle> GetFinancial()
			{
				var url = "http://unitcost.toastwaffle.com/api/";
				var y = await HttpGet(url);
				var z = JsonConvert.DeserializeObject<jsonTemplates.ToastWaffle>(y);
				return z; //untested
			} 
		}
		private static async Task<string> HttpGet(string urlIn)
		{
			var request = (HttpWebRequest)WebRequest.Create(urlIn);
			request.Accept = "application/json";

			WebResponse response = await request.GetResponseAsync();

			string temp;

			using (Stream stream = response.GetResponseStream())
			using (var reader = new StreamReader(stream))
				temp = reader.ReadToEnd();
			return temp;
		}
	}
}
