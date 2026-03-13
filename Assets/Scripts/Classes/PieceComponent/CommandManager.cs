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
        private Stack<ICommand> CommandStack;
        private ISelectable _pieceSelectionComponent;
        private CommandInvoker _invoker;

        private void Start()
        {
            CommandStack = new();
            _pieceSelectionComponent = GetComponent<ISelectable>();
            _invoker = new CommandInvoker(_pieceSelectionComponent);
            _move = GetComponent<IMove>();
            _command = AbstractPieceCommand.Create<ConcreteMoveCommand>(_move);
        }

        private void Update()
        {
            if (_pieceSelectionComponent.Status != SelectionStatus.Selected && _pieceSelectionComponent.ClickCount!=1) return;
            _invoker.ExecuteCommand(_command);
            _pieceSelectionComponent.ClickCount = 0;
            CommandStack.Push(_command);
         //   _pieceSelectionComponent.OnDeselect();
        }
    }
}