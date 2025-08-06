using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    public class Knight : Piece
    {
        [SerializeField] private PieceColor color;
        public override List<Vector2Int> PossibleMoves{ get;protected set;}
        public override uint Value => 3;


        protected override void Move(Vector3Int from)
        {
            throw new System.NotImplementedException();
        }

        public override PieceColor Color => color;

        protected override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            throw new System.NotImplementedException();
        }
        public override void Awake()
        {
            base.Awake();
        }
    }
}