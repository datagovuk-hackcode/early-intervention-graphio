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
			public static jsonTemplates.wfprefict wfpredict(string soc, string minYear, string maxYear)
			{
				
				return new jsonTemplates.wfprefict();
			}
			public static string socSearch(string profession)
			{
				var y = HttpGet("http://api.lmiforall.org.uk/api/v1/soc/search?q" + profession);
				var dummyAnon = new[] {new { soc = 0 }};

				var z = JsonConvert.DeserializeAnonymousType(y, dummyAnon);
				return "0000";
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
