using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;

namespace Filezouri.App
{
    public static class FormatterExtensions
    {
        public static Response AsStream(this IResponseFormatter formatter, Func<Stream> readStream, string contentType)
        {
            return new StreamResponse(readStream, contentType);
        }
    }

    public class StreamResponse : Response
    {
        public StreamResponse(Func<Stream> readStream, string contentType)
        {
            Contents = stream =>
            {
                using (var read = readStream())
                {
                    read.CopyTo(stream);
                }
            };
            ContentType = contentType;
            StatusCode = HttpStatusCode.OK;
        }
    }
}
