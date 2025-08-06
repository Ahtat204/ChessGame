using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Enums;
using Unity.VisualScripting;
using UnityEngine;


namespace Assets.Scripts.Classes.Pieces
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Queen : Piece
    {
        private GameObject _selectedObject;
        private Rigidbody2D _rigidbody;
        public override List<Vector2Int> PossibleMoves { get; protected set; }
        [SerializeField] private PieceColor pieceColor;
        public override uint Value => 9;

        public override PieceColor Color => pieceColor;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var MousePos = CameraMain.ScreenToWorldPoint(Input.mousePosition);
                DebugLog("mouse button down",MousePos);
            }
           
        }

        protected override void Move(Vector3Int to)
        {
            if (!PossibleMoves.Contains((Vector2Int)to)) return;

            var worldPosition = Tilemap.GetCellCenterWorld(to);
            _rigidbody.MovePosition(worldPosition);
        }

        /// <summary>
        /// function will return all the legal moves the queen can do 
        /// </summary>
        /// <param name="position">this is will be the transform.position of the GameObject, The Queen</param>
        /// <returns>return a List of Vector2Int of all the possible moves</returns>
        protected override List<Vector2Int> CalculateLegalMoves(Vector3 position)
        {
            var legalMoves = new List<Vector2Int>();
            var positionCell = (Vector2Int)Tilemap.WorldToCell(position);
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
            var filteredMovesList =
                legalMoves.Where(pos => pos is { x: >= 1 and <= 8, y: >= 1 and <= 8 }).ToList();
            return filteredMovesList;
        }

        public override void Awake()
        {
            base.Awake();
            if (CameraMain == null || Tilemap == null) throw new NullReferenceException();
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}