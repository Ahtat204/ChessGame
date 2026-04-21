using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.Command
{
    public abstract class AbstractPieceCommand : ICommand
    {
        protected readonly IMove _move;

        protected AbstractPieceCommand(IMove move)
        {
            _move = move;
        }

        public abstract MoveType MoveType { get; }
        public abstract void Execute(Vector2Int target);
        public abstract void Undo();

        public static T Create<T>(IMove move) where T : AbstractPieceCommand
        {
            return (T)Activator.CreateInstance(typeof(T), move);
        }
    }
}