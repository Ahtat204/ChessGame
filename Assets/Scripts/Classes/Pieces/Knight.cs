using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    public class Knight : Piece
    {
        [SerializeField] private PieceColor color;
        public override List<Vector2Int> PossibleMoves => CalculateLegalMoves(transform.position);
        public override uint Value => 3;
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