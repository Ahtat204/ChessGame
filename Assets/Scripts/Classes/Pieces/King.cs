using Assets.Scripts.Enums;
using UnityEngine;
using Square = UnityEngine.Vector2;
using Positions = System.Collections.Generic.List<UnityEngine.Vector2>;

namespace Assets.Scripts.Classes.Pieces
{
    public class King : Piece
    {
        [SerializeField] private uint _value;

        protected override Vector2 position { get; }

        public override uint Value
        {
            get => _value;
            protected set => _value = value;
        }

        

       
        protected override void Move(Square p)
        {
            throw new System.NotImplementedException();
        }

        public override PieceColor Color { get; }
        protected override Positions CalculateLegalMoves(Vector2 piecePosition)
        {
            return base.CalculateLegalMoves(piecePosition);
        }
    }
}