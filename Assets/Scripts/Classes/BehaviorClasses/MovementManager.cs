
using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.Pieces;
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
        public bool CanMove { get;  set; }

        private Piece _piece;

        /// <summary>
        /// Gets or sets the current board position of the piece in grid coordinates.
        /// </summary>
         Vector3Int CurrPos { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the piece has moved during the current turn.
        /// </summary>

        /// <summary>
        /// Called when the component is initialized.
        /// Initializes component references and default state values.
        /// </summary>
        private void Awake()
        {
            _piece = GetComponent<Piece>();
            CanMove=_piece.Color != PieceColor.Black;
        }

        /// <summary>
        /// Called before the first frame update.
        /// Sets the initial target position and determines the piece’s current board cell.
        /// </summary>
        private void Start()
        {
            CurrPos = Board.BoardInstance.tilemap.WorldToCell(transform.position);
            GameManager.Instance.Pieces ??= new();
            GameManager.Instance.Pieces.Add((Vector2Int)CurrPos, this);
        }

        /// <summary>
        /// Moves the piece to the target cell if it represents a valid move.
        /// Updates the piece’s board position and movement status.
        /// </summary>
        public void MovePiece(Dictionary<Vector2Int, MovementManager> pieces,Vector2 targetPos)
        {
            if (!CanMove) return;
            var targetCell = Board.BoardInstance.tilemap.WorldToCell(targetPos);
            var worldCellCenter = Board.BoardInstance.tilemap.GetCellCenterWorld(targetCell);
            if (!_piece.PossibleMoves.Contains((Vector2Int)targetCell)) return;
            var occupied = pieces.ContainsKey((Vector2Int)targetCell) ? pieces[(Vector2Int)targetCell] : null;
            if (occupied is null)
            {
                transform.position = Vector2.MoveTowards(targetPos, worldCellCenter, 10);
            }
            if (occupied is not null)
            {
                if (occupied._piece.Color == _piece.Color) return;
                if (occupied._piece.Color != _piece.Color)
                {
                    if (occupied._piece is King) return;
                    transform.position = Vector2.MoveTowards(targetPos, worldCellCenter, 10);

                    pieces.Remove((Vector2Int)targetCell);
                    pieces.Add((Vector2Int)targetCell, this);
                    Destroy(occupied.gameObject);
                }
            }
            if (!targetCell.Equals(CurrPos))
            {
                pieces.Remove((Vector2Int)CurrPos);
                CurrPos = targetCell;
                pieces[(Vector2Int)targetCell] = this;
            }
        }
    }
}