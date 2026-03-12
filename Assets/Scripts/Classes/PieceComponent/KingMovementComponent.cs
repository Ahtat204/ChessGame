using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;

using UnityEngine;

namespace Assets.Scripts.Classes.PieceComponent
{
    public class KingMovementComponent:PieceMovementComponent
    {
        private bool _canCastle;
        private Vector3Int CurrentPosition { get;  set; }
        
        private void Start()
        {
            _canCastle = true; //TODO:improve this condition with a function that scans dictionary and see if the Rook has moved 
            CurrentPosition = Board.BoardInstance.tilemap.WorldToCell(transform.position);
            GameManager.Instance.Pieces ??= new();
            GameManager.Instance.Pieces?.Add((Vector2Int)CurrentPosition, this);
        }
        public override void MovePiece(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2 targetPos)
        {
            base.MovePiece(pieces, targetPos);
            // TODO:add path checking
            if(Count>0 || !_canCastle) return;
            var position= transform.position;
            var gtar = Board.BoardInstance.tilemap.WorldToCell(targetPos);
            var gridTarget=(Vector2Int)gtar;
            var worldCellCenter = Board.BoardInstance.tilemap.GetCellCenterWorld(new Vector3Int(gridTarget.x,gridTarget.y,0));
            pieces.TryGetValue(gridTarget, out var occupied);
            if (occupied is null)
            {
                if (gridTarget.Equals(new Vector2Int(7, 1)))
                {
                    var rightWhiteRook = pieces[new Vector2Int(8, 1)];
                    rightWhiteRook?.MovePiece(pieces,Board.BoardInstance.tilemap.GetCellCenterWorld(new Vector3Int(6, 1)) );
                    transform.position = Vector2.MoveTowards(position, worldCellCenter, 10);
                    Count = 1;
                    _canCastle = false;
                    if (!gtar.Equals(CurrentPosition))
                    {
                        pieces.Remove((Vector2Int)CurrentPosition);
                        CurrentPosition = gtar;
                        pieces[(Vector2Int)gtar] = this;
                    }
                    return;
                    
                }

                if (gridTarget.Equals(new Vector2Int(3, 1)))
                {
                    var leftWhiteRook = pieces[new Vector2Int(1, 1)];
                    leftWhiteRook.MovePiece(pieces, Board.BoardInstance.tilemap.GetCellCenterWorld(new Vector3Int(4, 1)));
                    transform.position = Vector2.MoveTowards(position, worldCellCenter, 10);
                    _canCastle = false;
                    if (!gtar.Equals(CurrentPosition))
                    {
                        pieces.Remove((Vector2Int)CurrentPosition);
                        CurrentPosition = gtar;
                        pieces[(Vector2Int)gtar] = this;
                    }
                    return;
                    
                }
                if (gridTarget.Equals(new Vector2Int(7, 8)))
                {
                    transform.position = Vector2.MoveTowards(position, worldCellCenter, 10);
                    var rightWhiteRook = pieces[new Vector2Int(8, 8)];
                    rightWhiteRook.MovePiece(pieces, Board.BoardInstance.tilemap.GetCellCenterWorld(new Vector3Int(6, 8)));
                    _canCastle = false;
                    if (!gtar.Equals(CurrentPosition))
                    {
                        pieces.Remove((Vector2Int)CurrentPosition);
                        CurrentPosition = gtar;
                        pieces[(Vector2Int)gtar] = this;
                    }
                    return;
                }

                if (gridTarget.Equals(new Vector2Int(3, 8)))
                {
                    transform.position = Vector2.MoveTowards(position, worldCellCenter, 10);
                    var leftWhiteRook = pieces[new Vector2Int(1, 8)];
                    leftWhiteRook.MovePiece(pieces, Board.BoardInstance.tilemap.GetCellCenterWorld(new Vector3Int(4, 8)));
                    _canCastle = false;
                    if (!gtar.Equals(CurrentPosition))
                    {
                        pieces.Remove((Vector2Int)CurrentPosition);
                        CurrentPosition = gtar;
                        pieces[(Vector2Int)gtar] = this;
                    }
                    return;
                }
            }
        }
        
    }
}