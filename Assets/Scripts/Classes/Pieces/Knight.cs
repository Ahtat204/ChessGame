using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using UnityEngine;
using Assets.Scripts.Classes.GameClasses;

namespace Assets.Scripts.Classes.Pieces
{
    public class Knight : Piece
    {
        [SerializeField] private PieceColor color;
        public override List<Vector2Int> PossibleMoves => CalculateLegalMoves(transform.position);
        public override uint Value => 3;
        public override PieceColor Color => color;
        protected override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
         var legalMoves = new List<Vector2Int>(8);
         var positionCell = (Vector2Int)Board.BoardInstance.Tilemap.WorldToCell(position);
         legalMoves.Add(new Vector2Int(positionCell.x+2, positionCell.y+1));
         legalMoves.Add(new Vector2Int(positionCell.x+2, positionCell.y-1));
         legalMoves.Add(new Vector2Int(positionCell.x-2, positionCell.y+1));
         legalMoves.Add(new Vector2Int(positionCell.x-2, positionCell.y-1));
         legalMoves.Add(new Vector2Int(positionCell.x+1, positionCell.y+2));
         legalMoves.Add(new Vector2Int(positionCell.x+1, positionCell.y-2));
         legalMoves.Add(new Vector2Int(positionCell.x-1, positionCell.y-2));
         legalMoves.Add(new Vector2Int(positionCell.x-1, positionCell.y+2));
         legalMoves.Remove(positionCell);
         var filteredMovesList=legalMoves.Where(pos => pos is { x: >= 1 and <= 8, y: >= 1 and <= 8 }).ToList();
         return filteredMovesList;

        }
    }
}