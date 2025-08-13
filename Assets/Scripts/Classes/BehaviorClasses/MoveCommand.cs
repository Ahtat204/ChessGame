using System;
using Assets.Scripts.Interfaces;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Classes.BehaviorClasses
{
    internal class MoveCommand<T> : ICommand where T:Piece 
    {
        private readonly T _piece;
        private readonly Vector3Int _destination;
        private readonly Vector3Int _origin;

        public MoveCommand([NotNull] T piece, Vector3Int destination, Vector3Int origin)
        {
            _piece = piece ?? throw new ArgumentNullException(nameof(piece));
            _destination = destination;
            _origin = origin;
        }
        /// <summary>
        /// this method used to move the piece
        /// </summary>
        /// <param name="to"> to is the square where the piece it should move to </param>
        public void Execute()
        {
            throw new NotImplementedException();
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}