using System;
using Assets.Scripts.Enums;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Classes.PieceComponent;



namespace Assets.Scripts.Classes.GameClasses
{
    public sealed class GameManager : MonoBehaviour
    {
        private GameState _gameState;
        private MoveType _moveType;
        public Stack<Vector2Int> CommandStack;
        public event Action OnSwitchTurn;
        public static GameManager Instance { get; private set; }
        public Dictionary<Vector2Int, PieceMovementComponent> Pieces;
        public PlayerTurn Turn;

        private void Awake()
        {
            Turn = PlayerTurn.WhitePlayer;
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
        public void OnEnable() => PieceSelectionComponent.OnPieceSelectedEvent += SwitchPlayerTurn;

        /// <summary>
        /// Unsubscribes from the global event to prevent memory leaks or null reference exceptions.
        /// </summary>
        public void OnDisable() => PieceSelectionComponent.OnPieceSelectedEvent -=SwitchPlayerTurn;

        private void Start()
        {
            Pieces ??= new(32);

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
            
        }

        private void SwitchPlayerTurn()
        {
            Turn = Turn == PlayerTurn.WhitePlayer ? PlayerTurn.BlackPlayer : PlayerTurn.WhitePlayer;
            Debug.Log($"this {Turn} turn and  {nameof(SwitchPlayerTurn)} executed");
        }
    }
}