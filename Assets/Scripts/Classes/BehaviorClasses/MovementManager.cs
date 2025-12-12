using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.Pieces;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEditor;
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
    public class MovementManager : MonoBehaviour, ICommand
    {
        /// <summary>
        /// this variable will prevent penetration , passing through pieces , either friendly or enemy pieces
        /// </summary>
        bool CanMove { get; set; }

        private SelectableDecorator _selectableDecorator;
        public Piece _piece { get; private set; }
        private Vector3 _target;
        private Vector2 _currentPosition;

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
            GameManager.Instance._pieces ??= new();
            GameManager.Instance._pieces.Add((Vector2Int)CurrPos, this);
            GameManager.Instance.OnExecute += Execute;
            GameManager.Instance.OnExecute += SwitchScripts;
            //GameManager.Instance.OnExecute?.Invoke();

            //    GameManager.Instance.OnPieceMovedEvent += Execute;
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

       
        
        public void SwitchScripts()
        { /*
            if (GameManager.Instance._turn is Turn.WhitePlayer)
            {
                if (_piece.Color == PieceColor.White)
                {
                    gameObject.SetActive(false);
                }
                if (_piece.Color == PieceColor.Black)
                {
                   gameObject.SetActive(true);
                }
            }
            if (GameManager.Instance._turn is Turn.BlackPlayer )
            {
                if (_piece.Color == PieceColor.Black)
                {
                   // enabled=true;
                   gameObject.SetActive(true);
                }
                if (_piece.Color == PieceColor.White)
                {
                   // enabled=false;
                   gameObject.SetActive(false);
                }
            }*/
            switch (GameManager.Instance._turn)
            {
                case Turn.WhitePlayer:
                    
                    break;
                case Turn.BlackPlayer:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        //void switchTurn(Turn turn)=> turn = turn==Turn.WhitePlayer?Turn.BlackPlayer:Turn.WhitePlayer;
            
        /// <summary>
        /// Moves the piece to the target cell if it represents a valid move.
        /// Updates the piece’s board position and movement status.
        /// </summary>
        private void MovePiece(Dictionary<Vector2Int, MovementManager> pieces)
        {
            
            if (!CanMove) return;
            var targetCell = Board.BoardInstance.tilemap.WorldToCell(_target);
            var worldCellCenter = Board.BoardInstance.tilemap.GetCellCenterWorld(targetCell);
            if (!_piece.PossibleMoves.Contains((Vector2Int)targetCell)) return;
            var occupied = pieces.ContainsKey((Vector2Int)targetCell) ? pieces[(Vector2Int)targetCell] : null;
            if (occupied is null)
            {
                transform.position = Vector2.MoveTowards(_target, (Vector2)worldCellCenter, 10);
                //switchTurn(GameManager.Instance._turn);
              //  Debug.Log(GameManager.Instance._turn);
            }

            if (occupied is not null)
            {
                if (occupied._piece.Color == _piece.Color) return;
                if (occupied._piece.Color != _piece.Color)
                {
                    if (occupied._piece is King) return;
                    transform.position = Vector2.MoveTowards(_target, (Vector2)worldCellCenter, 10);
                    
                    pieces.Remove((Vector2Int)targetCell);
                    pieces.Add((Vector2Int)targetCell, this);
                    GameManager.Instance.OnExecute -= occupied.Execute;
                    Destroy(occupied.gameObject);
                    //   switchTurn(GameManager.Instance._turn);
                 //   Debug.Log(GameManager.Instance._turn);
                    //  GameManager.Instance.OnPieceMovedEvent -= Execute;
                }
            }

            if (!targetCell.Equals(CurrPos))
            {
                pieces.Remove((Vector2Int)CurrPos);
                CurrPos = targetCell;
                pieces[(Vector2Int)targetCell] = this;
                if(GameManager.Instance._turn is Turn.WhitePlayer) GameManager.Instance._turn=Turn.BlackPlayer;
                if (GameManager.Instance._turn is Turn.BlackPlayer) GameManager.Instance._turn=Turn.WhitePlayer;
                HasMoved = true;
                _selectableDecorator.Status = SelectionStatus.UnSelected;
            }
        }

        public void Execute()
        {
            HandleInput();
            MovePiece(GameManager.Instance._pieces);
           // Debug.Log("Moved");
        }

        public void Undo()
        {
            throw new NotImplementedException();
        }
    }
}