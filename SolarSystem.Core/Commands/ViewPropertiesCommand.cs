using System;
using System.Windows.Input;

namespace SolarSystem.Core.Commands
{
    public class ViewPropertiesCommand : ICommand
    {
       public event EventHandler CanExecuteChanged;

        Action<object> _execteMethod;
        Func<object, bool> _canexecuteMethod;

        public ViewPropertiesCommand(Action<object> execteMethod, Func<object, bool> canexecuteMethod)
        {
            _execteMethod = execteMethod;
            _canexecuteMethod = canexecuteMethod;
        }

        public bool CanExecute(object parameter)
        {
            if (_canexecuteMethod != null)
            {
                return _canexecuteMethod(parameter);
            }
            else
            {
                return false;
            }
        }

      
        public void Execute(object parameter)
        {
            _execteMethod(parameter);
        }
    }
}