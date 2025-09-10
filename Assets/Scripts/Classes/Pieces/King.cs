using System;
using Assets.Scripts.Classes.GameClasses;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    public class King : Piece
    {
        public override List<Vector2Int> PossibleMoves => CalculateLegalMoves(transform.position);
        /// <summary>
        /// inspector-initialized field 
        /// </summary>
        [SerializeField] private PieceColor pieceColor;
        /// <summary>
        /// King has no value since it can't be captured, often given infinite value or zero
        /// </summary>
        public override uint Value => 0;
        public override PieceColor Color => pieceColor;

        protected override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            var legalMoves = new List<Vector2Int>(8);
            var positionCell = (Vector2Int)Board.BoardInstance.Tilemap.WorldToCell(position);
            legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y + 1));
            legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y - 1));
            legalMoves.Add(new Vector2Int(positionCell.x - 1, positionCell.y));
            legalMoves.Add(new Vector2Int(positionCell.x + 1, positionCell.y));
            legalMoves.Add(new Vector2Int(positionCell.x - 1, positionCell.y - 1));
            legalMoves.Add(new Vector2Int(positionCell.x - 1, positionCell.y + 1));
            legalMoves.Add(new Vector2Int(positionCell.x + 1, positionCell.y - 1));
            legalMoves.Add(new Vector2Int(positionCell.x + 1, positionCell.y - 1));
            legalMoves.Remove(positionCell);
            var filteredMovesList = legalMoves.Where(pos => pos is { x: >= 1 and <= 8, y: >= 1 and <= 8 }).ToList();
            return filteredMovesList;
        }
    }
}