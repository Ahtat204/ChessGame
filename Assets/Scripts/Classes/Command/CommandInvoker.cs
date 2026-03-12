using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Classes.Command
{
    public class CommandInvoker
    {
        private  ISelectable _pieceSelectionComponent;

        public CommandInvoker(ISelectable pieceSelectionComponent)
        {
            _pieceSelectionComponent = pieceSelectionComponent;
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute(_pieceSelectionComponent.Target);
        }

     
    }
}