using System;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.Pieces;
using UnityEngine;


namespace Assets.Scripts.Classes.BehaviorClasses
{
    /// <summary>
    /// as the name suggests , this class is only responsible for allowing king to castle
    /// </summary>
    public class KingCastling : MonoBehaviour
    {
        private King _king;
        public bool _canCastle;
        private Vector3Int _castlePosition;
        private Vector3 _initialPosition;

        private void Start()
        {
            _initialPosition = transform.position;
            _castlePosition = Board.BoardInstance.tilemap.WorldToCell(_initialPosition);
            _king = GetComponentInParent<King>();
        }

        private void Update()
        {
            if (_castlePosition!= Board.BoardInstance.tilemap.WorldToCell(transform.position))
            {
                _canCastle = false;
            }
        }

        

        private void OnMouseDrag()
        {
            if(!_canCastle) return;
            
        }
        
    }
}