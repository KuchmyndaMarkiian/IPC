

using System;
using System.IO;
using System.IO.Pipes;

namespace ListenerConsole
{
    class Program
    {
        static void Main()
        {
            using (NamedPipeServerStream pipeServer =
                new NamedPipeServerStream("testpipe", PipeDirection.InOut))
            {
                Console.WriteLine("NamedPipeServerStream object created.");

                // Wait for a client to connect
                Console.Write("Waiting for client connection...");
                pipeServer.WaitForConnection();

                Console.WriteLine("Client connected.");
                try
                {
                    // Read user input and send that to the client process.
                    using (StreamReader sw = new StreamReader(pipeServer))
                    {
                        Console.WriteLine(sw.ReadToEnd());
                    }
                    Console.ReadLine();
                }
                
                // Catch the IOException that is raised if the pipe is broken
                // or disconnected.
                catch (IOException e)
                {
                    Console.WriteLine("ERROR: {0}", e.Message);
                }
            }
        }
    }
}
