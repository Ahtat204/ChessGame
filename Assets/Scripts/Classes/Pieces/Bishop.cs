using System.Collections.Generic;
using System.Linq;
using ChessGame.Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    /// <summary>
    /// Bishop class
    /// </summary>
    public class Bishop : Piece
    {

        public override List<Vector2Int> PossibleMoves => CalculateLegalMoves(transform.position);
        [SerializeField] private PieceColor pieceColor;
        private Rigidbody2D _rigidbody;
/// <summary>
/// in the chess community , it's known that a Bishop is stronger than a Knight , but for simplicity , we'll assume they're equal "in value"  
/// </summary>
        public override uint Value => 3;

        public override PieceColor Color => pieceColor;


     


        protected override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            var legalMoves = new List<Vector2Int>();
            var positionCell = (Vector2Int)Tilemap.WorldToCell(position);
            for (var i = 0; i <= 8; i++)
            {
                legalMoves.Add(new Vector2Int(positionCell.x + i, positionCell.y + i));
                legalMoves.Add(new Vector2Int(positionCell.x + i, positionCell.y - i));
                legalMoves.Add(new Vector2Int(positionCell.x - i, positionCell.y + i));
                legalMoves.Add(new Vector2Int(positionCell.x - i, positionCell.y - i));
            }

            legalMoves.Remove(positionCell);
            var filtredMovesList = legalMoves.Where(pos => pos is { x: >= 1 and <= 8, y: >= 1 and <= 8 }).ToList();
            return filtredMovesList;
        }
        
        public override void Awake()
        {
            base.Awake();
            _rigidbody=GetComponent<Rigidbody2D>();
        }

       
    }
}