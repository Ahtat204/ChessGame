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
    public class KingMovementComponent : PieceMovementComponent
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
        /// Cache of the King's current tilemap coordinates to manage dictionary updates.
        /// </summary>
        private Vector3Int CurrentPosition { get; set; }

        /// <summary>
        /// Initializes the component, registers the King in the global piece tracker, 
        /// and sets initial castling rights.
        /// </summary>
        private void Start()
        {
            _canCastle =
                true; //TODO:improve this condition with a function that scans dictionary and see if the Rook has moved 
            CurrentPosition = Board.BoardInstance.tilemap.WorldToCell(transform.position);
            GameManager.Instance.Pieces ??= new();
            GameManager.Instance.Pieces?.Add((Vector2Int)CurrentPosition, this);
            SelectionComponent = GetComponent<PieceSelectionComponent>();
        }

        /// <inheritdoc />
        public override MoveType MovePiece(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int targetPos)
        {
            base.MovePiece(pieces, targetPos);
            var position = transform.position;
            var threInt = new Vector3Int(targetPos.x, targetPos.y, 0);
            var worldCellCenter =
                Board.BoardInstance.tilemap.GetCellCenterWorld(new Vector3Int(targetPos.x, targetPos.y, 0));
            pieces.TryGetValue(targetPos, out var occupied);
            if (!_canCastle) return MoveType.None;
            if (occupied is null)
            {
                if (targetPos.Equals(Board.BoardInstance.WhiteKingShortCastlePosition))
                {
                    var rightWhiteRook = pieces[Board.BoardInstance.WhiteRightRook];
                    rightWhiteRook?.MovePiece(pieces, Board.BoardInstance.WhiteRightRookAfterShortCastlePosition);
                    transform.position = Vector2.MoveTowards(position, worldCellCenter, 10);
                    SelectionComponent.OnDeselect();
                    //  Count = 1;
                    _canCastle = false;
                    if (!threInt.Equals(CurrentPosition))
                    {
                        pieces.Remove((Vector2Int)CurrentPosition);
                        CurrentPosition = threInt;
                        pieces[targetPos] = this;
                        _canCastle = false;
                    }

                    return MoveType.ShortCastle;
                }

                if (targetPos.Equals(Board.BoardInstance.WhiteKingLongCastlePosition))
                {
                    var leftWhiteRook = pieces[Board.BoardInstance. WhiteLeftRook];
                    leftWhiteRook.MovePiece(pieces, Board.BoardInstance.WhiteLeftRookAfterLongCastlePosition);
                    transform.position = Vector2.MoveTowards(position, worldCellCenter, 10);
                    SelectionComponent.OnDeselect();
                    _canCastle = false;
                    if (!threInt.Equals(CurrentPosition))
                    {
                        pieces.Remove((Vector2Int)CurrentPosition);
                        CurrentPosition = threInt;
                        pieces[targetPos] = this;
                        _canCastle = false;
                    }

                    return MoveType.LongCastle;
                }

                if (targetPos.Equals(Board.BoardInstance.BlackKingShortCastlePosition))
                {
                    transform.position = Vector2.MoveTowards(position, worldCellCenter, 10);
                    var rightWhiteRook = pieces[Board.BoardInstance.BlackRightRook];
                    rightWhiteRook.MovePiece(pieces, Board.BoardInstance.BlackRightRookAfterShortCastlePosition);
                    SelectionComponent.OnDeselect();
                    _canCastle = false;
                    if (!threInt.Equals(CurrentPosition))
                    {
                        pieces.Remove((Vector2Int)CurrentPosition);
                        CurrentPosition = threInt;
                        pieces[targetPos] = this;
                        _canCastle = false;
                    }

                    return MoveType.ShortCastle;
                }

                if (targetPos.Equals(Board.BoardInstance.BlackKingLongCastlePosition))
                {
                    transform.position = Vector2.MoveTowards(position, worldCellCenter, 10);
                    var leftWhiteRook = pieces[Board.BoardInstance.BlackLeftRook];
                    leftWhiteRook.MovePiece(pieces,Board.BoardInstance.BlackLeftRookAfterLongCastlePosition );
                    SelectionComponent.OnDeselect();
                    _canCastle = false;
                    if (!threInt.Equals(CurrentPosition))
                    {
                        pieces.Remove((Vector2Int)CurrentPosition);
                        CurrentPosition = threInt;
                        pieces[targetPos] = this;
                        _canCastle = false;
                    }

                    return MoveType.LongCastle;
                }
            }

            return 0;
        }
    }
}