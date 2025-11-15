using System;
using System.Collections.Generic;
using Assets.Scripts.Classes.BehaviorClasses;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    /// <summary>
    /// Base class of all Chess pieces
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SelectableDecorator))]
    [RequireComponent(typeof(MovementManager))]


    public abstract class Piece : MonoBehaviour
    {
        /// <summary>
        /// the possible moves the piece can make
        /// </summary>
        public abstract List<Vector2Int> PossibleMoves { get; }
        /// <summary>
        /// this property represent the Piece color , and should be initialized from the inspector in derived classes
        /// </summary>
        public abstract PieceColor Color { get; protected set; }
        /// <summary>
        /// a property that represent the Value of The piece 
        /// </summary>
        public abstract uint Value { get; }
        /// <summary>
        /// this method used to calculate the legal moves for the piece , 
        /// </summary>
        /// <param name="position">position is the current position of the Piece </param>
        /// <returns> a List of a 2D vector</returns>
        protected abstract List<Vector2Int> CalculateLegalMoves(Vector3 position);
        
    }
}