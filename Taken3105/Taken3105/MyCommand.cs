using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Taken3105
{
    public class MyCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<Cube> MyAction { get; set; }

        public MyCommand(Action<Cube> action)
        {
            MyAction = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MyAction(parameter as Cube);
        }
    }
}
