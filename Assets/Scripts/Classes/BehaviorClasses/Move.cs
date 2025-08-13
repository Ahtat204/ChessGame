using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Classes.GameClasses;
using UnityEngine.Tilemaps;


namespace Assets.Scripts.Classes.BehaviorClasses
{
    [RequireComponent(typeof(Piece))]
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Move : MonoBehaviour
    {
        private SelectableDecorator _selectableDecorator;
        private Piece _piece;
        private Rigidbody2D _rigidbody;
        private Tilemap _tilMap;
        private  List<Vector2Int> _possibleMoves;
        
        

        private void Awake()
        {
            _selectableDecorator = GetComponent<SelectableDecorator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _tilMap =Board.BoardInstance.Tilemap;
            _possibleMoves=_piece.PossibleMoves;
        }
        public void MovePiece(Vector3Int to)
        {
            if (!_possibleMoves.Contains((Vector2Int)to)) return;

            var worldPosition = _tilMap.GetCellCenterWorld(to);
            _rigidbody.MovePosition(worldPosition);
        }
    }
}