using System;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.Pieces;
using UnityEngine;


namespace Assets.Scripts.Classes.BehaviorClasses.KingCastling
{
    /// <summary>
    /// as the name suggests , this class is only responsible for allowing king to castle
    /// </summary>
    public class KingCastling : MonoBehaviour
    {
        private King _king;
        public bool canCastle;
        private Vector3Int _castlePosition;
        private Vector3 _initialPosition;

        private void Start()
        {
            canCastle = true;
            _initialPosition = transform.position;
            _castlePosition = Board.BoardInstance.tilemap.WorldToCell(_initialPosition);
            _king = GetComponent<King>();
        }

        private void Update()
        {
            if (!_castlePosition.Equals(Board.BoardInstance.tilemap.WorldToCell(transform.position)))
            {
                canCastle = false;
            }
        }
        
    }
}