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
     
        public override List<Vector2Int> PossibleMoves => CalculateLegalMoves(transform.position);
        [field: SerializeField]
        public override PieceColor Color { get; protected set; }
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
        public override uint Value => 5;
    }
}
