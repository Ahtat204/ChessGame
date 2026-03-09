using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;

using UnityEngine;

namespace Assets.Scripts.Classes.PieceComponent
{
    public class KingMovementComponent:PieceMovementComponent
    {
        private bool _canCastle; 
        private void Start()
        {
            _canCastle = true;
        }
        public override void MovePiece(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2 targetPos)
        {
            base.MovePiece(pieces, targetPos);
            // TODO:add path checking
            //if(!_canCastle) return;
            var position= transform.position;
            var gridTarget=(Vector2Int)Board.BoardInstance.tilemap.WorldToCell(targetPos);
            var worldCellCenter = Board.BoardInstance.tilemap.GetCellCenterWorld(new Vector3Int(gridTarget.x,gridTarget.y,0));
            var occupied = pieces.ContainsKey(gridTarget) ? pieces[gridTarget] : null;
            if (occupied is null)
            {
                if (gridTarget.Equals(new Vector2Int(7, 1)))
                {
                    var rightWhiteRook = pieces[new Vector2Int(8, 1)];
                    transform.position = Vector2.MoveTowards(targetPos, worldCellCenter, 10);
                    rightWhiteRook.MovePiece(pieces, new Vector2Int(6, 1));
                    _canCastle = false;
                    return;
                }

                if (gridTarget.Equals(new Vector2Int(3, 1)))
                {
                    var leftWhiteRook = pieces[new Vector2Int(1, 1)];
                    leftWhiteRook.MovePiece(pieces, new Vector2Int(4, 1));
                    transform.position = Vector2.MoveTowards(position, gridTarget, 10);
                    _canCastle = false;
                    return;
                }
                if (gridTarget.Equals(new Vector2Int(7, 8)))
                {
                    transform.position = Vector2.MoveTowards(position, gridTarget, 10);
                    var rightWhiteRook = pieces[new Vector2Int(8, 8)];
                    rightWhiteRook.MovePiece(pieces, new Vector2Int(6, 8));
                    _canCastle = false;
                    return;
                }

                if (gridTarget.Equals(new Vector2Int(3, 8)))
                {
                    transform.position = Vector2.MoveTowards(position, gridTarget, 10);
                    var leftWhiteRook = pieces[new Vector2Int(1, 8)];
                    leftWhiteRook.MovePiece(pieces, new Vector2Int(4, 8));
                    _canCastle = false;
                    return;
                }
            }
        }
        
    }
}