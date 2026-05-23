using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.GameClasses;
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

        /// <summary>
        /// reference to the <see cref="Assets.Scripts.Classes.Piece"/>  attached to this <c>gameObject</c>
        /// </summary>
        public Piece piece { get; private set; }

        /// <summary>
        /// reference to the <see cref="Assets.Scripts.Classes.PieceComponent.PieceSelectionComponent"/> attached to this <c>gameObject</c>
        /// </summary>
        protected PieceSelectionComponent SelectionComponent;

        /// <summary>
        /// a boolean flag that act as a switch and permission to prevent/allow piece to move
        /// <remarks>this condition will combine many validations such as :if this piece is pinned to the King by another piece : <code>CanMove=false</code></remarks>
        /// </summary>
        private bool CanMove { get; set; }

        /// <summary>
        /// this field caches the position of the game object to avoid calling <c>tranform.position</c> multiple times
        /// </summary>
        public Vector3Int CurrPos { get; protected set; }

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
            var pos = new Vector3Int(targetPos.x, targetPos.y, 0);
            var worldCellCenter = Board.BoardInstance.tilemap.GetCellCenterWorld(pos);
            if (!piece.PossibleMoves.Contains(targetPos)) return 0;
            PieceMovementComponent occupied = pieces.GetValueOrDefault(targetPos);
            if (occupied is null) return MoveToEmptySquare(pieces, targetPos, worldCellCenter, pos, position);
            if (occupied.piece.Color == piece.Color) return MoveType.None;
            if (occupied.piece.Color != piece.Color)
            {

                if (occupied.piece is King) return 0;
                if (piece is Pawn)
                {

                }

                return CapturePiece(pieces, targetPos, worldCellCenter, pos, position, occupied);
            }

            return MoveType.None;
        }

        /// <summary>
        /// a helper method to manage NormalMove(no capture,just move a piece to an empty square) ,avoiding sacking everything in one method
        /// </summary>
        /// <param name="pieces">reference to the Hashtable representing a key-value pair of every piece with its current position </param>
        /// <param name="targetSquare">the precise <c>Vector3</c> position converted from the <c>Vector2Int targetSquare</c> parameter</param>
        /// <param name="targetPos">the grid square that the player clicked to move the piece to</param>
        /// <param name="pos"></param>
        /// <param name="currentPos">cached Current position to avoid calling <code>transform.position</code> again</param>
        /// <returns></returns>
        private MoveType MoveToEmptySquare(Dictionary<Vector2Int, PieceMovementComponent> pieces,
            Vector2Int targetSquare, Vector3 targetPos, Vector3Int pos, Vector3 currentPos)
        {
            transform.position = Vector2.MoveTowards(currentPos, targetPos, 10);
            GameManager.Instance.CommandStack.Push(targetSquare);
            SelectionComponent.OnDeselect();
            if (!pos.Equals(CurrPos))
            {
                pieces.Remove((Vector2Int)CurrPos);
                CurrPos = pos;
                pieces[targetSquare] = this;
            }

            return MoveType.Normal;
        }

        private MoveType CapturePiece(Dictionary<Vector2Int, PieceMovementComponent> pieces,
            Vector2Int targetSquare,
            Vector3 targetPos,
            Vector3Int pos,
            Vector3 currentPos,
            PieceMovementComponent occupied)
        {
            transform.position = Vector2.MoveTowards(currentPos, targetPos, 10);
            GameManager.Instance.CommandStack.Push(targetSquare);
            SelectionComponent.OnDeselect();
            pieces.Remove(targetSquare);
            pieces.Add(targetSquare, this);
            Destroy(occupied.gameObject);
            if (!pos.Equals(CurrPos))
            {
                pieces.Remove((Vector2Int)CurrPos);
                CurrPos = pos;
                pieces[targetSquare] = this;
            }

            return MoveType.Capture;
        }

        #endregion
    }
}