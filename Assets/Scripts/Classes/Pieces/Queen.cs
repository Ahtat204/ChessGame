using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    public class Queen : Piece
    {
        /// <summary>
        /// List of the Legal Moves for the Queen,here it's a readonly property .<remarks>it's been assigned here to avoid any  NullReferenceException</remarks>
        /// </summary>
        public override List<Vector2Int> PossibleMoves => CalculateLegalMoves(transform.position);
        /// <summary>
        /// inspector-initialized field 
        /// </summary>
        [SerializeField] private PieceColor pieceColor;
        public override uint Value => 9;
        public override PieceColor Color => pieceColor;

        /// <summary>
        /// function will return all the legal moves the queen can do 
        /// </summary>
        /// <param name="position">this is will be the transform.position of the GameObject, The Queen</param>
        /// <returns>return a List of Vector2Int of all the possible moves</returns>
        protected override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            var legalMoves = new List<Vector2Int>(28);
            var positionCell = (Vector2Int)Board.BoardInstance.Tilemap.WorldToCell(position);
            for (var i = 1; i <= Board.Size; i++)
            {
                #region Straight
                legalMoves.Add(new Vector2Int(positionCell.x + i, positionCell.y));
                legalMoves.Add(new Vector2Int(positionCell.x - i, positionCell.y));
                legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y + i));
                legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y - i));
                #endregion
                #region Diagonal
                legalMoves.Add(new Vector2Int(positionCell.x + i, positionCell.y + i));
                legalMoves.Add(new Vector2Int(positionCell.x + i, positionCell.y - i));
                legalMoves.Add(new Vector2Int(positionCell.x - i, positionCell.y + i));
                legalMoves.Add(new Vector2Int(positionCell.x - i, positionCell.y - i));
                #endregion
            }
            legalMoves.Remove(positionCell);
            var filteredMovesList =
                legalMoves.Where(pos => pos is { x: >= 1 and <= 8, y: >= 1 and <= 8 }).ToList();
            return filteredMovesList;
        }

    }
}