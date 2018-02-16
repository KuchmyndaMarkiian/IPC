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

namespace SenderUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private NamedPipeClientStream clientStream;

        public MainWindow()
        {
            InitializeComponent();
           
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            Task.Run(StrtClient);
        }

        private async Task StrtClient()
        {
            clientStream =
                new NamedPipeClientStream(".", "testpipe", PipeDirection.InOut);
            
                //Console.WriteLine("NamedPipeServerStream object created.");

                // Wait for a client to connect
                //Console.Write("Waiting for client connection...");
                //clientStream.WaitForConnection();
                clientStream.Connect();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            clientStream =
                new NamedPipeClientStream(".", "testpipe", PipeDirection.InOut);
            clientStream.Connect();
            using (StreamWriter sw = new StreamWriter(clientStream))
            {
                sw.AutoFlush = true;
                sw.WriteLine(RichTextBox.Text);
            }
        }
    }
}
