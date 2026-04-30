using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Enums;
using Classes.PieceComponent;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    /// <summary>
    /// Represents the King entity, the primary objective of the simulation.
    /// </summary>
    /// <remarks>
    /// The King's move-set is defined by a Moore neighborhood of range 1. 
    /// Its survival is the mandatory win-condition for both factions.
    /// </remarks>
    [RequireComponent(typeof(PieceMovementComponent), typeof(PieceSelectionComponent), typeof(CommandManager))]
    public sealed class King : Piece
    {
        /// <summary>
        /// Internal buffer for potential move coordinates.
        /// Fixed capacity of 8 represents the maximum theoretical mobility of the unit.
        /// </summary>
        private readonly List<Vector2Int> _possibleMoves = new(8);

        /// <summary>
        /// The King has no material value (0) because its loss terminates the simulation.
        /// In AI heuristics, this is often treated as positive/negative infinity.
        /// </summary>
        public override uint Value => 0;

        /// <inheritdoc cref="Piece.PossibleMoves"/>
        public override IReadOnlyList<Vector2Int> PossibleMoves => _possibleMoves;

        /// <inheritdoc/>
        [field: SerializeField]
        public override PieceColor Color { get; protected set; }

        /// <summary>
        /// Populates the move-set by checking all 8 adjacent tiles relative to the current cell.
        /// </summary>
        /// <param name="position">Current world-space transform to be quantized into grid coordinates.</param>
        /// <remarks>
        /// Future iterations must include "Check" validation logic, preventing the King 
        /// from entering cells controlled by the opponent's influence maps.
        /// </remarks>
        public override void CalculateLegalMoves(Vector3 position)
        {
            _possibleMoves.Clear();
            Vector2Int positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);

            // Orthogonal Directions
            _possibleMoves.AddIfValid(positionCell.x, positionCell.y + 1); // North
            _possibleMoves.AddIfValid(positionCell.x, positionCell.y - 1); // South
            _possibleMoves.AddIfValid(positionCell.x - 1, positionCell.y); // West
            _possibleMoves.AddIfValid(positionCell.x + 1, positionCell.y); // East

            // Diagonal Directions
            _possibleMoves.AddIfValid(positionCell.x - 1, positionCell.y - 1); // South-West
            _possibleMoves.AddIfValid(positionCell.x - 1, positionCell.y + 1); // North-West
            _possibleMoves.AddIfValid(positionCell.x + 1, positionCell.y - 1); // South-East
            _possibleMoves.AddIfValid(positionCell.x + 1, positionCell.y + 1); // North-East
        }
    }
}