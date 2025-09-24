using System;
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
        private MovementManager _movementManager;
        private Piece _piece;
        private BoxCollider2D _boxCollider;
        public bool canCapture;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _movementManager = GetComponent<MovementManager>();
            _piece = GetComponent<Piece>();
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            canCapture = !other.gameObject.CompareTag(gameObject.tag) && !other.gameObject.CompareTag("King");
            Debug.Log(canCapture);
            var rb = other.attachedRigidbody;
            if (!canCapture) return;
            if (rb is null) return;
            Destroy(rb.velocity.magnitude > _rb.velocity.magnitude ? gameObject : other.gameObject);
        }
    }
}