using System.Collections.Generic;
using Assets.Scripts.Classes.Command;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.PieceComponent
{
    public class CommandManager : MonoBehaviour
    {
        private IMove _move;
        private ICommand _command;
        public Stack<ICommand> CommandStack = new();
        private ISelectable _pieceSelectionComponent;
        private CommandInvoker _invoker;

        void Start()
        {
            _pieceSelectionComponent = GetComponent<PieceSelectionComponent>();
            _invoker = new CommandInvoker(_pieceSelectionComponent);
            _move = GetComponent<IMove>();
            _command = AbstractPieceCommand.Create<ConcreteMoveCommand>(_move);
        }

        private void Update()
        {
            if (_pieceSelectionComponent.Status == SelectionStatus.Selected)
            {
                {
                    _invoker.ExecuteCommand(_command);
                }
            }
        }
    }
}