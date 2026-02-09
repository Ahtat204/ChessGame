using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Classes.Command
{
    public class CommandInvoker
    {
        private  PieceSelectionComponent _pieceSelectionComponent;

        public CommandInvoker(PieceSelectionComponent pieceSelectionComponent)
        {
            _pieceSelectionComponent = pieceSelectionComponent;
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute(_pieceSelectionComponent._target);
        }
    }
}