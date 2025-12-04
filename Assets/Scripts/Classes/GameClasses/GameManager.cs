using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.BehaviorClasses;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Classes.GameClasses
{
    public class GameManager : MonoBehaviour
    {
        private GameState _gameState;
        private MoveType _moveType;
        Dictionary<Vector2Int, MovementManager> _pieces;
        private Turn _turn { get; set; }
        private Coordinates _coordinates;
        // private Scene _scene;


        private void Start()
        {
            _pieces = new Dictionary<Vector2Int, MovementManager>();
            foreach (var piece in FindObjectsOfType<MovementManager>())
            {
                _pieces.Add((Vector2Int)piece.CurrPos, piece);
               // Debug.Log(piece.CurrPos);
            }

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