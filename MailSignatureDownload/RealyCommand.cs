using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MailSignatureDownload
{
    /// <summary>
    /// Class for storing a command that is bind to graphic UI. Also provide one way for error handling for all further command objects - see method "Execute".
    /// </summary>
    public class RelayCommand : ObservableObject, ICommand
    {
        private string _lastOperationStatus;
        
        public string LastOperationStatus
        {
            get => _lastOperationStatus;
            set
            {
                _lastOperationStatus = value;
                OnPropChanged(nameof(LastOperationStatus));
            }
        }

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new NullReferenceException("execute");

            _execute = execute;
            _canExecute = canExecute;

            LastOperationStatus = "No operation executed";
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.LastOperationStatus = "Operation succeeded!";
            
            try
            {
                _execute.Invoke(parameter);
            }
            catch (Exception e)
            {
                this.LastOperationStatus = $"Operation failed - details: {e.Message}";
            }
        }
    }
}
