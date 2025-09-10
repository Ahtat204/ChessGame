using Assets.Scripts.Classes;
using Assets.Scripts.Classes.Pieces;

namespace Assets.Scripts.Interfaces
{

    /// <summary>
    /// interface for pieces that can be promoted
    /// </summary>
    public interface IPromotable
    {
        public Piece Promote(Pawn pawn);
    }
}