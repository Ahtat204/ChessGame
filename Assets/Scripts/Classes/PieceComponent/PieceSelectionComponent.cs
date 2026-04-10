using System.Linq;
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

        private Piece _piece;
        public bool CanMove;

        private Vector3Int clickedCell;
        public delegate void OnPieceSelected();

        public static event OnPieceSelected OnPieceSelectedEvent;
        public Vector2 Target => target;
        public Vector2 target;

        private void Start()
        {
            clickedCell = Board.BoardInstance.tilemap.WorldToCell(target);
            Status = SelectionStatus.UnSelected;
            _piece = GetComponent<Piece>();
            CanMove= Utility.SwitchTurn(GameManager.Instance.Turn,gameObject);
        }

        public void OnSelect()
        {
            if (!CanMove) return;
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
                clickedCell = Board.BoardInstance.tilemap.WorldToCell(target);
                if (Count > 1 )
                {
                    OnPieceSelectedEvent?.Invoke();
                    CanMove = Utility.SwitchTurn(GameManager.Instance.Turn, gameObject);
                }
            }
        }
    }
}