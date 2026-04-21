

using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    /// <summary>
    /// this is a generic command pattern interface for all suitable use cases
    /// </summary>
    public interface ICommand
    {
        public MoveType MoveType { get; }
        public void Execute(Vector2Int target);
        public void Undo();
    }
}