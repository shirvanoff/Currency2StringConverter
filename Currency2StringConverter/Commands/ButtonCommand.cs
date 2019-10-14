using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Currency2StringConverter.Commands
{
    public class ButtonCommand : ICommand
    {
        private Action<object> action = null;
        private Func<object, bool> canExecute = null;

        public event EventHandler CanExecuteChanged;

        public ButtonCommand(Action<object> action, Func<object, bool> canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
                return canExecute(parameter);
            return action != null;
        }

        public void Execute(object parameter = null) => action?.Invoke(parameter);
    }
}
