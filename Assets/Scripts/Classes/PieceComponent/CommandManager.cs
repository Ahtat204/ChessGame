using System.Collections.Generic;
using Assets.Scripts.Classes.Command;
using Assets.Scripts.Classes.GameClasses;
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
        readonly CommandInvoker _invoker = new();
        private PieceSelectionComponent _pieceSelectionComponent;

        void Start()
        {
            _pieceSelectionComponent = GetComponent<PieceSelectionComponent>();
            _move = GetComponent<IMove>();
            _command = PieceCommand.Create<MoveCommand>(_move);
        }

        private void Update()
        {
            if (_pieceSelectionComponent.Status == SelectionStatus.Selected)
            {
                Debug.Log(_pieceSelectionComponent.Status.ToString());
                
                {
                    Debug.Log(Board.BoardInstance.tilemap.WorldToCell(_pieceSelectionComponent._target));
                    _invoker.ExecuteCommand(_command);
                }
            }
        }
    }
}