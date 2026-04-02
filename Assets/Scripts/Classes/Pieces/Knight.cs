using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using UnityEngine;
using Assets.Scripts.Classes.GameClasses;

namespace Assets.Scripts.Classes.Pieces
{
    public sealed class Knight : Piece
    {
        private readonly List<Vector2Int> _possibleMoves = new (8);

        public override uint Value => 3;
        public override IReadOnlyList<Vector2Int> PossibleMoves => _possibleMoves;
        [field: SerializeField] public override PieceColor Color { get; protected set; }

        public override void CalculateLegalMoves(Vector3 position)
        {
            _possibleMoves.Clear();
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);
            _possibleMoves.AddIfValid(positionCell.x + 2, positionCell.y + 1);
            _possibleMoves.AddIfValid(positionCell.x + 2, positionCell.y - 1);
            _possibleMoves.AddIfValid(positionCell.x - 2, positionCell.y + 1);
            _possibleMoves.AddIfValid(positionCell.x - 2, positionCell.y - 1);
            _possibleMoves.AddIfValid(positionCell.x + 1, positionCell.y + 2);
            _possibleMoves.AddIfValid(positionCell.x + 1, positionCell.y - 2);
            _possibleMoves.AddIfValid(positionCell.x - 1, positionCell.y - 2);
            _possibleMoves.AddIfValid(positionCell.x - 1, positionCell.y + 2);
        }
    }
}