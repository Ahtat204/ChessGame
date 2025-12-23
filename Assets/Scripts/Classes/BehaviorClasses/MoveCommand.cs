using System;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Interfaces;
using UnityEngine;


namespace Assets.Scripts.Classes.BehaviorClasses
{
    internal class MoveCommand : ICommand
    {
        private readonly MovementManager _manager;
        private readonly SelectableDecorator _decorator;

        public MoveCommand(MovementManager manager, SelectableDecorator decorator)
        {
            _manager = manager;
            _decorator = decorator;
        }

        public void Execute()
        {
            _decorator.HandleInput();
           _manager.MovePiece(GameManager.Instance.Pieces, _decorator._target);
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}