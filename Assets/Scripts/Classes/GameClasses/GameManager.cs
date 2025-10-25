using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Classes.BehaviorClasses;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Classes.GameClasses
{
    public class GameManager : MonoBehaviour
    {
        public List<GameObject> blackPieces;
        public List<GameObject> whitePieces;
        private GameState _gameState;
        private MoveType _moveType;
        private Turn _turn;
        private Coordinates _coordinates;
        private Scene _scene;
        private  GameObject[] _sceneObjects;


        private void Start()
        {
            _turn = Turn.WhitePlayer;
            blackPieces = new(16);
            whitePieces = new(16);
            _scene = SceneManager.GetActiveScene();
            _sceneObjects = _scene.GetRootGameObjects();
            for(var i = 0; i < _sceneObjects.Length; i++)
            {
                var obj = _sceneObjects[i];
                if (obj.name.StartsWith("b")) blackPieces.Add(obj);
                else if (obj.name.StartsWith("w")) whitePieces.Add(obj);
            }
            Array.Clear(_sceneObjects, 0, _sceneObjects.Length);
            _sceneObjects = null;
        }

        private void Update()
        {
            if(blackPieces is null || whitePieces is null) throw new NullReferenceException();


            if (_turn is Turn.BlackPlayer)
            {
                for (var i = 0; i < whitePieces.Count; i++)
                {
                    var movementmanager= whitePieces[i].GetComponent<MovementManager>();
                    if (movementmanager is null) return;
                    if (movementmanager.HasMoved) SetTurn(_turn);
                }
            }

            if (_turn is Turn.WhitePlayer)
            {
                for (var i = 0; i < blackPieces.Count; i++)
                {
                    var movementmanager= blackPieces[i].GetComponent<MovementManager>();
                    if (movementmanager is null) continue;
                    if (movementmanager.HasMoved) SetTurn(_turn);
                }
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

        void SetTurn(Turn turn)
        {
            TogglePieceScripts(whitePieces, turn == Turn.WhitePlayer);
            TogglePieceScripts(blackPieces, turn == Turn.BlackPlayer);
        }

        void TogglePieceScripts(List<GameObject> pieces, bool enable)
        {
            foreach(var p in pieces)
            {
                p.GetComponent<MovementManager>().enabled = enable;
            }
        }
    }
}