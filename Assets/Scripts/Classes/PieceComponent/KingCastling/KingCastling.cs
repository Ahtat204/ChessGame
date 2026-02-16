using System;
using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.Pieces;
using UnityEngine;


namespace Assets.Scripts.Classes.BehaviorClasses.KingCastling
{
    /// <summary>
    /// as the name suggests , this class is only responsible for allowing king to castle
    /// </summary>
    ///  <remarks>Castling is a special kind of move(justifies why should inherit from MovementManager) , it only happen once </remarks>
    // @TODO Rename this class to KingMovementComponent (After testing its functionality) and used it in the King instead of the MovementManager(this class is MovementComponent+Castling) better than Movement twice+Castling
    public class KingCastling : MovementManager
    {
        private King _king;
        private Rook _rook;
        public int canCastle; // King can only castle if he didn't move from (5,1) or (5,8)
        private Vector3Int _castlePosition;
        private Vector3 _initialPosition;

        private void Start()
        {
            canCastle = 0;
            _initialPosition = transform.position;
            _castlePosition = Board.BoardInstance.tilemap.WorldToCell(_initialPosition);
            _king = GetComponent<King>();
        }

        private void Update()
        {
            if (!_castlePosition.Equals(Board.BoardInstance.tilemap.WorldToCell(transform.position)))
            {
                canCastle = 1;
            }
        }

        protected override void MovePiece(Dictionary<Vector2Int, MovementManager> pieces)
        {
            base.MovePiece(pieces);
            if (canCastle == 0) // this checks if king hasn't moved yet 
            {
            }
        }
    }
}