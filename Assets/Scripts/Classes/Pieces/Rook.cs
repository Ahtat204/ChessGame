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
    public sealed class Rook : Piece
    {
        public override uint Value => 5;
        private readonly List<Vector2Int> _possibleMoves = new(14);
        public override IReadOnlyList<Vector2Int> PossibleMoves => _possibleMoves;
        [field: SerializeField] public override PieceColor Color { get; protected set; }
        public override void CalculateLegalMoves(Vector3 position)
        {
            _possibleMoves.Clear();
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);
            for (var i = 0; i < Board.Size; i++)
            {
                _possibleMoves.AddIfValid(positionCell.x + i, positionCell.y);
                _possibleMoves.AddIfValid(positionCell.x - i, positionCell.y);
                _possibleMoves.AddIfValid(positionCell.x, positionCell.y + i);
                _possibleMoves.AddIfValid(positionCell.x, positionCell.y - i);
            }
        }
    }
}