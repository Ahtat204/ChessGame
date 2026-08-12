using System.Runtime.CompilerServices;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.GameClasses.Validators;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;
using static Assets.Scripts.Classes.Utility;

namespace Assets.Scripts.Classes.PieceComponent
{
    /// <summary>
    ///     Handles the selection state and input orchestration for individual Chess pieces.
    /// </summary>
    /// <remarks>
    ///     Acts as a bridge between the Unity Input System and the <see cref="CommandManager" />.
    ///     Implements a "Single Selection" pattern via a static reference to ensure
    ///     only one piece is active globally.
    /// </remarks>
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Piece))]
    [RequireComponent(typeof(PieceMovementComponent))]
    [RequireComponent(typeof(CommandManager))]
    public class PieceSelectionComponent : MonoBehaviour, ISelectable
    {
        /// <summary>
        ///     Global reference to the currently active selection.
        ///     Ensures mutual exclusivity of piece selection across the board.
        /// </summary>
        public static PieceSelectionComponent SelectedPiece;

        /// <summary>
        ///     Delegate for broadcast notifications when a valid movement target is finalized.
        /// </summary>
        public byte canMove;

        [SerializeField] private Vector2Int target;
        private Piece _piece;

        private Vector2 _target;

        private Vector3Int CurrentPosition { get; set; }

        private void Start()
        {
            _piece = GetComponent<Piece>();
            Status = SelectionStatus.UnSelected;
            canMove = Mapper(_piece.Color, GameManager.Instance.turn);
        }

        /// <summary>
        ///     Orchestrates the 'Selection -> Target' input sequence.
        /// </summary>
        private void Update()
        {
            if (!Input.GetMouseButtonDown(0) || Status != SelectionStatus.Selected) return;
            _target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            target = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(_target);
            if (target.x == CurrentPosition.x && target.y == CurrentPosition.y) return;
            {
                Target = target;
                var checkPath = GameManager.Instance.Pieces.ValidatePath((Vector2Int)CurrentPosition, Target);
                if (!checkPath) return;
                OnPieceSelectedEvent?.Invoke();
            }
        }

        /// <summary>
        ///     Unity Callback: Detects clicks directly on the piece collider to toggle selection.
        /// </summary>
        private void OnMouseDown()
        {
            canMove = Mapper(_piece.Color, GameManager.Instance.turn);
            if (canMove == 0) return;
            if (Status == SelectionStatus.Selected) OnDeselect();
            else OnSelect();
        }

        /// <inheritdoc cref="ISelectable.Status" />
        public SelectionStatus Status { get; set; }

        /// <inheritdoc />
        public Vector2Int Target
        {
            get => target;
            set => target = value;
        }

        /// <inheritdoc />
        public void OnSelect()
        {
            //  if (!CanMove) return;
            CurrentPosition = Board.BoardInstance.tilemap.WorldToCell(transform.position);
            // Enforce Single Selection Policy
            if (SelectedPiece is not null && SelectedPiece != this) SelectedPiece.OnDeselect();

            SelectedPiece = this;
            Status = SelectionStatus.Selected;
        }

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnDeselect()
        {
            if (SelectedPiece == this) SelectedPiece = null;
            Status = SelectionStatus.UnSelected;
        }

        /// <summary>
        ///     Global event triggered when a piece selection lifecycle completes a movement instruction.
        /// </summary>
        public static event OnPieceSelected OnPieceSelectedEvent;
    }
}