using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IREDONotifier.Model
{
    public class GcmOutgoingMessage
    {
        public Data data { get; set; }
        public string to { get; set; }
        public class Data { public string message { get; set; } }
    }
}
