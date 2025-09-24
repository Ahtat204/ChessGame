using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.BehaviorClasses;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    /// <summary>
    /// Bishop class
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(MovementManager))]
    [RequireComponent(typeof(SelectableDecorator))]
    public class Bishop : Piece
    {
        /// <summary>
        /// in the chess community , it's known that a Bishop is stronger than a Knight , but for simplicity , we'll assume they're equal "in value"  
        /// </summary>
        public override uint Value => 3;
        public override List<Vector2Int> PossibleMoves => CalculateLegalMoves(transform.position);
        [field: SerializeField]
        public override PieceColor Color { get; protected set; }
        protected sealed override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            var legalMoves = new List<Vector2Int>(14);
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);
            for (var i = 0; i <= Board.Size; i++)
            {
                legalMoves.Add(new Vector2Int(positionCell.x + i, positionCell.y + i));
                legalMoves.Add(new Vector2Int(positionCell.x + i, positionCell.y - i));
                legalMoves.Add(new Vector2Int(positionCell.x - i, positionCell.y + i));
                legalMoves.Add(new Vector2Int(positionCell.x - i, positionCell.y - i));
            }

            legalMoves.Remove(positionCell);
            var filteredList = legalMoves.Where(pos => pos is { x: >= 1 and <= 8, y: >= 1 and <= 8 }).ToList();
            return filteredList;
        }
    }
}