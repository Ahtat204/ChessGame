using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Classes.Command
{
    public class CommandInvoker
    {
        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
        }
    }
}