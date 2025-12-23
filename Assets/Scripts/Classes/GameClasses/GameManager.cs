using Assets.Scripts.Enums;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Classes.BehaviorClasses;
using Assets.Scripts.Interfaces;


namespace Assets.Scripts.Classes.GameClasses
{
    public class GameManager : MonoBehaviour
    {
        private GameState _gameState;
        private MoveType _moveType;
        public static GameManager Instance { get; private set; }
        public Dictionary<Vector2Int, MovementManager> Pieces;
        private readonly Stack<ICommand> _moveCommands;
        public delegate void OnMovePiece();
        public event OnMovePiece OnExecute;
        public PlayerTurn Turn { get; set; }
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
        }

        void ToggleScripts()
        {
            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnExecute?.Invoke();
                foreach (var manager in Instance.Pieces)
                {
                    manager.Value.CanMove = !manager.Value.CanMove;
                }
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