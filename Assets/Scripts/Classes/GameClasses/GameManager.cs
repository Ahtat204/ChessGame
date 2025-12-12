using Assets.Scripts.Enums;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.BehaviorClasses;


namespace Assets.Scripts.Classes.GameClasses
{
    public class GameManager : MonoBehaviour
    {
        private GameState _gameState;
        private MoveType _moveType;
        public static GameManager Instance { get; private set; }
        public Dictionary<Vector2Int, MovementManager> _pieces;

        public delegate void onMovePiece();

        public event onMovePiece OnExecute;
        public Turn _turn { get; set; }

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
            _pieces ??= new();
            _turn = Turn.WhitePlayer;
           // OnExecute += switchTurns;
        }

        private void Update()
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                OnExecute?.Invoke();
            }
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