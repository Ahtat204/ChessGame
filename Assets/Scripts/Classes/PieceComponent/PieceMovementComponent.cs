using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.GameClasses.Validators;
using Assets.Scripts.Classes.Pieces;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.PieceComponent
{
    /// <summary>
    /// Manages movement and capture logic for a chess piece.
    ///  updates piece position, and resolves captures.
    /// </summary>
    /// <!--this class is working correcly-->
    [
        RequireComponent(typeof(Piece)),
        RequireComponent(typeof(BoxCollider2D)),
        RequireComponent(typeof(SpriteRenderer)),
        RequireComponent(typeof(PieceSelectionComponent))
    ]
    public class PieceMovementComponent : MonoBehaviour, IMove
    {
        #region fields&props

        public Piece piece { get; private set; }
        protected PieceSelectionComponent SelectionComponent;
        private bool CanMove { get; set; }
        public Vector3Int CurrPos { get;protected set; }

        #endregion

        #region methods

        private void Awake()
        {
            piece = GetComponent<Piece>();
            CanMove = true;
        }

        private void Start()
        {
            CurrPos = Board.BoardInstance.tilemap.WorldToCell(transform.position);
            GameManager.Instance.Pieces ??= new();
            GameManager.Instance.Pieces?.Add((Vector2Int)CurrPos, this);
            SelectionComponent = GetComponent<PieceSelectionComponent>();
        }

        /// <inheritdoc />
        public virtual MoveType MovePiece(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int targetPos)
        {
            var position = transform.position;
            CurrPos = Board.BoardInstance.tilemap.WorldToCell(position);
            piece.CalculateLegalMoves(position);
            if (!CanMove) return 0;
            Vector3Int pos = new Vector3Int(targetPos.x, targetPos.y, 0);
            var targetCell = targetPos;
            var worldCellCenter = Board.BoardInstance.tilemap.GetCellCenterWorld(pos);
            if (!piece.PossibleMoves.Contains(targetCell)) return 0;
            var occupied = pieces.ContainsKey(targetCell) ? pieces[targetCell] : null;
            if (occupied is null)
            {
                transform.position = Vector2.MoveTowards(transform.position, worldCellCenter, 10);

                SelectionComponent.OnDeselect();
                if (!pos.Equals(CurrPos))
                {
                    pieces.Remove((Vector2Int)CurrPos);
                    CurrPos = pos;
                    pieces[targetCell] = this;
                }

                return MoveType.Normal;
            }

            if (occupied.piece.Color == piece.Color) return 0;
            if (occupied.piece.Color != piece.Color)
            {
                if (occupied.piece is King) return 0;
                transform.position = Vector2.MoveTowards(targetPos, worldCellCenter, 10);

                SelectionComponent.OnDeselect();
                pieces.Remove(targetCell);
                pieces.Add(targetCell, this);
                Destroy(occupied.gameObject);
                if (!pos.Equals(CurrPos))
                {
                    pieces.Remove((Vector2Int)CurrPos);
                    CurrPos = pos;
                    pieces[targetCell] = this;
                }

                return MoveType.Capture;
            }

            return 0;
        }

        #endregion
    }
}