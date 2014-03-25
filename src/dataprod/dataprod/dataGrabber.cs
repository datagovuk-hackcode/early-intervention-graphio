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
			public async static Task<jsonTemplates.wfprefict> wfpredict(string soc, string minYear, string maxYear)
			{
				var y = await HttpGet("http://api.lmiforall.org.uk/api/v1/wf/predict?soc=" + soc + "&minYear=" + minYear + "&maxYear=" + maxYear);
				var z = JsonConvert.DeserializeObject<jsonTemplates.wfprefict>(y);
				return z;
			}
			public async static Task<List<jsonTemplates.socSearch>> socSearch(string profession)
			{
				var y = await HttpGet("http://api.lmiforall.org.uk/api/v1/soc/search?q=" + profession);
				var z = JsonConvert.DeserializeObject<List<jsonTemplates.socSearch>>(y);
				return z;
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
