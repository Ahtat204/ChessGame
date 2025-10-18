using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.Pieces;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes.BehaviorClasses
{
    [RequireComponent(typeof(Piece))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(SelectableDecorator))]
    internal sealed class MovementManager : MonoBehaviour
    {
        private SelectableDecorator _selectableDecorator;
        private Piece _piece;
        private Rigidbody2D _rigidbody;
        private Vector3 _target;
        private bool _canCapture;
        private Vector3Int CurrPos { get; set; }
        public bool HasMoved { get; set; }

        private void Awake()
        {
            _selectableDecorator = GetComponent<SelectableDecorator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _piece = GetComponent<Piece>();
            _target = transform.position;
            HasMoved = false;
        }

        private void Start()
        {
            CurrPos = Board.BoardInstance.tilemap.WorldToCell(transform.position);
        }

        private void Update()
        {
            HandleInput();
            MovePiece();
        }
        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0) && _selectableDecorator.Status == SelectionStatus.Selected)
            {
                _target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                _target.z = 0;
            }
        }

        private void MovePiece()
        {
            var targetCell = Board.BoardInstance.tilemap.WorldToCell(_target);
            var worldCellCenter = Board.BoardInstance.tilemap.GetCellCenterWorld(targetCell);
            if (!_piece.PossibleMoves.Contains((Vector2Int)targetCell)) return;
            _rigidbody.MovePosition(worldCellCenter);
            if ( targetCell != CurrPos)
            {
                CurrPos = targetCell;
                HasMoved = true;
                _selectableDecorator.Status = SelectionStatus.UnSelected;  
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _canCapture = !other.gameObject.CompareTag(gameObject.tag) && !other.gameObject.CompareTag("King");
            var mom=other.GetComponent<MovementManager>(); //this was the critical line
            if (!_canCapture) return;
            if (HasMoved && mom is not null && !mom.HasMoved)
            {
                Destroy(other.gameObject);
            }
            HasMoved = false;
        }
    }
}