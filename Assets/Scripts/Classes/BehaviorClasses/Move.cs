using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.GameClasses;
using UnityEngine;
using static System.Console;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Classes.BehaviorClasses
{
    [RequireComponent(typeof(Piece))]
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Move : MonoBehaviour
    {
        private SelectableDecorator _selectableDecorator;
        private Piece _piece;
        private Rigidbody2D _rigidbody;
        private Tilemap _tilMap;
        private List<Vector2Int> _possibleMoves;


//
        private Vector3 _target;
        private bool _isSelected;
        public static List<Move> MovableObjects = new List<Move>();
//

        private void Awake()
        {
            _selectableDecorator = GetComponent<SelectableDecorator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _tilMap = Board.BoardInstance.Tilemap;
            _piece = gameObject.GetComponent<Piece>();
            _possibleMoves = _piece.PossibleMoves;
        }

        private void Start()
        {
            
            _target = transform.position;
        }

        public void MovePiece(Vector3Int to)
        {
            if (!_piece.PossibleMoves.Contains((Vector2Int)to)) return;

            var worldPosition = _tilMap.GetCellCenterWorld(to);
            _rigidbody.MovePosition(worldPosition);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && _selectableDecorator.IsSelected)
            {
                _target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                _target.z = transform.position.z;
            }
            
             var target=(Vector2Int)Board.BoardInstance.Tilemap.WorldToCell(_target);
             _rigidbody.MovePosition(_piece.PossibleMoves.Contains(target)?_target:transform.position);
          
        }

        
    }
}