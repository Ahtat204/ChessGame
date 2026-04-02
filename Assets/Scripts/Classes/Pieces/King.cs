using Assets.Scripts.Classes.GameClasses;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    public sealed class King : Piece
    {
        public override List<Vector2Int> PossibleMoves { get; }=new List<Vector2Int>(8);
        public override uint Value => 0;
        [field: SerializeField]
        public override PieceColor Color { get; protected set; }
        public sealed override void CalculateLegalMoves(Vector3 position)
        {
            PossibleMoves.Clear();
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);
            AddIfValid(positionCell.x, positionCell.y + 1);
           AddIfValid(positionCell.x, positionCell.y - 1);
            AddIfValid(positionCell.x - 1, positionCell.y);
            AddIfValid(positionCell.x + 1, positionCell.y);
            AddIfValid(positionCell.x - 1, positionCell.y - 1);
            AddIfValid(positionCell.x - 1, positionCell.y + 1);
            AddIfValid(positionCell.x + 1, positionCell.y - 1);
            AddIfValid(positionCell.x + 1, positionCell.y - 1);
        }
    }
}