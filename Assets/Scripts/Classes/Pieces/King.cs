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


      

        public override PieceColor Color { get; }

        protected override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            throw new NotImplementedException();
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