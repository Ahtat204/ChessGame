using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using UnityEngine;


namespace Assets.Scripts.Classes.Pieces
{
    public sealed class Queen : Piece
    {
        private readonly List<Vector2Int> _possibleMoves = new(28);
        public override uint Value => 9;
        public override IReadOnlyList<Vector2Int> PossibleMoves => _possibleMoves;

        [field: SerializeField] public override PieceColor Color { get; protected set; }

        public override void CalculateLegalMoves(Vector3 position)
        {
            _possibleMoves.Clear();
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);
            for (var i = 1; i <= Board.Size; i++)
            {
                #region Straight

                _possibleMoves.AddIfValid(positionCell.x + i, positionCell.y);
                _possibleMoves.AddIfValid(positionCell.x - i, positionCell.y);
                _possibleMoves.AddIfValid(positionCell.x, positionCell.y + i);
                _possibleMoves.AddIfValid(positionCell.x, positionCell.y - i);

                #endregion

                #region Diagonal

                _possibleMoves.AddIfValid(positionCell.x + i, positionCell.y + i);
                _possibleMoves.AddIfValid(positionCell.x + i, positionCell.y - i);
                _possibleMoves.AddIfValid(positionCell.x - i, positionCell.y + i);
                _possibleMoves.AddIfValid(positionCell.x - i, positionCell.y - i);

                #endregion
            }
        }
    }
}