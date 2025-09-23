using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Classes.GameClasses;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    public class Pawn : Piece
    {
        public override List<Vector2Int> PossibleMoves=> CalculateLegalMoves(transform.position);
        public override uint Value => 1; 
        [field: SerializeField]
        public override PieceColor Color { get; protected set; }
        protected sealed override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            var legalMoves= new List<Vector2Int>(3);
            var positionCell = (Vector2Int)Board.BoardInstance.Tilemap.WorldToCell(position);
            legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y+1));
            legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y+2));//only in first move
            legalMoves.Add(new Vector2Int(positionCell.x+1, positionCell.y+1));//en Passant,handled by Proxy classes
            legalMoves.Remove(positionCell);
            var filteredMovesList = legalMoves.Where(pos => pos is { x: >= 1 and <= 8, y: >= 1 and <= 8 }).ToList();
            return filteredMovesList;
        }
       
    }
}