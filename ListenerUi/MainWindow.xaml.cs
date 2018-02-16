using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core;

namespace ListenerUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NamedPipeServerStream pipeServer;

        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            Task.Run(StartServer);
        }

        private async Task StartServer()
        {
            using (pipeServer =
                new NamedPipeServerStream("testpipe", PipeDirection.InOut))
            {
                //Console.WriteLine("NamedPipeServerStream object created.");

                // Wait for a client to connect
                //Console.Write("Waiting for client connection...");
              
                //Console.WriteLine("Client connected.");
                try
                {
                    while (true)
                    {
                        pipeServer.WaitForConnection();

                        using (var reader = new  StreamReader(pipeServer))
                        {
                            string text;
                            while ((text = reader.ReadLine())!= null)
                            {
                                RichTextBox.AppendText(text);
                            }
                        }
                    }
                }
                // Catch the IOException that is raised if the pipe is broken
                // or disconnected.
                catch (IOException e)
                {
                    //Console.WriteLine("ERROR: {0}", e.Message);
                }
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
