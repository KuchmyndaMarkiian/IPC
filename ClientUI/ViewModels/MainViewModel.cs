using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ClientUI.Annotations;
using Core;

namespace ClientUI.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private MyCommand _sendCommand;
        private INewService newService;
        private string _text;

        public MainViewModel()
        {
            SendCommand = new MyCommand(){CommandAction = () =>
            {
                newService?.Send(Text);
            }};
        }
        public async Task Hosting()
        {
            var pipeBnding = new NetNamedPipeBinding
            {
                ReceiveTimeout = TimeSpan.MaxValue,
                CloseTimeout = TimeSpan.MaxValue,
                OpenTimeout = TimeSpan.MaxValue
            };
            var channel = new ChannelFactory<INewService>(pipeBnding,
                new EndpointAddress($"{NewService.BaseAddress}"));
            newService = channel.CreateChannel();
        }

        public MyCommand SendCommand
        {
            get { return _sendCommand; }
            set { _sendCommand = value;
                OnPropertyChanged(); }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }
        #region Notification

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
