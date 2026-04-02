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
        public override List<Vector2Int> PossibleMoves { get; }= new List<Vector2Int>(14);

        [field: SerializeField]
        public override PieceColor Color { get; protected set; }

        public override void CalculateLegalMoves(Vector3 position)
        {
            PossibleMoves.Clear();
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);
            for (var i = 0; i < Board.Size; i++)
            {
                AddIfValid(positionCell.x + i, positionCell.y);
                AddIfValid(positionCell.x - i, positionCell.y);
                AddIfValid(positionCell.x, positionCell.y + i);
                AddIfValid(positionCell.x, positionCell.y - i);
            }
        }
    }
}
