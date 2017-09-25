using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Data.Json;
using System.Net;

namespace FDSClock
{
    class JSON
    {
        public string GetJSON(string url)
        {
            //API Key
            //6f4a7ad5649bbc66
            url = "http://api.wunderground.com/api/6f4a7ad5649bbc66/features/settings/q/98028.json";

            var request = WebRequest.Create(url);

            return request.ToString();
        }
    }
}
