using System;
using Assets.Scripts.Enums;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Interfaces;


namespace Assets.Scripts.Classes.GameClasses
{
    public sealed class GameManager : MonoBehaviour
    {
        private GameState _gameState;
        private MoveType _moveType;
        public Stack<ICommand> CommandStack;
        public event Action OnSwitchTurn;
        public static GameManager Instance { get; private set; }
        public Dictionary<Vector2Int, PieceMovementComponent> Pieces;
        public PlayerTurn Turn;
        private void Awake()
        {
            CommandStack = new(30);
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
            OnSwitchTurn?.Invoke();
        }
        private void Update()
        {
            if (Turn == PlayerTurn.WhitePlayer)
            {
               
            }

            if (Turn == PlayerTurn.BlackPlayer)
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