using Assets.Scripts.Classes;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;

namespace Assets.Scripts.Classes.Pieces
{
    //Bishop Class
    public class Bishop : Piece, IMove
    {
        protected override uint Value { get; }

        public void Move()
        {
            throw new System.NotImplementedException();
        }
    }
}