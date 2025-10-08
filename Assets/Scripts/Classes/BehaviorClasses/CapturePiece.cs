using System.Collections.Generic;
using System.Linq;
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
        private static List<CapturePiece> _movedPiece;
        public bool canCapture;//this field should be used in the MovementManager class to prevent a piece from moving to a square occupied by a friendly piece
        private Vector3 _currPosition;
        private Vector3 _newPos;

        private void Awake()
        {
            _movedPiece = new(32);
            _currPosition = transform.position;
            _newPos = new();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _newPos = transform.position;
            canCapture = !other.gameObject.CompareTag(gameObject.tag) && !other.gameObject.CompareTag(nameof(King));
            if (!canCapture) return;
            _movedPiece.Add(this);
            foreach (var piece in _movedPiece.Where(piece => piece._currPosition != piece._newPos))
            {
                Destroy(other.gameObject);
            }
            _currPosition = _newPos;
            Debug.Log("new position is "+_newPos);
            Debug.Log("Current position is "+_currPosition);
            _movedPiece.Clear();
        }
    }
}