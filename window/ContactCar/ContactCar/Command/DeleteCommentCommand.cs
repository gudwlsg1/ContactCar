using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContactCar.Command
{
    public class DeleteCommentCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<int> _execute;

        public DeleteCommentCommand(Action<int> execute)
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
