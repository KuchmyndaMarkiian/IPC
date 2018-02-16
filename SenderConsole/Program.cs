using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            using (NamedPipeClientStream pipeClient =
                new NamedPipeClientStream(".", "testpipe", PipeDirection.InOut))
            {

                // Connect to the pipe or wait until the pipe is available.
                Console.Write("Attempting to connect to pipe...");
                pipeClient.Connect();
                   
                Console.WriteLine("Connected to pipe.");
                Console.WriteLine("There are currently {0} pipe server instances open.",
                    pipeClient.NumberOfServerInstances);
                using (StreamWriter sr = new StreamWriter(pipeClient))
                {
                    // Display the read text to the console
                    var msg = Console.ReadLine();
                    sr.WriteLine(msg);
                }
            }
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
