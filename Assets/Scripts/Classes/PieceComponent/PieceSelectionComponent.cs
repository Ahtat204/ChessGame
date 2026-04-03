using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.PieceComponent
{
    [
        RequireComponent(typeof(BoxCollider2D)),
        RequireComponent(typeof(Piece)),
        RequireComponent(typeof(PieceMovementComponent)),
        RequireComponent(typeof(CommandManager)),
    ]
    public class PieceSelectionComponent : MonoBehaviour, ISelectable
    {
        private static PieceSelectionComponent _selectedPiece;
        public SelectionStatus Status { get; set; }
        private int Count { set; get; }

        public delegate void OnPieceSelected();

        public static event OnPieceSelected OnPieceSelectedEvent;
        public Vector2 Target => target;
        public Vector2 target;

        private void Start() => Status = SelectionStatus.UnSelected;

        public void OnSelect()
        {
            if (_selectedPiece is not null && _selectedPiece != this)
            {
                _selectedPiece.OnDeselect();
            }

            Status = SelectionStatus.Selected;
            Count = 1;
        }

        public void OnDeselect()
        {
            if (_selectedPiece == this) _selectedPiece = null;
            Status = SelectionStatus.UnSelected;
            Count = 0;
        }

        private void OnMouseDown()
        {
            if (Status == SelectionStatus.Selected) OnDeselect();
            else OnSelect();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && Status == SelectionStatus.Selected)
            {
                Count++;
                target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                if (Count > 1)
                {
                    OnPieceSelectedEvent?.Invoke();
                }
            }
        }
    }
}