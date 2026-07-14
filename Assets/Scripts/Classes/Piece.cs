using System.Collections.Generic;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    /// <summary>
    /// The fundamental abstraction for all Chess entities within the simulation.
    /// Manages physical presence, movement logic, and value heuristics.
    /// </summary>
    /// <remarks>
    /// This class enforces a "Composition over Inheritance" model by requiring 
    /// specific components for selection and movement handling.
    /// </remarks>
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PieceSelectionComponent))]
    [RequireComponent(typeof(PieceMovementComponent))]
    public abstract class Piece : MonoBehaviour
    {
        /// <summary>
        /// Gets the collection of directional vectors or relative offsets 
        /// representing the piece's move-set template.
        /// </summary>
        public abstract IReadOnlyList<Vector2Int> PossibleMoves { get; }

        /// <summary>
        /// Defines the piece's faction. Must be assigned during instantiation 
        /// or via the Unity Inspector.
        /// </summary>
        /// <value>The <see cref="PieceColor"/> representing White or Black.</value>
        [field: SerializeField]
        public abstract PieceColor Color { get; protected set; }

        /// <summary>
        /// The relative material worth of the piece used for AI evaluation 
        /// and game-state weighting.
        /// </summary>
        public abstract uint Value { get; }

        /// <summary>
        /// Evaluates the board state and populates the legal move-set for the piece.
        /// </summary>
        /// <param name="position">The current world or grid coordinates of the entity.</param>
        /// <remarks>
        /// This method should account for board boundaries, collision with 
        /// allied pieces, and checkmate-prevention logic.
        /// </remarks>
        public abstract void CalculateLegalMoves(Vector3 position);
    }
}