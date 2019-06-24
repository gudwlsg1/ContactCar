using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContactCar.Command
{
    public class UpdateCommentCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<int> _execute;

        public UpdateCommentCommand(Action<int> execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute.Invoke((int)parameter);
        }
    }
}
