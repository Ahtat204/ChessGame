using System;

using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.Command
{
    public abstract class AbstractPieceCommand : ICommand
    {
        protected readonly IMove Move;

        protected AbstractPieceCommand(IMove move)
        {
            Move = move;
        }

        public abstract void Execute(Vector2 target);
        public abstract void Undo();

        public static T Create<T>(IMove move) where T : AbstractPieceCommand
        {
            return (T)Activator.CreateInstance(typeof(T), move);
        }
    }
}