using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;
namespace Assets.Scripts.Classes.Pieces

{
    public class Rook : Piece
    {
        [SerializeField] private uint _value;
        [SerializeField] private PieceColor _pieceColor;

        protected override void Move(Coordinates p)
        {
            throw new System.NotImplementedException();
        }

        public override PieceColor Color => _pieceColor;

        protected override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public override uint Value
        {
            get => _value;
            protected set => _value = value;
        }
    }
}