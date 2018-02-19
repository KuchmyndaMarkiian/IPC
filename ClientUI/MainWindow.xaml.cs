using System;
using System.Collections.Generic;
using System.Linq;
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
using ClientUI.ViewModels;

namespace ClientUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel ViewModel;
        public MainWindow()
        {
            ViewModel=new MainViewModel();
            InitializeComponent();
            DataContext = ViewModel;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            ViewModel?.Hosting();
        }
    }
}
