using Assets.Scripts.Classes;
using Assets.Scripts.Classes.Pieces;

namespace Assets.Scripts.Interfaces
{
    public interface IPromotable
    {
        protected Piece Promote(Pawn pawn);
    }
}