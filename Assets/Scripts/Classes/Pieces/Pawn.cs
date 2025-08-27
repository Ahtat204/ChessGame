using System;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    public class Pawn : Piece, IPromotable
    {
        private bool _isFirstMove;
        public override List<Vector2Int> PossibleMoves=> CalculateLegalMoves(transform.position);
        public override uint Value => 1; 
       
        public override PieceColor Color { get; }
        protected override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            throw new NotImplementedException();
        }
        Piece IPromotable.Promote(Pawn pawn)
        {
            throw new NotImplementedException();
        }
    }
}