using System.Collections.Generic;
using positions=System.Collections.Generic.List<Assets.Scripts.Structs.Coordinates>;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    /// <summary>
    /// Base class of all Chess pieces
    /// </summary>
    public abstract class Piece : MonoBehaviour
    {
        
       // public abstract Coordinates Coordinates { get; set; }
        /// <summary>
        /// this property represent the Piece color , and should be initialized from the inspector in derived classes
        /// </summary>
        public abstract PieceColor Color { get; }
        /// <summary>
        /// a property that represent the Value of The piece 
        /// </summary>
        public abstract uint Value { get; protected set; }

        /// <summary>
        /// this method used to move the piece
        /// </summary>
        /// <param name="p"> p is the square where the piece is should move to </param>
        protected virtual void Move(Coordinates p)
        {
        }
        /// <summary>
        /// this method used to calculate the legal moves for the piece , 
        /// </summary>
        /// <param name="piecePosition">piecePosition is the current position of the Piece </param>
        /// <returns>Squares is an Alias for a List of a 2D vector</returns>
        protected abstract List<Vector2Int> CalculateLegalMoves(Vector3 position);
    }
    //watch this to create the grid :https://www.youtube.com/watch?v=kkAjpQAM-jE
}