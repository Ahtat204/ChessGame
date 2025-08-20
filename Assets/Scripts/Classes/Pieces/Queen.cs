using System.Collections.Generic;
using System;
using System.Linq;
using Assets.Scripts.Classes.BehaviorClasses;
using Assets.Scripts.Classes.GameClasses;
using ChessGame.Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Queen : Piece
    {
        private SelectableDecorator _decorator;
        private GameObject _selectedObject;
        private Rigidbody2D _rigidbody;
        public override List<Vector2Int> PossibleMoves => CalculateLegalMoves(transform.position);
        [SerializeField] private PieceColor pieceColor;
        public override uint Value => 9;
        public override PieceColor Color => pieceColor;


        /// <summary>
        /// function will return all the legal moves the queen can do 
        /// </summary>
        /// <param name="position">this is will be the transform.position of the GameObject, The Queen</param>
        /// <returns>return a List of Vector2Int of all the possible moves</returns>
        protected override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            var legalMoves = new List<Vector2Int>();
            var positionCell = (Vector2Int)Tilemap.WorldToCell(position);
            for (var i = 1; i <= Board.Size; i++)
            {
                legalMoves.Add(new Vector2Int(positionCell.x + i, positionCell.y));
                legalMoves.Add(new Vector2Int(positionCell.x - i, positionCell.y));
                legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y + i));
                legalMoves.Add(new Vector2Int(positionCell.x, positionCell.y - i));
                legalMoves.Add(new Vector2Int(positionCell.x + i, positionCell.y + i));
                legalMoves.Add(new Vector2Int(positionCell.x + i, positionCell.y - i));
                legalMoves.Add(new Vector2Int(positionCell.x - i, positionCell.y + i));
                legalMoves.Add(new Vector2Int(positionCell.x - i, positionCell.y - i));
            }

            legalMoves.Remove(positionCell);
            var filteredMovesList =
                legalMoves.Where(pos => pos is { x: >= 1 and <= 8, y: >= 1 and <= 8 }).ToList();
            return filteredMovesList;
        }
        
        public override void Awake()
        {
            base.Awake();
            if (CameraMain == null || Tilemap == null) throw new NullReferenceException();
            _rigidbody = GetComponent<Rigidbody2D>();
            _decorator = GetComponent<SelectableDecorator>();
        }
    }
}