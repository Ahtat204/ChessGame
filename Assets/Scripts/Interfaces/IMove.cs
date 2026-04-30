using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    /// <summary>
    /// Defines the contract for entities capable of spatial displacement and capture resolution.
    /// </summary>
    /// <remarks>
    /// Implementing classes are responsible for validating target coordinates against 
    /// their specific movement templates and synchronizing logical board dictionaries.
    /// </remarks>
    public interface IMove
    {
        /// <summary>
        /// Executes a movement or interaction sequence based on a target spatial coordinate.
        /// </summary>
        /// <param name="pieces">A reference to the global spatial index containing all active piece components.</param>
        /// <param name="targetPos">The grid-space vector representing the intended destination.</param>
        /// <remarks>
        /// Implementations should handle:
        /// 1. Path-finding/Collision validation.
        /// 2. Capture logic (destruction of opponent entities).
        /// 3. Internal state updates (e.g., updating current grid-cell cache).
        /// </remarks>
        [Pure]
        public MoveType MovePiece(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int targetPos);
    }
}