using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    /// <summary>
    /// Bishop class
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PieceMovementComponent))]
    [RequireComponent(typeof(PieceSelectionComponent))]
    public sealed class Bishop : Piece
    { 
        public override uint Value => 3;

        // 2. Property returns the SAME list every time
        public override List<Vector2Int> PossibleMoves { get; } = new List<Vector2Int>(14);

        [field: SerializeField]
        public override PieceColor Color { get; protected set; }
        public  override void CalculateLegalMoves(Vector3 position)
        {
            PossibleMoves.Clear();
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);
            for (var i = 1; i <= Board.Size; i++)
            {
                AddIfValid(positionCell.x + i, positionCell.y + i);
                AddIfValid(positionCell.x + i, positionCell.y - i);
                AddIfValid(positionCell.x - i, positionCell.y + i);
               AddIfValid(positionCell.x - i, positionCell.y - i);
            }
        }
       
    }
}