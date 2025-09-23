using System;
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
        public bool hasMoved;

        private void Awake()
        {
            _selectableDecorator = GetComponent<SelectableDecorator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _piece = GetComponent<Piece>();
            _target = transform.position;
            hasMoved = false;
        }
#if UNITY_STANDALONE_WIN || WINDOWS_UWP || UNITY_EDITOR
        public void MovePiece()
        {
            
            if (Input.GetMouseButtonDown(0) && _selectableDecorator.Status == SelectionStatus.Selected)
            {
                _target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                _target.z = 0;
            }
            var target = Board.BoardInstance.Tilemap.WorldToCell(_target);
            var tar = Board.BoardInstance.Tilemap.GetCellCenterWorld(target);
            _rigidbody.MovePosition(_piece.PossibleMoves.Contains((Vector2Int)target) ? tar : transform.position);
          /* if (_piece.PossibleMoves.Contains((Vector2Int)target))
            {
              this.hasMoved = true;
                _selectableDecorator.Status = SelectionStatus.UnSelected;
            }*/
        }
#endif
        private void Update()
        {
            MovePiece();
        }
    }
}