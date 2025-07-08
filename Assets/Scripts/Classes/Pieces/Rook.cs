using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Classes.Pieces
{
    public class Rook : Piece, IMove

    {
        protected override uint Value { get; }

        public void Move()
        {
            throw new System.NotImplementedException();
        }
    }
}