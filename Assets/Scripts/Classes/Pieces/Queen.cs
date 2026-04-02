using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using UnityEngine;


namespace Assets.Scripts.Classes.Pieces
{
    
    public sealed class Queen : Piece
    {
        public override List<Vector2Int> PossibleMoves { get;}=new List<Vector2Int>(28);
        public override uint Value => 9;
        [field: SerializeField]
        public override PieceColor Color { get; protected set; }
        public  override void CalculateLegalMoves(Vector3 position)
        {
            PossibleMoves.Clear();
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);
            for (var i = 1; i <= Board.Size; i++)
            {
                #region Straight
                AddIfValid(positionCell.x + i, positionCell.y);
                AddIfValid(positionCell.x - i, positionCell.y);
                AddIfValid(positionCell.x, positionCell.y + i);
                AddIfValid(positionCell.x, positionCell.y - i);
                #endregion
                #region Diagonal
                AddIfValid(positionCell.x + i, positionCell.y + i);
                AddIfValid(positionCell.x + i, positionCell.y - i);
                AddIfValid(positionCell.x - i, positionCell.y + i);
                AddIfValid(positionCell.x - i, positionCell.y - i);
                #endregion
            }
        }

    }
}