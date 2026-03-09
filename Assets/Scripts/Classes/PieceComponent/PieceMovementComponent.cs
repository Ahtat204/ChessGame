using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.GameClasses.Proxies;
using Assets.Scripts.Classes.Pieces;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.PieceComponent
{
    /// <summary>
    /// Manages movement and capture logic for a chess piece.
    /// Handles player input, updates piece position, and resolves captures.
    /// </summary>
    /// <!--this class is working correcly-->
    [RequireComponent(typeof(Piece))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(PieceSelectionComponent))]
    public class PieceMovementComponent : MonoBehaviour, IMove
    {
        #region fields&props

        protected Piece _piece;

        /// <summary>
        /// this variable will prevent penetration , passing through pieces , either friendly or enemy pieces
        /// </summary>
        public bool CanMove { get; set; }
        
        /// <summary>
        /// Gets or sets the current board position of the piece in grid coordinates.
        /// </summary>
        public Vector3Int CurrPos { get;private set; }

        #endregion

        #region methods

        /// <summary>
        /// Called when the component is initialized.
        /// Initializes component references and default state values.
        /// </summary>
        private void Awake()
        {
            _piece = GetComponent<Piece>();
         //   _pieceSelectionComponent = GetComponent<PieceSelectionComponent>();
            CanMove = _piece.Color != PieceColor.Black;
            CanMove = true;
        }

        /// <summary>
        /// Called before the first frame update.
        /// Sets the initial target position and determines the piece’s current board cell.
        /// </summary>
        private void Start()
        {
            CurrPos = Board.BoardInstance.tilemap.WorldToCell(transform.position);
            GameManager.Instance.Pieces ??= new();
            GameManager.Instance.Pieces?.Add((Vector2Int)CurrPos, this);
        }
        
     

        public virtual void MovePiece(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2 targetPos)
        {
            if (!CanMove) return;
            var targetCell = Board.BoardInstance.tilemap.WorldToCell(targetPos);
            bool checkPath = PieceMovementProxy.CheckPath(pieces, (Vector2Int)CurrPos, (Vector2Int)targetCell);
            if (!checkPath && _piece is not Knight) return;
            var worldCellCenter = Board.BoardInstance.tilemap.GetCellCenterWorld(targetCell);
            if (!_piece.PossibleMoves.Contains((Vector2Int)targetCell)) return;
            var occupied = pieces.ContainsKey((Vector2Int)targetCell) ? pieces[(Vector2Int)targetCell] : null;
            if (occupied is null)
            {
                transform.position = Vector2.MoveTowards(transform.position, worldCellCenter, 10);
                if (!targetCell.Equals(CurrPos))
                {
                    pieces.Remove((Vector2Int)CurrPos);
                    CurrPos = targetCell;
                    pieces[(Vector2Int)targetCell] = this;
                }

                return;
            }

            if (occupied._piece.Color == _piece.Color) return;
            if (occupied._piece.Color != _piece.Color)
            {
                if (occupied._piece is King) return;
                transform.position = Vector2.MoveTowards(targetPos, worldCellCenter, 10);
                pieces.Remove((Vector2Int)targetCell);
                pieces.Add((Vector2Int)targetCell, this);
                Destroy(occupied.gameObject);
                if (!targetCell.Equals(CurrPos))
                {
                    pieces.Remove((Vector2Int)CurrPos);
                    CurrPos = targetCell;
                    pieces[(Vector2Int)targetCell] = this;
                }
                
            }
        }

        #endregion
    }
}