using System.Collections.Generic;
using Assets.Scripts.Classes.Pieces;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.BehaviorClasses.KingCastling
{
    public abstract class KingDecorator:ICastle
    {
        private readonly ICastle _castle;
        private readonly KingCastling _kingCastling;
        private readonly Rook _rook;

        protected KingDecorator(ICastle castle , KingCastling king, Rook rook)
        {
            _castle = castle;
            _kingCastling = king;
            _rook = rook;
        }


        public void CastleKing(King king)
        {
            if(!_kingCastling.canCastle) return;
        }
    }

    public class KingCastleDecorator : KingDecorator
    {
        
        public KingCastleDecorator(ICastle castle,KingCastling king,Rook rook):base(castle,king,rook) {}
        
    }
}