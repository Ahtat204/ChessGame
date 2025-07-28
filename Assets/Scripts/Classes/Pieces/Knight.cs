using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;
namespace Assets.Scripts.Classes.Pieces
{
    public class Knight : Piece
    {
        [SerializeField] private uint _value;

       
        public override uint Value
        {
            get => _value;
            protected set => _value = value;
        }


        protected override void Move(Coordinates p)
        {
            throw new System.NotImplementedException();
        }

        public override PieceColor Color { get; }
        protected override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            throw new System.NotImplementedException();
        }
    }
}