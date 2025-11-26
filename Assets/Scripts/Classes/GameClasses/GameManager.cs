using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Classes.BehaviorClasses;


namespace Assets.Scripts.Classes.GameClasses
{
    public class GameManager : MonoBehaviour
    {
        public List<MovementManager> blackPieces;
        public List<MovementManager> whitePieces;
        private GameState _gameState;
        private MoveType _moveType;
        public Turn turn { get; set; }
        private Coordinates _coordinates;
        // private Scene _scene;


        private void Start()
        {
            turn = Turn.WhitePlayer;
            blackPieces = new(16);
            whitePieces = new(16);
            if (blackPieces is null || whitePieces is null) throw new NullReferenceException();
        }

        public void RegisterPiece(MovementManager gObject)
        {
            var piece = gObject?._piece;
            if (piece is null) return;
            if (gObject._piece.Color == PieceColor.Black) blackPieces.Add(gObject);
            else whitePieces.Add(gObject);
            gObject.OnMoveCompleted += HandleMoveCompleted;
        }

        private void Update()
        {
            if (_gameState is GameState.Check && turn is Turn.WhitePlayer)
            {
            }

            if (_gameState is GameState.Check && turn is Turn.BlackPlayer)
            {
            }

            if (_moveType == MoveType.Castling)
            {
            }
        }


        void HandleMoveCompleted(MovementManager m)
        {
            SwitchTurn(m._piece.Color);
        }


        void TogglePieceScripts(List<MovementManager> pieces, bool enable)
        {
            if (pieces is null) return;
            foreach (var p in pieces)
            {
                p.enabled = !enable;
            }
        }

        void SwitchTurn(PieceColor color)
        {
            /*if(turn == Turn.WhitePlayer) turn = Turn.BlackPlayer;
            else if(turn == Turn.BlackPlayer) turn = Turn.WhitePlayer;*/

            if (color == PieceColor.Black)
            {
                turn = Turn.BlackPlayer;
            }
            else if (color == PieceColor.White)
            {
                turn = Turn.WhitePlayer;
            }

            TogglePieceScripts(blackPieces, color == PieceColor.Black);
            TogglePieceScripts(whitePieces, color == PieceColor.White);
        }
    }
}