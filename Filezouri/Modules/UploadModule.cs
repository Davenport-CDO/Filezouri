using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Filezouri.App;
using Nancy;
using Nancy.Responses;

namespace Filezouri.Modules
{
    public class UploadModule : NancyModule
    {
        public UploadModule()
        {
            Before += ctx =>
            {
                return (Context.CurrentUser == null) ? new HtmlResponse(HttpStatusCode.Unauthorized) : null;
            };

            Get["/upload"] = x => View["Upload.sshtml"];

            Post["/upload", true] = async (x, token) =>
            {
                if (!Request.Files.Any()) { return new HtmlResponse(HttpStatusCode.BadRequest); }

                foreach (var file in Request.Files)
                {
                    string filePath;
                    int fileMod = 1;

                    do
                    {
                        filePath = Path.Combine(Configuration.Current.DirectoryRoot, Path.GetFileNameWithoutExtension(file.Name) +
                            (fileMod > 1 ? "_" + fileMod : "") + Path.GetExtension(file.Name));

                        fileMod++;
                    } while (File.Exists(filePath));

                    using (FileStream fileStream = File.OpenWrite(filePath))
                    {
                        await file.Value.CopyToAsync(fileStream);
                    }

                    Console.WriteLine(Request.UserHostAddress + " uploaded " + filePath);
                }

                return new TextResponse(HttpStatusCode.OK, "File Uploaded");
            };
        }
    }
}
