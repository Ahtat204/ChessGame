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

        private Piece piece { get; set; }
        protected PieceSelectionComponent SelectionComponent;
        private bool CanMove { get; set; }
        private Vector3Int CurrPos { get; set; }

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
            piece.CalculateLegalMoves(transform.position);
            if (!CanMove) return 0;
            Vector3Int pos=new Vector3Int(targetPos.x,targetPos.y,0);
            var targetCell = targetPos;
            bool checkPath = PieceMovementValidator.CheckPath(pieces, (Vector2Int)CurrPos, (Vector2Int)targetCell);
            if (!checkPath && piece is not Knight) return 0;
            var worldCellCenter = Board.BoardInstance.tilemap.GetCellCenterWorld(pos);
            if (!piece.PossibleMoves.Contains((Vector2Int)targetCell)) return 0;
            var occupied = pieces.ContainsKey((Vector2Int)targetCell) ? pieces[(Vector2Int)targetCell] : null;
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
                pieces.Remove((Vector2Int)targetCell);
                pieces.Add((Vector2Int)targetCell, this);
                Destroy(occupied.gameObject);
                if (!pos.Equals(CurrPos))
                {
                    pieces.Remove((Vector2Int)CurrPos);
                    CurrPos = pos;
                    pieces[(Vector2Int)targetCell] = this;
                }
                return MoveType.Capture;
            }
            return 0;
        }

        #endregion
    }
}