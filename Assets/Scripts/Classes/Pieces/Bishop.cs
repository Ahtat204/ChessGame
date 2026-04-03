using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    /// <summary>
    /// Bishop class
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PieceMovementComponent))]
    [RequireComponent(typeof(PieceSelectionComponent))]
    public sealed class Bishop : Piece
    {
        /// <inheritdoc/>
        public override uint Value => 3;
        private readonly List<Vector2Int> _possibleMoves = new(14);
        /// <inheritdoc/>
        public override IReadOnlyList<Vector2Int> PossibleMoves => _possibleMoves;
        /// <inheritdoc/>
        [field: SerializeField] public override PieceColor Color { get; protected set; }
        /// <inheritdoc/>
        public override void CalculateLegalMoves(Vector3 position)
        {
            _possibleMoves.Clear();
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);
            for (var i = 1; i <= Board.Size; i++)
            {
                _possibleMoves.AddIfValid(positionCell.x + i, positionCell.y + i);
                _possibleMoves.AddIfValid(positionCell.x + i, positionCell.y - i);
                _possibleMoves.AddIfValid(positionCell.x - i, positionCell.y + i);
                _possibleMoves.AddIfValid(positionCell.x - i, positionCell.y - i);
            }
        }
    }
}