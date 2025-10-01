using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Classes.GameClasses
{
    public class GameManager:MonoBehaviour
    {

        [SerializeField]private List<GameObject> pieces;
        private IEnumerable<GameObject> _blackPieces;
        private IEnumerable<GameObject> _whitePieces;
        private GameState _gameState;
        private MoveType _moveType;
        private Turn _turn;
        private Coordinates _coordinates;


        private void Start()
        {
            pieces = new (32);
            _gameState = GameState.WaitingForPlayer;
            _turn = Turn.WhitePlayer;
            _moveType=new MoveType();
            _blackPieces= from piece in pieces where ; ;
        }

        private void Update()
        {
            if (_turn == Turn.WhitePlayer)
            {
               
            }

            if (_turn == Turn.BlackPlayer)
            {
                
            }

            if (_gameState is GameState.Check && _turn == Turn.WhitePlayer)
            {
                
            }

            if (_gameState is GameState.Check && _turn == Turn.BlackPlayer)
            {
                
            }

            if (_moveType == MoveType.Castling)
            {
                
            }
        }
    }
}