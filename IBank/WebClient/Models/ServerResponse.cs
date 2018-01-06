using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WebClient.Models
{
    public class ServerResponse
    {
        public bool Success { get; set; }
        public string Info { get; set; }
        public string Message { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}