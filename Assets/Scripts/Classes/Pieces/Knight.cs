using Assets.Scripts.Classes;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Classes.Pieces
{
    public class Knight : Piece, IMove
    {
        protected override uint Value { get; }

        public void Move()
        {
            throw new System.NotImplementedException();
        }
    }
}