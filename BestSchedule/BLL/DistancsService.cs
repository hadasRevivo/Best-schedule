using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL
{
   public  class DistancsService
    {
        public static int GetDystance(string origin,string dest)
        {
            try
            {
                string key = "AIzaSyBNVjEXhyDOUvcCECJFY5x_OGKt38dxVBk";
                string uri = $"https://maps.googleapis.com/maps/api/distancematrix/xml?key={key}&origins={origin}&destinations={dest}&mode=driving";
                var client = new RestClient(uri);
                //  client.Timeout = -1;
                var request = new RestRequest(uri, Method.Get);
                request.Timeout = -1;
                RestResponse response = client.Execute(request);
                XElement element = XElement.Parse(response.Content);
                if (element != null)
                    return int.Parse(element.Descendants("duration").FirstOrDefault()?.Element("value").Value) / 60;
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
