using System;
using System.Windows.Input;
using IREDONotifier.ViewModel;

namespace IREDONotifier.Command
{
    class SendCmd : ICommand
    {
        public SendCmd(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        private readonly MainViewModel _mainViewModel;

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return _mainViewModel.CanSend();
        }

        public void Execute(object parameter)
        {
            _mainViewModel.Send();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }

        #endregion    
    }
}
