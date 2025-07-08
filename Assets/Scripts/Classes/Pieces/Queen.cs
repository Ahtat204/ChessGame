using Assets.Scripts.Interfaces;


namespace Assets.Scripts.Classes.Pieces
{
    public class Queen : Piece, IMove
    {
        protected override uint Value => 9;


        public void Move()
        {
            throw new System.NotImplementedException();
        }
    }
}