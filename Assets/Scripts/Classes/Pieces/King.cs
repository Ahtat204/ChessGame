using System;
using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    public class King : Piece
    {
        public override List<Vector2Int> PossibleMoves { get; protected set; }
        private Rigidbody2D _rigidbody;

        /// <summary>
        /// King has no value since it can't be captured, often given infinite value
        /// </summary>
        public override uint Value { get; }


        protected override void Move(Vector3Int to)
        {
            if (!PossibleMoves.Contains((Vector2Int)to)) return;

            var worldPosition = Tilemap.GetCellCenterWorld(to);
            _rigidbody.MovePosition(worldPosition);
        }

        public override PieceColor Color { get; }

        protected override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public override void Awake()
        {
            base.Awake();
            if (CameraMain == null || Tilemap == null)
                throw new NullReferenceException("camera or Tilemap has not been assigned");
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}