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
    public class Pawn : Piece
    {
        public override List<Vector2Int> PossibleMoves=> CalculateLegalMoves(transform.position);
        public override uint Value => 1; 
        [field: SerializeField]
        public override PieceColor Color { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private int Sign(PieceColor color)=> color == PieceColor.White ? 1 : -1;
        protected sealed override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            var legalMoves= new List<Vector2Int>(5);
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);
            legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y+(1 * Sign(Color))));
            legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y+ (2 * Sign(Color))));//only in first move
            legalMoves.Add(new Vector2Int(positionCell.x+1, positionCell.y+ (1 * Sign(Color))));
            legalMoves.Add(new Vector2Int(positionCell.x-1, positionCell.y+ (1 * Sign(Color))));
            legalMoves.Add(new Vector2Int(positionCell.x-1, positionCell.y+ (1 * Sign(Color))));//en Passant,handled by Proxy classes
            legalMoves.Remove(positionCell);
            var filteredMovesList = legalMoves.Where(pos => pos is { x: >= 1 and <= 8, y: >= 1 and <= 8 }).ToList();
            return filteredMovesList;
        }

        private void Start()
        {
            Debug.Log(Sign(Color));
        }
    }
}