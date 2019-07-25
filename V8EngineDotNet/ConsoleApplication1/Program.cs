using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Noesis.Javascript;
using System.IO;

namespace JSNet
{
    class Program
    {
        public class SystemConsole
        {
            public SystemConsole() { }

            public void Print(string iString)
            {
                Console.WriteLine(iString);
            }
        }

        static string getJavascriptFromFile(string path)
        {
            string js = "";

            if (File.Exists(path))
            {
                js = File.ReadAllText(path);
            }
            else
            {
                throw new Exception("file not found");
            }

            return js;
        }

        static void Main(string[] args)
        {
            string path = Environment.CurrentDirectory + "\\test.js";
            string js = getJavascriptFromFile(path);

            using (JavascriptContext context = new JavascriptContext())
            {

                // Setting external parameters for the context
                context.SetParameter("console", new SystemConsole());
                context.SetParameter("p1", 10);
                context.SetParameter("p2", 11);
                context.SetParameter("runResult", 0);

                string script = js + @"

                    runResult = add(p1, p2);
                    console.Print(runResult);
                ";

                context.Run(script);

                Console.WriteLine("run result: " + context.GetParameter("runResult"));
                Console.ReadKey();
            }
        }
    }
}
