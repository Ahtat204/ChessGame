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
        private Piece _piece;
        private BoxCollider2D _boxCollider;
        public bool canCapture;

        private void Awake()
        {
            _piece = GetComponent<Piece>();
            _boxCollider = GetComponent<BoxCollider2D>();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            canCapture = !other.gameObject.CompareTag(gameObject.tag);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (canCapture)
            {
                Destroy(other.gameObject);
            }
        }

       
    }
}