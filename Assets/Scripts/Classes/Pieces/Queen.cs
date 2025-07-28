using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector2 = System.Numerics.Vector2;

namespace Assets.Scripts.Classes.Pieces
{
    public class Queen : Piece
    {
        public Tilemap tilemap;
        private uint valeur;
        private List<Vector2Int> moves;
        [SerializeField] private PieceColor _pieceColor;

        public override uint Value
        {
            get => 9;
            protected set => valeur = value;
        }

        public override PieceColor Color => _pieceColor;


        protected override void Move(Coordinates p)
        {
        }
        protected override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            var count = 0;
            var legalMoves = new List<Vector2Int>();
            var positionCell = (Vector2Int)tilemap.WorldToCell(position);
            for (var i = 1; i <= 8; i++)
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
            var filtredMovesList =
                legalMoves.Where(pos => pos.x >= 1 && pos.y >= 1 && pos.x <= 8 && pos.y <= 8).ToList();


            return filtredMovesList;
        }

        private void Start()
        {
            moves = CalculateLegalMoves(transform.position);
            foreach (var move in moves)
            {
                Debug.Log(move);
            }
        }
    }
}