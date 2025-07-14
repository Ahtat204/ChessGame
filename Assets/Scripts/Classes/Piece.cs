using Assets.Scripts.Enums;
using UnityEngine;
using Squares = System.Collections.Generic.List<UnityEngine.Vector2>;
using Square = UnityEngine.Vector2;

namespace Assets.Scripts.Classes
{
    /// <summary>
    /// Base class of all Chess pieces
    /// </summary>
    public abstract class Piece : MonoBehaviour
    {
        /// <summary>
        /// a property that represent the position of the piece
        /// </summary>
        protected abstract Vector2 position { get; }

        /// <summary>
        /// a property that represent the Value of The piece 
        /// </summary>
        public abstract uint Value { get; protected set; }

        /// <summary>
        /// this method used to move the piece
        /// </summary>
        /// <param name="p"> p is the square where the piece is should move to </param>
        protected abstract void Move(Square p);

        /// <summary>
        /// this property represent the Piece color , and should be initialized from the inspector in derived classes
        /// </summary>
        public abstract PieceColor Color { get; }

        /// <summary>
        /// this method used to calculate the legal moves for the piece , 
        /// </summary>
        /// <param name="piecePosition">piecePosition is the current position of the Piece </param>
        /// <returns>Squares is an Alias for a List of a 2D vector</returns>
        protected virtual Squares CalculateLegalMoves(Vector2 piecePosition)
        {
            return new Squares();
        }
    }

    //watch this to create the grid :https://www.youtube.com/watch?v=kkAjpQAM-jE
}