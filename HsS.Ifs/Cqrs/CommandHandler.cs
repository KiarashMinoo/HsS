using HsS.Ifs.Data;
using System;

namespace HsS.Ifs.Cqrs
{
    public class CommandEventArgs : EventArgs
    {
        public string Message { get; set; }
    }

    public delegate object CommandExecuting(object sender, CommandEventArgs args);
    public delegate object CommandExecuted(object sender, CommandEventArgs args);

    public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand> where TCommand : ICommand
    {
        protected IUnitOfWork UnitOfWork { get; private set; }

        public event CommandExecuting CommandExecuting;

        public event CommandExecuted CommandExecuted;

        public CommandHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public void Handle(TCommand command)
        {
            OnCommandExecuting(string.Empty);
            HandleCommand(command);
            OnCommandExecuted(string.Empty);
        }

        protected abstract void HandleCommand(TCommand command);

        protected virtual void OnCommandExecuting(string msg)
        {
            CommandExecuting?.Invoke(this, new CommandEventArgs { Message = msg });
        }

        protected virtual void OnCommandExecuted(string msg)
        {
            CommandExecuted?.Invoke(this, new CommandEventArgs { Message = msg });
        }
    }
}
