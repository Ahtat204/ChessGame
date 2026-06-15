using System;
using Assets.Scripts.Enums;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Structs;
using UnityEngine.Serialization;
using static Assets.Scripts.Utility;

namespace Assets.Scripts.Classes.GameClasses
{
    public sealed class GameManager : MonoBehaviour
    {
        private GameState _gameState;
        private MoveType _moveType;
        public Stack<Vector2Int> CommandStack;
        public static GameManager Instance { get; private set; }
        public Dictionary<Vector2Int, PieceMovementComponent> Pieces;
        [FormerlySerializedAs("Turn")] public PlayerTurn turn;

        private void Awake()
        {
            turn = PlayerTurn.WhitePlayer;
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
        public void OnDisable() => PieceSelectionComponent.OnPieceSelectedEvent -= SwitchPlayerTurn;

        private void Start()
        {
            Pieces ??= new(32);
        }
        private void SwitchPlayerTurn()
        {
            byte attackers = 0;
            turn = turn == PlayerTurn.WhitePlayer ? PlayerTurn.BlackPlayer : PlayerTurn.WhitePlayer;
            Span<PieceInfo> pieces = stackalloc PieceInfo[Pieces.Count];
            Pieces.ToSpan(pieces);
            PieceInfo targetKing = new PieceInfo();
            if (turn == PlayerTurn.BlackPlayer)
            {
                for (byte i = 0; i < pieces.Length; i++)
                {
                    if (pieces[i].Color == PieceColor.Black && pieces[i].MaterialValue == 0)
                    {
                        targetKing = pieces[i];
                    }
                }
            }
            else if (turn == PlayerTurn.WhitePlayer)
            {
                for (byte i = 0; i < pieces.Length; i++)
                {
                    if (pieces[i].Color == PieceColor.White && pieces[i].MaterialValue == 0)
                    {
                        targetKing = pieces[i];
                    }
                }
            }
            //check if knight is attacking the king
            for (byte i = 0; i < pieces.Length; i++)
            {
                var piece = pieces[i];
                if (piece.Color != targetKing.Color)
                {
                    if (piece.MaterialValue == 1)
                    {
                    
                    }
                    if(piece.MaterialValue==3)//making sure that a pawn is not considered a knight
                    {
                        attackers += IsAttackedByKnights(targetKing.Position, piece.Position);
                    } 
                }
               
            }

        }

    }
}