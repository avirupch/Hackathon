using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;
using System.IO;
using System.Net;

using Newtonsoft.Json;  //Install Json.Net package
using Newtonsoft.Json.Linq;

namespace RssNewsReader
{
    public class NewsSummary
    {
        public string title { get; set; }
        public string digest { get; set; }
        public string source { get; set; }

    }
}
