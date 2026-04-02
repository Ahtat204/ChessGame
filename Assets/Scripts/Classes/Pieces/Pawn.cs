using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Classes.GameClasses;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    public sealed class Pawn : Piece
    {
        private readonly List<Vector2Int> _possibleMoves  = new (5);
        public override uint Value => 1;
        public override IReadOnlyList<Vector2Int> PossibleMoves => _possibleMoves;
        [field: SerializeField] public override PieceColor Color { get; protected set; }
        private int Sign(PieceColor color) => color == PieceColor.White ? 1 : -1;

        public  override void CalculateLegalMoves(Vector3 position)
        {
            _possibleMoves.Clear();
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);
            _possibleMoves.AddIfValid(positionCell.x, positionCell.y + (1 * Sign(Color)));
            _possibleMoves.AddIfValid(positionCell.x, positionCell.y + (2 * Sign(Color))); //only in first move
            _possibleMoves.AddIfValid(positionCell.x + 1, positionCell.y + (1 * Sign(Color)));
            _possibleMoves.AddIfValid(positionCell.x - 1, positionCell.y + (1 * Sign(Color)));
            _possibleMoves.AddIfValid(positionCell.x - 1, positionCell.y + (1 * Sign(Color))); //en Passant,handled by Proxy classes
        }
    }
}