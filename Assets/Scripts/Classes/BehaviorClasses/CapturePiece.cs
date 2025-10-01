using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.Pieces;
using UnityEngine;

namespace Assets.Scripts.Classes.BehaviorClasses
{
    [RequireComponent(typeof(Piece))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(SelectableDecorator))]
    [RequireComponent(typeof(MovementManager))]
    
    public class CapturePiece : MonoBehaviour
    {
        private static List<CapturePiece> capturePieces = new (32);
        private MovementManager _movementManager;
        private Piece _piece;
        private BoxCollider2D _boxCollider;
        public bool canCapture;
        private Rigidbody2D _rb;
        private Vector3 currPosition;

        private void Awake()
        {
            capturePieces.Add(this);
            currPosition=transform.position;
            _rb = GetComponent<Rigidbody2D>();
            _movementManager = GetComponent<MovementManager>();
            _piece = GetComponent<Piece>();
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var newPos= transform.position;
            canCapture = !other.gameObject.CompareTag(gameObject.tag) && !other.gameObject.CompareTag(nameof(King));
            if(!canCapture) return;
            foreach (var piece in capturePieces.Where(cp => cp)) //still not correctly implemented
            {
                if (newPos != currPosition)
                {
                    Destroy(gameObject);
                }
            }
            newPos = currPosition;
            
        }

     
    }
}