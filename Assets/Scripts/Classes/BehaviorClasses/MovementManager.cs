using System;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using Assets.Scripts.Classes;
using Unity.VisualScripting;
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
        ///       this variable will prevent penetration , passing through pieces , either friendly or enemy pieces
        /// </summary>
        private bool CanMove { get; set; }

        private SelectableDecorator _selectableDecorator;
        [SerializeField] private GameManager gameManager;
        public Piece _piece { get; private set; }
        private Rigidbody2D _rigidbody;
        private Vector3 _target;
        public event Action<MovementManager> OnMoveCompleted;
        private bool _canCapture;
        private Vector3 _destination;
        private Vector2 _currentPosition;
        private Vector3 _difference;

        /// <summary>
        /// Gets or sets the current board position of the piece in grid coordinates.
        /// </summary>
        private Vector3Int CurrPos { get; set; }

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
            _difference = Vector3.zero;
            _selectableDecorator = GetComponent<SelectableDecorator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _piece = GetComponent<Piece>();
            HasMoved = false;
        }

        /// <summary>
        /// Called before the first frame update.
        /// Sets the initial target position and determines the piece’s current board cell.
        /// </summary>
        private void Start()
        {
            Singleton<GameManager>.Instance.RegisterPiece(this);
            /*if(_piece is null) return;
            if (this._piece.Color == PieceColor.White)
            {
                Singleton<GameManager>.Instance.whitePieces.Add(this);
            }
            else if (this._piece.Color == PieceColor.Black)
            {
                Singleton<GameManager>.Instance.blackPieces.Add(this);
            }*/
            _target = transform.position;
            CurrPos = Board.BoardInstance.tilemap.WorldToCell(transform.position);
        }


        void FinishMovement()
        {
            if (!HasMoved)
            {
                HasMoved = true;
                OnMoveCompleted?.Invoke(this);
            }
        }

        /// <summary>
        /// Called once per frame to handle input and update the piece’s position.
        /// </summary>
        private void Update()
        {
            HandleInput();
            MovePiece();
        }


        public void SwitchTurn(Turn turn)
        {
            if (!HasMoved) return;
            if (turn != Singleton<GameManager>.Instance.turn)
            {
                Singleton<GameManager>.Instance.turn = turn;
            }
        }

        public void SetCanMove(bool value) => CanMove = value;

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
        ///    1.  Moves the piece to the target cell if it represents a valid move.
        ///      Updates the piece’s board position and movement status.
        /// </summary>
        private void MovePiece()
        {
            if (!CanMove) return;
            var targetCell = Board.BoardInstance.tilemap.WorldToCell(_target);
            var worldCellCenter = Board.BoardInstance.tilemap.GetCellCenterWorld(targetCell);
            if (!_piece.PossibleMoves.Contains((Vector2Int)targetCell)) return;
            _rigidbody.MovePosition(worldCellCenter);
            if (targetCell != CurrPos)
            {
                CurrPos = targetCell;
                FinishMovement();
                _selectableDecorator.Status = SelectionStatus.UnSelected;
                
            }
            HasMoved = false;
        }

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
            if(mom is null) return;
            if (!_canCapture ) return;
            Debug.Log("(" + HasMoved + "\r" + gameObject.name + ")" + "and" + "(" + mom.gameObject.name + "" +
                      mom.HasMoved);
            if (HasMoved && !mom.HasMoved)
            {
                Destroy(other.gameObject);
            }
        }
    }
}