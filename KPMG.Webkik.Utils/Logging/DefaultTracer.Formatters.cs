using System;
using System.IO;
using System.Net.Http;

namespace KPMG.Webkik.Utils.Logging
{
    public partial class DefaultTracer
    {
        static DefaultTracer()
        {
            AddFormater<MulticastDelegate>(v => v.ToString());
            AddFormater<HttpRequestMessage>(r => r.ToString());
            AddFormater<MemoryStream>(s => "<MemoryStream>");
        }
    }
}
