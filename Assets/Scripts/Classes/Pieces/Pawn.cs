using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Structs;
using UnityEngine;


namespace Assets.Scripts.Classes.Pieces
{
    public class Pawn : Piece, IPromotable
    {
        private bool _isFirstMove;
        public override List<Vector2Int> PossibleMoves{ get;protected set;}
        public override uint Value => 1; 
        protected override void Move(Vector3Int from)
        {
            throw new System.NotImplementedException();
        }
        public override PieceColor Color { get; }
        protected override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            throw new System.NotImplementedException();
        }
        Piece IPromotable.Promote(Pawn pawn)
        {
            throw new System.NotImplementedException();
        }
    }
}