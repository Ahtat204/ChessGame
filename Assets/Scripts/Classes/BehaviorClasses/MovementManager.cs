using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using Assets.Scripts.Classes;
using UnityEngine;


namespace Assets.Scripts.Classes.BehaviorClasses
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class MovementManager : MonoBehaviour
    {
        private SelectableDecorator _selectableDecorator;
        private Piece _piece;
        private Rigidbody2D _rigidbody;
        private Vector3 _target;
        private void Awake()
        {
            _selectableDecorator = GetComponent<SelectableDecorator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _piece = GetComponent<Piece>();
            _target = transform.position;
            _target.x=(int)_target.x;
            _target.y = (int)_target.y;
            _target.z = 0;
        }
        public void MovePiece(Vector3Int destination)
        {
            if (Input.GetMouseButtonDown(0) && _selectableDecorator.Status == SelectionStatus.Selected)
            {
                var position = transform.position;
                _target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                _target.z = position.z;
            }

            var target = (Vector2Int)Board.BoardInstance.Tilemap.WorldToCell(_target);
            _rigidbody.MovePosition(_piece.PossibleMoves.Contains((Vector2Int)destination)?destination:_target );
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _selectableDecorator.Status == SelectionStatus.Selected)
            {
                _target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                
            }

            var target = (Vector2Int)Board.BoardInstance.Tilemap.WorldToCell(_target);
            _rigidbody.MovePosition(_piece.PossibleMoves.Contains(target) ? _target : transform.position);
        }
    }
}