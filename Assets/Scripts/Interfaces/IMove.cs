using System.Collections.Generic;
using Assets.Scripts.Classes.PieceComponent;
using UnityEngine;


namespace Assets.Scripts.Interfaces
{
    public interface IMove
    {
        /// <summary>
        /// Evaluates movement input and intercepts specific coordinates to execute Castling.
        /// </summary>
        /// <param name="pieces">The spatial index of all active <see cref="PieceMovementComponent"/> instances.</param>
        /// <param name="targetPos">The world-space destination selected by the user.</param>
        /// <remarks>
        /// This method checks for four hardcoded target cells (Kingside and Queenside for both colors).
        /// If a valid castling square is selected and unoccupied, it triggers a secondary 
        /// <see cref="MovePiece"/> call on the associated Rook.
        /// </remarks>
        public void MovePiece(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2 targetPos);
    }
}