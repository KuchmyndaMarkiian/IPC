using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Threading.Tasks;
using Core;
using HostUI.Annotations;

namespace HostUI.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private ServiceHost _service;
        public NewService NewService { get; }
        private string _allText;

        public MainViewModel()
        {
            NewService = new NewService
            {
                ListenAction = s =>
                {
                    lock (this) AllText = s;
                }
            };
        }

        public async Task Hosting()
        {
            var pipeBnding = new NetNamedPipeBinding
            {
                ReceiveTimeout = TimeSpan.MaxValue,
                CloseTimeout = TimeSpan.MaxValue,
                OpenTimeout = TimeSpan.MaxValue
            };
            _service = new ServiceHost(NewService, NewService.BaseAddress);
            _service.AddServiceEndpoint(typeof(INewService), pipeBnding, NewService.Address);

            _service.Open();
        }

        public string AllText
        {
            get => _allText;
            set
            {
                _allText = value;
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