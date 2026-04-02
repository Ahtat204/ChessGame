using Assets.Scripts.Classes.GameClasses;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    public sealed class King : Piece
    {
        public override List<Vector2Int> PossibleMoves => CalculateLegalMoves(transform.position);
        public override uint Value => 0;
        [field: SerializeField]
        public override PieceColor Color { get; protected set; }
        protected sealed override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            var legalMoves = new List<Vector2Int>(8);
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);
            legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y + 1));
            legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y - 1));
            legalMoves.Add(new Vector2Int(positionCell.x - 1, positionCell.y));
            legalMoves.Add(new Vector2Int(positionCell.x + 1, positionCell.y));
            legalMoves.Add(new Vector2Int(positionCell.x - 1, positionCell.y - 1));
            legalMoves.Add(new Vector2Int(positionCell.x - 1, positionCell.y + 1));
            legalMoves.Add(new Vector2Int(positionCell.x + 1, positionCell.y - 1));
            legalMoves.Add(new Vector2Int(positionCell.x + 1, positionCell.y - 1));
            legalMoves.Remove(positionCell);
            return legalMoves.Where(pos => pos is { x: >= 1 and <= 8, y: >= 1 and <= 8 }).ToList();
        }
    }
}