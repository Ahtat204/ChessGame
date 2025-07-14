using Assets.Scripts.Enums;
using UnityEngine;
using Square = UnityEngine.Vector2;
using Squares = System.Collections.Generic.List<UnityEngine.Vector2>;

namespace Assets.Scripts.Classes.Pieces
{
    public class Rook : Piece

    {
        [SerializeField] private uint _value;
        [SerializeField] private PieceColor _pieceColor;
        public override PieceColor Color => _pieceColor;
        protected override Squares CalculateLegalMoves(Vector2 piecePosition)
        {
            return base.CalculateLegalMoves(piecePosition);
        }

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
    }
}