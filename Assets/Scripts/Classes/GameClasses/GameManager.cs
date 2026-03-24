using System;
using Assets.Scripts.Enums;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Interfaces;


namespace Assets.Scripts.Classes.GameClasses
{
    public class GameManager : MonoBehaviour
    {
        private GameState _gameState;
        private MoveType _moveType;
        public static event Action OnPieceSelected;
        public static GameManager Instance { get; private set; }
        public Dictionary<Vector2Int, PieceMovementComponent> Pieces;
        private PlayerTurn Turn { get; set; }
        private void Awake()
        {
            if (Instance is not null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            Pieces ??= new(32);
            Turn = PlayerTurn.WhitePlayer;
            OnPieceSelected?.Invoke();
        }
        private void Update()
        {
            if (Turn == PlayerTurn.WhitePlayer)
            {
                
            }
            if (_gameState is GameState.Check && Turn is PlayerTurn.WhitePlayer)
            {
                
            }

            if (_gameState is GameState.Check && Turn is PlayerTurn.BlackPlayer)
            {
            }

            if (_moveType == MoveType.Castling)
            {
            }
        }
    }
}