using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace PythonDotnetcore
{
    class Program
    {
        static void Main(string[] args)
        {
            var listIndex = new List<string>();
            for (int i = 1; i < 26; i++)
            {
                listIndex.Add(i + "");
            }

            Parallel.ForEach(listIndex, new ParallelOptions { MaxDegreeOfParallelism = 4 }, (index) =>
            {
                Process p = new Process();
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.FileName = "python";
                p.StartInfo.Arguments = "my_python_demo.py " + index + " " + 10;
                p.StartInfo.RedirectStandardOutput = true;

                p.Start();

                p.WaitForExit();

                var result = p.StandardOutput.ReadToEnd();
                Console.WriteLine("Result for #" + index + " : " + result.Trim());
            });
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
