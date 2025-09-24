using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using UnityEngine;
using Assets.Scripts.Classes.GameClasses;

namespace Assets.Scripts.Classes.Pieces
{
    /// <summary>
    /// Represents a rook chess piece.
    /// Can move horizontally or vertically across the board.
    /// </summary>
    public class Rook : Piece
    {
        /// <summary>
        /// Gets the list of legal moves available for this rook 
        /// from its current board position.
        /// </summary>
        public override List<Vector2Int> PossibleMoves => CalculateLegalMoves(transform.position);
        /// <summary>
        /// Gets the color of this rook.
        /// </summary>
        [field: SerializeField]
        public override PieceColor Color { get; protected set; }

        /// <summary>
        /// Calculates all legal moves for the rook based on the given world position.
        /// A rook can move any number of squares along ranks or files,
        /// but cannot move outside the board boundaries.
        /// </summary>
        /// <param name="position">The rook's current world position in Unity space.</param>
        /// <returns>A filtered list of legal moves represented as <see cref="Vector2Int"/> positions.</returns>
        protected sealed override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            var legalMoves = new List<Vector2Int>(14);

            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);

            // Generate moves in all four directions (horizontal and vertical).
            for (var i = 0; i < Board.Size; i++)
            {
                legalMoves.Add(new Vector2Int(positionCell.x + i, positionCell.y));
                legalMoves.Add(new Vector2Int(positionCell.x - i, positionCell.y));
                legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y + i));
                legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y - i));
            }
            // Remove current position (rook cannot stay in place as a move).
            legalMoves.Remove(positionCell);
            // Filter out moves outside the 8x8 chessboard.
            var filteredMovesList =
                legalMoves.Where(pos => pos is { x: >= 1 and <= 8, y: >= 1 and <= 8 }).ToList();
            return filteredMovesList;
        }

        /// <summary>
        /// Gets the material value of the rook in standard chess evaluation.
        /// Default value is 5.
        /// </summary>
        public override uint Value => 5;
    }
}
