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
        private Piece _piece;
        protected PieceSelectionComponent SelectionComponent;
        private bool CanMove { get; set; }
        private Vector3Int CurrPos { get;set; }
        #endregion
        #region methods
        private void Awake()
        {
            _piece = GetComponent<Piece>();
            CanMove = _piece.Color != PieceColor.Black;
            CanMove = true;
        }
        private void Start()
        {
            CurrPos = Board.BoardInstance.tilemap.WorldToCell(transform.position);
            GameManager.Instance.Pieces ??= new();
            GameManager.Instance.Pieces?.Add((Vector2Int)CurrPos, this);
            SelectionComponent=GetComponent<PieceSelectionComponent>();
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
                SelectionComponent.OnDeselect();
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
                SelectionComponent.OnDeselect();
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