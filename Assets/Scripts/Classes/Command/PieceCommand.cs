using System;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Classes.Command
{
    public abstract class PieceCommand : ICommand
    {
        protected readonly IMove _move;

        protected PieceCommand(IMove move)
        {
            _move = move;
        }

        public abstract void Execute();
        public abstract void Undo();

        public static T create<T>(IMove move) where T : PieceCommand
        {
            return (T)Activator.CreateInstance(typeof(T), move);
        }
    }
}