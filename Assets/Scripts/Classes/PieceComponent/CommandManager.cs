using System.Collections.Generic;
using Assets.Scripts.Classes.Command;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.PieceComponent
{
    public class CommandManager : MonoBehaviour
    {
        private IMove _move;
        private ICommand _command;
       
        private ISelectable _pieceSelectionComponent;
        private CommandInvoker _invoker;
        private void Start()
        {
           
            _pieceSelectionComponent = GetComponent<ISelectable>();
            _invoker = new CommandInvoker(_pieceSelectionComponent);
            _move = GetComponent<IMove>();
            _command = AbstractPieceCommand.Create<ConcreteMoveCommand>(_move);
        }
        public void OnEnable()=>PieceSelectionComponent.OnPieceSelectedEvent +=DoWork;
        public void OnDisable()=>PieceSelectionComponent.OnPieceSelectedEvent -= DoWork;
        private void DoWork()
        {
            _invoker.ExecuteCommand(_command);
            GameManager.Instance.CommandStack.Push(_command);
        }
    }
}