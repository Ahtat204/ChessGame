using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.PieceComponent
{
    /// <summary>
    /// Handles the selection state and input orchestration for individual Chess pieces.
    /// </summary>
    /// <remarks>
    /// Acts as a bridge between the Unity Input System and the <see cref="CommandManager"/>.
    /// Implements a "Single Selection" pattern via a static reference to ensure 
    /// only one piece is active globally.
    /// </remarks>
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Piece))]
    [RequireComponent(typeof(PieceMovementComponent))]
    [RequireComponent(typeof(CommandManager))]
    public class PieceSelectionComponent : MonoBehaviour, ISelectable
    {
        /// <summary>
        /// Global reference to the currently active selection. 
        /// Ensures mutual exclusivity of piece selection across the board.
        /// </summary>
        private static PieceSelectionComponent _selectedPiece;

        /// <inheritdoc cref="ISelectable.Status"/>
        public SelectionStatus Status { get; set; }

        /// <summary>
        /// Internal state counter used to distinguish between the 'Selection' click 
        /// and the 'Targeting' click.
        /// </summary>
        private int Count { get; set; }

        private Piece _piece;

        /// <summary>
        /// Determines if the piece is allowed to act based on the current <see cref="GameManager.Turn"/>.
        /// </summary>
        public bool CanMove;
        

        /// <summary>
        /// Delegate for broadcast notifications when a valid movement target is finalized.
        /// </summary>
        public delegate void OnPieceSelected();

        /// <summary>
        /// Global event triggered when a piece selection lifecycle completes a movement instruction.
        /// </summary>
        public static event OnPieceSelected OnPieceSelectedEvent;

        /// <inheritdoc />
        public Vector2 Target => target;

        [SerializeField] private Vector2 target;

        private void Start()
        {
            Status = SelectionStatus.UnSelected;
            _piece = GetComponent<Piece>();
            CanMove = _piece.Color != PieceColor.Black;
        }

        /// <inheritdoc />
        public void OnSelect()
        {
          //  if (!CanMove) return;

            // Enforce Single Selection Policy
            if (_selectedPiece is not null && _selectedPiece != this)
            {
                _selectedPiece.OnDeselect();
            }

            _selectedPiece = this;
            Status = SelectionStatus.Selected;
            Count = 1;
        }
        /// <inheritdoc />
        public void OnDeselect()
        {
            if (_selectedPiece == this) _selectedPiece = null;
            Status = SelectionStatus.UnSelected;
            Count = 0;
        }

        /// <summary>
        /// Unity Callback: Detects clicks directly on the piece collider to toggle selection.
        /// </summary>
        private void OnMouseDown()
        {
            if (Status == SelectionStatus.Selected) OnDeselect();
            else OnSelect();
        }

        /// <summary>
        /// Orchestrates the 'Selection -> Target' input sequence.
        /// </summary>
        private void Update()
        {
            // Only process secondary clicks if this specific piece is the active selection
            if (Input.GetMouseButtonDown(0) && Status == SelectionStatus.Selected)
            {
                Count=1;
                // Vector Quantization: Transform screen touch to world coordinates
                target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
             
                Count = 2;
                if (Count > 1)
                {
                    // Fire movement instruction event
                    OnPieceSelectedEvent?.Invoke();
                    // Re-evaluate turn status post-action
                   
                }
            }
        }
    }
}