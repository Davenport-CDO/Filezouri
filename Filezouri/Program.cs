using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Hosting.Self;

namespace Filezouri
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "http://localhost:4040";

            using (NancyHost host = new NancyHost(new Uri(path)))
            {
                Console.WriteLine("Welcome to Filezouri!");
                Console.WriteLine("Starting NancyHost...");
                host.Start();
                Console.WriteLine("Host started, access at {0}", path);
                Console.ReadLine();
            }


        }
    }
}
