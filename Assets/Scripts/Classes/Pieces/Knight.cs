using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using UnityEngine;
using Assets.Scripts.Classes.GameClasses;

namespace Assets.Scripts.Classes.Pieces
{
    public sealed class Knight : Piece
    {
        public override List<Vector2Int> PossibleMoves { get; } =
            new List<Vector2Int>(8);

        public override uint Value => 3;
        [field: SerializeField] public override PieceColor Color { get; protected set; }

        public override void CalculateLegalMoves(Vector3 position)
        {
            PossibleMoves.Clear();
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);
            AddIfValid(positionCell.x + 2, positionCell.y + 1);
            AddIfValid(positionCell.x + 2, positionCell.y - 1);
            AddIfValid(positionCell.x - 2, positionCell.y + 1);
            AddIfValid(positionCell.x - 2, positionCell.y - 1);
            AddIfValid(positionCell.x + 1, positionCell.y + 2);
            AddIfValid(positionCell.x + 1, positionCell.y - 2);
            AddIfValid(positionCell.x - 1, positionCell.y - 2);
            AddIfValid(positionCell.x - 1, positionCell.y + 2);
        }
    }
}