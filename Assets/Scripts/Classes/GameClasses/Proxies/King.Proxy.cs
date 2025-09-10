using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Classes.Pieces;


namespace Assets.Scripts.Classes.GameClasses.Proxies
{
    public class KingProxy : MonoBehaviour
    {
        private King _king;
        private Piece[] _enemyPieces;
        private HashSet<Vector2Int> _threatnedpositions;
        private void Awake()
        {
            _king = GetComponent<King>();
            _enemyPieces = FindObjectsOfType<Piece>()
                .Where(p => p.Color != _king.Color).ToArray();
            _threatnedpositions = new(8);
        }
        public void RestrictMoving()
        {
            _threatnedpositions.Clear();
            foreach (var piece  in _enemyPieces )
            {
                foreach (var move in piece.PossibleMoves)
                {
                    _threatnedpositions.Add(move);
                }  
            } 
            _king.PossibleMoves.RemoveAll(move => _threatnedpositions.Contains(move));
        }
    }
}