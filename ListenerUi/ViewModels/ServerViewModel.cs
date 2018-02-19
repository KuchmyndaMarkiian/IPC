using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Core;
using ListenerUi.Annotations;

namespace ListenerUi.ViewModels
{
    class ServerViewModel : INotifyPropertyChanged
    {
        private string _allText = "";

        public ServerViewModel()
        {
            SendCommand = new MyCommand()
            {
                CommandAction = async () =>
                {
                    var pipe = new NamedPipeServerStream("testpipe", PipeDirection.InOut);
                    await pipe.WaitForConnectionAsync();
                    if (pipe.IsConnected)
                    {
                        using (var writer = new StreamWriter(pipe))
                        {
                            lock (this.Text)
                            {
                                writer.AutoFlush = true;
                                writer.Write(Text);
                            }
                        }
                    }
                    pipe.Close();
                }
            };
        }

        public async Task StartServer()
        {
            while (true)
            {
                var pipe =new NamedPipeServerStream("testpipe",PipeDirection.InOut,10);
                await pipe.WaitForConnectionAsync();
                if (pipe.IsConnected)
                {
                    using (var reader = new StreamReader(pipe))
                    {
                        lock(this) AllText = reader.ReadToEnd().Replace("\r\n", "");
                    }
                }
                pipe.Close();
            }
        }

        public ICommand SendCommand { get; private set; }

        public string Text { get; set; } = "";

        public string AllText
        {
            get { return _allText; }
            set { _allText = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
