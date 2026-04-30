using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes.PieceComponent
{
    /// <inheritdoc/>
    /// <summary>
    /// Specialized movement controller for the King entity, facilitating complex multi-piece maneuvers.
    /// </summary>
    /// <remarks>
    /// This class overrides standard movement behavior to implement Castling logic. 
    /// It orchestrates the simultaneous coordinate transformation of both the King 
    /// and the corresponding Rook based on specific grid-target triggers.
    /// </remarks>
    public sealed class KingMovementComponent : PieceMovementComponent
    {
        /// <summary>
        /// Flag tracking the eligibility of the King for castling maneuvers.
        /// </summary>
        /// <remarks>
        /// TODO: Implement a state-check function to verify if the King or Rook 
        /// have previously performed a move, as per standard FIDE rules.
        /// </remarks>
        private bool _canCastle;


        /// <summary>
        /// Initializes the component, registers the King in the global piece tracker, 
        /// and sets initial castling rights.
        /// </summary>
        private void Start()
        {
            _canCastle =
                true; //TODO:improve this condition with a function that scans dictionary and see if the Rook has moved 
            CurrPos = Board.BoardInstance.tilemap.WorldToCell(transform.position);
            GameManager.Instance.Pieces ??= new();
            GameManager.Instance.Pieces?.Add((Vector2Int)CurrPos, this);
            SelectionComponent = GetComponent<PieceSelectionComponent>();
        }

        /// <inheritdoc />
        public override MoveType MovePiece(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int targetPos)
        {
            base.MovePiece(pieces, targetPos);
            if (!_canCastle) return MoveType.None;
            var position = transform.position;
            var newPosition = new Vector3Int(targetPos.x, targetPos.y, 0);
            var worldCellCenter =
                Board.BoardInstance.tilemap.GetCellCenterWorld(new Vector3Int(targetPos.x, targetPos.y, 0));
            pieces.TryGetValue(targetPos, out var occupied);

            if (occupied is null)
            {
                if (targetPos.Equals(Board.BoardInstance.WhiteKingShortCastlePosition))
                {
                    var rightWhiteRook = pieces[Board.BoardInstance.WhiteRightRook];
                    rightWhiteRook?.MovePiece(pieces, Board.BoardInstance.WhiteRightRookAfterShortCastlePosition);
                    transform.position = Vector2.MoveTowards(position, worldCellCenter, 10);
                    SelectionComponent.OnDeselect();
                    //  Count = 1;
                    UpdatePosition(pieces, targetPos, newPosition);
                    return MoveType.ShortCastle;
                }

                if (targetPos.Equals(Board.BoardInstance.WhiteKingLongCastlePosition))
                {
                    var leftWhiteRook = pieces[Board.BoardInstance.WhiteLeftRook];
                    leftWhiteRook.MovePiece(pieces, Board.BoardInstance.WhiteLeftRookAfterLongCastlePosition);
                    transform.position = Vector2.MoveTowards(position, worldCellCenter, 10);
                    SelectionComponent.OnDeselect();
                    UpdatePosition(pieces, targetPos, newPosition);
                    return MoveType.LongCastle;
                }

                if (targetPos.Equals(Board.BoardInstance.BlackKingShortCastlePosition))
                {
                    transform.position = Vector2.MoveTowards(position, worldCellCenter, 10);
                    var rightWhiteRook = pieces[Board.BoardInstance.BlackRightRook];
                    rightWhiteRook.MovePiece(pieces, Board.BoardInstance.BlackRightRookAfterShortCastlePosition);
                    SelectionComponent.OnDeselect();
                    UpdatePosition(pieces, targetPos, newPosition);
                    return MoveType.ShortCastle;
                }

                if (targetPos.Equals(Board.BoardInstance.BlackKingLongCastlePosition))
                {
                    transform.position = Vector2.MoveTowards(position, worldCellCenter, 10);
                    var leftWhiteRook = pieces[Board.BoardInstance.BlackLeftRook];
                    leftWhiteRook.MovePiece(pieces, Board.BoardInstance.BlackLeftRookAfterLongCastlePosition);
                    SelectionComponent.OnDeselect();
                    UpdatePosition(pieces, targetPos, newPosition);
                    return MoveType.LongCastle;
                }
            }

            return 0;
        }
        /// <summary>
        /// Updates the King's logical position in the piece registry after a physical move has already occurred.
        /// The transform position is updated by the caller before this method is invoked —
        /// this method synchronizes the Dictionary to reflect that change.
        /// </summary>
        /// <param name="pieces">The authoritative registry mapping board positions to piece components.</param>
        /// <param name="targetPos">The destination cell the King has moved to.</param>
        /// <param name="newPosition">The Vector3Int equivalent of targetPos, used to update the cached current position.</param>
        private void UpdatePosition(
            Dictionary<Vector2Int, PieceMovementComponent> pieces,
            Vector2Int targetPos,
            Vector3Int newPosition)
        {
            if (!newPosition.Equals(CurrPos))
            {
                pieces.Remove((Vector2Int)CurrPos);
                CurrPos = newPosition;
                pieces[targetPos] = this;
                _canCastle = false;
            }
        }
    }
}