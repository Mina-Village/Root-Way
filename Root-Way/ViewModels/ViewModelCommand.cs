using System;
using Avalonia.Input;
using System.Windows.Input;

namespace Root_Way.ViewModels;

public class ViewModelCommand : ICommand
{
    private readonly Action<object> _executeAction;
    private readonly Predicate<object> _canExecuteAction;
    private bool _canExecute;

    public ViewModelCommand(Action<object> executeAction)
    {
        _executeAction = executeAction;
        _canExecuteAction = null;
        _canExecute = true;
    }

    public ViewModelCommand(Action<object> executeAction, Predicate<object> canExecuteAction)
    {
        _executeAction = executeAction;
        _canExecuteAction = canExecuteAction;
        _canExecute = true;
    }

    public bool CanExecute(object? parameter)
    {
        if (_canExecuteAction != null)
        {
            return _canExecute && _canExecuteAction(parameter);
        }

        return _canExecute;
    }

    public void SetCanExecute(bool canExecute)
    {
        if (_canExecute != canExecute)
        {
            _canExecute = canExecute;
            RaiseCanExecuteChanged();
        }
    }

    public event EventHandler? CanExecuteChanged;

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Execute(object? parameter)
    {
        _executeAction(parameter);
    }
}
