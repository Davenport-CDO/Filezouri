using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filezouri.App;
using Humanizer;
using Nancy;
using Nancy.Responses;

namespace Filezouri.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = x =>
            {
                return View["Home.sshtml", new { Files = Directory.GetFiles(Configuration.Current.DirectoryRoot).Select(d=>Path.GetFileName(d)) }];
            };

            Get["/download/{filename}"] = x =>
            {
                string fileName = x.filename;
                Console.WriteLine(Request.UserHostAddress + " downloaded " + fileName);
                return Response.AsStream(() => new FileStream(Path.Combine(Configuration.Current.DirectoryRoot, Path.GetFileName(fileName)), FileMode.Open), "application/octet-stream");
            };

            Get["/files.json"] = x =>
            {
                return Response.AsJson(Directory.GetFiles(Configuration.Current.DirectoryRoot).Select(d=>new FileInfo(d)).Select(d => new
                {
                    name = d.Name,
                    length = d.Length.Bytes().Humanize("#.##"),
                    realLength = d.Length,
                    date = d.CreationTime.ToString("G")
                }));
            };

        }
    }
}
