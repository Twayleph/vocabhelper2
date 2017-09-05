using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VocabularyHelperVS
{
    public class DelegateCommand : ICommand
    {
        public Action Execute { get; }

        public Func<bool> CanExecute { get; }

        public DelegateCommand(Action execute)
            : this(execute, () => true)
        {
        }

        public DelegateCommand(Action execute, Func<bool> canExecute)
        {
            Execute = execute;
            CanExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            Execute();
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        public Action<T> Execute { get; }

        public Func<T, bool> CanExecute { get; }

        public DelegateCommand(Action<T> execute)
            : this(execute, _ => true)
        {
        }

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute)
        {
            Execute = execute;
            CanExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }
    }
}
