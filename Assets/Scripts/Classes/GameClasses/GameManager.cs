using Assets.Scripts.Enums;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Classes.BehaviorClasses;

namespace Assets.Scripts.Classes.GameClasses
{
    public class GameManager : MonoBehaviour
    {
        private GameState _gameState;
        private MoveType _moveType;
        public static GameManager Instance { get; private set; }
        public Dictionary<Vector2Int, MovementManager> _pieces;
        public delegate void OnPieceMoved();
        public event OnPieceMoved OnPieceMovedEvent;
        private Turn _turn { get; set; }


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _pieces ??= new();
            _turn = Turn.WhitePlayer;
        }


        private void Update()
        {
           
            if (_gameState is GameState.Check && _turn is Turn.WhitePlayer)
            {
            }

            if (_gameState is GameState.Check && _turn is Turn.BlackPlayer)
            {
            }

            if (_moveType == MoveType.Castling)
            {
            }
        }
    }
}