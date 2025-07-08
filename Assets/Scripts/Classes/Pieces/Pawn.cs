using Assets.Scripts.Classes;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Classes.Pieces
{
    public class Pawn : Piece, IMove, IPromotable
    {
        protected override uint Value { get; }

        public void Move()
        {
            throw new System.NotImplementedException();
        }

        Piece IPromotable.Promote(Pawn pawn)
        {
            throw new System.NotImplementedException();
        }
    }
}