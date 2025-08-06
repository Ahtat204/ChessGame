using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces

{
    public class Rook : Piece
    {
        [SerializeField] private PieceColor pieceColor;
        public override List<Vector2Int> PossibleMoves{ get;protected set;}
        private Rigidbody2D _rigidbody;

        protected override void Move(Vector3Int to)
        {
            if (!PossibleMoves.Contains((Vector2Int)to)) return;

            var worldPosition = Tilemap.GetCellCenterWorld(to);
            _rigidbody.MovePosition(worldPosition);
        }

        public override PieceColor Color => pieceColor;

        protected override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public override uint Value => 5;
        public override void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}