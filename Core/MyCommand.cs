using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core
{
    public class MyCommand : ICommand
    {
        public Action CommandAction { get; set; }
        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            CommandAction?.Invoke();
        }

        public event EventHandler CanExecuteChanged;
    }
}
