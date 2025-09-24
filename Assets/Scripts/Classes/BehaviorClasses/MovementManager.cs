using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using Assets.Scripts.Classes;
using Unity.VisualScripting;
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
        private List<MovementManager> _movementManagers= new (32);
        public Vector3Int CurrPos{get; private set;}
        public bool HasMoved { get; private set; }

        private void Awake()
        {
            _movementManagers.Add(this);
            _selectableDecorator = GetComponent<SelectableDecorator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _piece = GetComponent<Piece>();
            _target = transform.position;
            HasMoved = false;
            
        }

        private void Start()
        {
            CurrPos=Board.BoardInstance.tilemap.WorldToCell(transform.position);
        }

        public void MovePiece()
        {
            if (Input.GetMouseButtonDown(0) && _selectableDecorator.Status == SelectionStatus.Selected)
            {
                _target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                _target.z = 0;
            }
            var target = Board.BoardInstance.tilemap.WorldToCell(_target);
            var tar = Board.BoardInstance.tilemap.GetCellCenterWorld(target);
            _rigidbody.MovePosition(_piece.PossibleMoves.Contains((Vector2Int)target) ? tar : transform.position);
            HasMoved=CurrPos != target;
            
                 CurrPos = target;
             if (HasMoved)
             {
                 _selectableDecorator.Status = SelectionStatus.UnSelected;
             }
             HasMoved = false;
        }

        private void Update()
        {
            MovePiece();
        }
    }
}