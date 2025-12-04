using System;
using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes.BehaviorClasses
{
    /// <summary>
    /// Manages movement and capture logic for a chess piece.
    /// Handles player input, updates piece position, and resolves captures.
    /// </summary>
    [RequireComponent(typeof(Piece))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(SelectableDecorator))]
    public class MovementManager : MonoBehaviour
    {
        /// <summary>
        /// this variable will prevent penetration , passing through pieces , either friendly or enemy pieces
        /// </summary>
        public bool CanMove { get; private set; }

        private SelectableDecorator _selectableDecorator;

        public Piece _piece { get; private set; }

        // private Rigidbody2D _rigidbody;
        // public event Action<MovementManager> OnMoveCompleted;
        private Vector3 _target;

        // private bool _canCapture;
        // private Vector3 _destination;
        private Vector2 _currentPosition;
        //   private Vector3 _difference;

        /// <summary>
        /// Gets or sets the current board position of the piece in grid coordinates.
        /// </summary>
        public Vector3Int CurrPos { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the piece has moved during the current turn.
        /// </summary>
        public bool HasMoved { get; private set; }

        /// <summary>
        /// Called when the component is initialized.
        /// Initializes component references and default state values.
        /// </summary>
        private void Awake()
        {
            CanMove = true;
            //_difference = Vector3.zero;
            _selectableDecorator = GetComponent<SelectableDecorator>();
            _piece = GetComponent<Piece>();
        }

        /// <summary>
        /// Called before the first frame update.
        /// Sets the initial target position and determines the piece’s current board cell.
        /// </summary>
        private void Start()
        {
            _target = transform.position;
            HasMoved = false;
            CurrPos = Board.BoardInstance.tilemap.WorldToCell(transform.position);
            Debug.Log(CurrPos);
        }

        /// <summary>
        /// Handles mouse input when the piece is selected.
        /// Updates the target position based on the cursor location.
        /// </summary>
        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0) && _selectableDecorator.Status == SelectionStatus.Selected)
            {
                _target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                _target.z = 0;
            }
        }

        /// <summary>
        /// Moves the piece to the target cell if it represents a valid move.
        /// Updates the piece’s board position and movement status.
        /// </summary>
        public void MovePiece(Dictionary<Vector2Int, MovementManager> pieces)
        {
            if (!CanMove) return;
            var targetCell = Board.BoardInstance.tilemap.WorldToCell(_target);
            var worldCellCenter = Board.BoardInstance.tilemap.GetCellCenterWorld(targetCell);
            if (!_piece.PossibleMoves.Contains((Vector2Int)targetCell)) return;
            var occupied=pieces.ContainsKey((Vector2Int)targetCell)?pieces[(Vector2Int)targetCell]:null;
            if (occupied is null)
            {
                transform.position = Vector2.MoveTowards(_target, (Vector2)worldCellCenter, 10);
                
            }
            if (occupied is not null)
            {
                if(occupied._piece.Color == _piece.Color) return;
                if (occupied._piece.Color != _piece.Color)
                {
                    transform.position = Vector2.MoveTowards(_target, (Vector2)worldCellCenter, 10);
                    pieces.Remove((Vector2Int)targetCell);
                    pieces.Add((Vector2Int)targetCell, this);
                    Destroy(occupied.gameObject); 
                }
            }
            if (!targetCell.Equals(CurrPos))
            {
                CurrPos = targetCell;
                pieces.Remove((Vector2Int)CurrPos);
                pieces[(Vector2Int)targetCell] = this;
                HasMoved = true;
                _selectableDecorator.Status = SelectionStatus.UnSelected;
            }
            
        }

/*
        /// <summary>
        /// Captures the initial mouse offset relative to the piece’s position when clicked.
        /// </summary>
        private void OnMouseDown()
        {
            _difference = (Vector2)Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition) -
                          (Vector2)transform.position;
        }

        /// <summary>
        /// Updates the target position based on mouse drag.
        /// </summary>
        private void OnMouseDrag()
        {
            _target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition) - _difference;
        }

        /// <summary>
        /// Called when the mouse button is released.
        /// Finalizes the move if the target cell is valid.
        /// </summary>
        private void OnMouseUp()
        {
            var targetCell = Board.BoardInstance.tilemap.WorldToCell(_target);
            _destination = Board.BoardInstance.tilemap.GetCellCenterWorld(targetCell);
            if (!_piece.PossibleMoves.Contains((Vector2Int)targetCell)) return;
            if (_selectableDecorator.Status != SelectionStatus.UnSelected) return;
            if (!CanMove) return;
            // transform.position = _destination;
            _rigidbody.MovePosition(_destination);
        }


        /// <summary>
        /// Handles collision detection and piece capture logic.
        /// If a valid opposing piece is detected, it is destroyed upon capture.
        /// </summary>
        /// <param name="other">The collider of the other game object.</param>
        private void OnTriggerEnter2D(Collider2D other)
        {
            _canCapture = !other.gameObject.CompareTag(gameObject.tag) && !other.gameObject.CompareTag("King");
            var mom = other.GetComponent<MovementManager>();
            if (!_canCapture || mom is null) return;
            /*Debug.Log(other.gameObject.name+mom.HasMoved);
            Debug.Log(gameObject.name+HasMoved);
                Destroy(other.gameObject);
                Debug.Log(other.gameObject.name + mom.HasMoved);

        }
*/
        private void Update()
        {
            HandleInput();
        }
    }
}