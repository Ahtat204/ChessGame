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

        private Vector3Int _clickedCell;

        /// <summary>
        /// Delegate for broadcast notifications when a valid movement target is finalized.
        /// </summary>
        public delegate void OnPieceSelected();

        /// <summary>
        /// Global event triggered when a piece selection lifecycle completes a movement instruction.
        /// </summary>
        public static event OnPieceSelected OnPieceSelectedEvent;

        /// <summary>
        /// The world-space coordinates of the movement destination.
        /// </summary>
        public Vector2 Target => target;
        
        [SerializeField] private Vector2 target;

        private void Start()
        {
            _clickedCell = Board.BoardInstance.tilemap.WorldToCell(target);
            Status = SelectionStatus.UnSelected;
            _piece = GetComponent<Piece>();
            
            // Initialization-time turn validation
            CanMove = Utility.SwitchTurn(GameManager.Instance.Turn, gameObject);
        }

        /// <summary>
        /// Transitions the piece to the Selected state and releases any previously held selection.
        /// </summary>
        public void OnSelect()
        {
            if (!CanMove) return;

            // Enforce Single Selection Policy
            if (_selectedPiece != null && _selectedPiece != this)
            {
                _selectedPiece.OnDeselect();
            }

            _selectedPiece = this;
            Status = SelectionStatus.Selected;
            Count = 1;
        }

        /// <summary>
        /// Resets the piece to an idle, unselected state.
        /// </summary>
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
                Count++;
                
                // Vector Quantization: Transform screen touch to world coordinates
                target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                _clickedCell = Board.BoardInstance.tilemap.WorldToCell(target);

                if (Count > 1)
                {
                    // Fire movement instruction event
                    OnPieceSelectedEvent?.Invoke();
                    
                    // Re-evaluate turn status post-action
                    CanMove = Utility.SwitchTurn(GameManager.Instance.Turn, gameObject);
                }
            }
        }
    }
}