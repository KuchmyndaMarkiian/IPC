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
using ListenerUi.ViewModels;

namespace ListenerUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServerViewModel _serviceViewModel;
        public MainWindow()
        {
            InitializeComponent();
            _serviceViewModel = new ServerViewModel();
            DataContext = _serviceViewModel;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            Task.Run(_serviceViewModel.StartServer);
        }
        
    }
}
