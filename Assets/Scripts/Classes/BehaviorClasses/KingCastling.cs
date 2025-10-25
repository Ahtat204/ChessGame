using System;
using Assets.Scripts.Classes.GameClasses;
using UnityEngine;

namespace Assets.Scripts.Classes.BehaviorClasses
{
    /// <summary>
    /// as the name suggests , this class is only responsible for allowing king to castle
    /// </summary>
    public class KingCastling : MonoBehaviour
    {
        private bool _canCastle;
        private Vector3Int _castlePosition;

        private void Start()
        {
            _castlePosition = Board.BoardInstance.tilemap.WorldToCell(transform.position);
        }

        private void Update()
        {
            if (_castlePosition!= Board.BoardInstance.tilemap.WorldToCell(transform.position))
            {
                _canCastle = false;
            }
        }

        private void CastleKing()
        {
            if(_canCastle) return;
        }
    }
}