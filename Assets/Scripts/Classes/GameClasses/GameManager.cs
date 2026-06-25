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
                    //pawn check detection
                    if (piece.MaterialValue == 1) attackers += IsAttackedByPawns(targetKing.Position, piece.Position, piece.Color);
                    //knight check detection
                    if (piece.MaterialValue == 3) attackers += IsAttackedByKnights(targetKing.Position, piece.Position);
                    //rook check detection
                    if (piece.MaterialValue == 5)
                    {
                        if (piece.Position.x == targetKing.Position.x)
                        {
                            //check vertically
                            for (byte j = 0; j < Math.Abs(targetKing.Position.y - piece.Position.y); j++)
                            {
                                if (pieces[j].Color == targetKing.Color)
                                    break; // we found a piece that covers the rook's attack on the king, the piece is pinned
                            }
                        }

                        if (piece.Position.y == targetKing.Position.y)
                        {
                            //check horizontally
                            for (byte j = 0; j < Math.Abs(targetKing.Position.x - piece.Position.x); j++)
                            {
                                if (pieces[j].Color == targetKing.Color)
                                    break; // we found a piece that covers the rook's attack on the king, the piece is pinned
                            }
                        }
                    }
                    //bishop check detection
                    if (piece.MaterialValue == 4)
                    {
                        
                    }
                }
            }
        }
    }
}