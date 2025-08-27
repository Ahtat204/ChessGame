using System.Collections.Generic;
using System;
using System.Linq;
using Assets.Scripts.Classes.BehaviorClasses;
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
            var legalMoves = new List<Vector2Int>();
            var positionCell = (Vector2Int)Board.BoardInstance.Tilemap.WorldToCell(position);
            for (var i = 1; i <= Board.Size; i++)
            {
                legalMoves.Add(new Vector2Int(positionCell.x + i, positionCell.y));
                legalMoves.Add(new Vector2Int(positionCell.x - i, positionCell.y));
                legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y + i));
                legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y - i));
                legalMoves.Add(new Vector2Int(positionCell.x + i, positionCell.y + i));
                legalMoves.Add(new Vector2Int(positionCell.x + i, positionCell.y - i));
                legalMoves.Add(new Vector2Int(positionCell.x - i, positionCell.y + i));
                legalMoves.Add(new Vector2Int(positionCell.x - i, positionCell.y - i));
            }

            legalMoves.Remove(positionCell);
            var filteredMovesList =
                legalMoves.Where(pos => pos is { x: >= 1 and <= 8, y: >= 1 and <= 8 }).ToList();
            return filteredMovesList;
        }

        public override void Awake()
        {
            base.Awake();
           
        }
    }
}