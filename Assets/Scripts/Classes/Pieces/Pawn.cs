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
        public override List<Vector2Int> PossibleMoves { get; }= new List<Vector2Int>(5);
        public override uint Value => 1;
        [field: SerializeField] public override PieceColor Color { get; protected set; }
        private int Sign(PieceColor color) => color == PieceColor.White ? 1 : -1;

        public  override void CalculateLegalMoves(Vector3 position)
        {
            PossibleMoves.Clear();
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);
            PossibleMoves.AddIfValid(positionCell.x, positionCell.y + (1 * Sign(Color)));
            PossibleMoves.AddIfValid(positionCell.x, positionCell.y + (2 * Sign(Color))); //only in first move
            PossibleMoves.AddIfValid(positionCell.x + 1, positionCell.y + (1 * Sign(Color)));
            PossibleMoves.AddIfValid(positionCell.x - 1, positionCell.y + (1 * Sign(Color)));
            PossibleMoves.AddIfValid(positionCell.x - 1, positionCell.y + (1 * Sign(Color))); //en Passant,handled by Proxy classes
        }
    }
}