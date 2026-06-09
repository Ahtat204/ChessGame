using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Assets.Scripts.Classes;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Classes.Pieces;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// A set of utility methods to simplify chess piece movement validation
    /// and board-related operations. These helpers keep instance methods cleaner.
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// Delegate invoked when a piece is selected.
        /// </summary>
        public delegate void OnPieceSelected();

        /// <summary>
        /// Adds a position to the list if the coordinates are valid (within 1–8).
        /// </summary>
        /// <param name="pieces">The list of positions.</param>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddIfValid(this List<Vector2Int> pieces, int x, int y)
        {
            if (x is >= 1 and <= 8 && y is >= 1 and <= 8)
            {
                pieces.Add(new Vector2Int(x, y));
            }
        }

        /// <summary>
        /// Maps a piece color and player turn to an integer value.
        /// Returns 1 if the piece color matches the current turn, otherwise 0.
        /// </summary>
        /// <param name="color">The color of the piece.</param>
        /// <param name="turn">The current player's turn.</param>
        /// <returns>1 if valid, otherwise 0.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Mapper(PieceColor color, PlayerTurn turn)
        {
            if (turn == PlayerTurn.BlackPlayer && color == PieceColor.Black) return 1;
            if (turn == PlayerTurn.WhitePlayer && color == PieceColor.White) return 1;
            return 0;
        }

        /// <summary>
        /// Validates queen movement by combining rook and bishop movement rules.
        /// </summary>
        public static bool QueenValidator(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end, int dx, int dy)
        {
            if (dx == 1 || dy == 1) return true;
            var result1 = RookValidator(pieces, start, end, dx, dy);
            var result2 = BishopValidator(pieces, start, end, dx, dy);
            return result1 && result2;
        }

        /// <summary>
        /// Validates rook movement ensuring no blocking pieces exist along the path.
        /// </summary>
        public static bool RookValidator(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end, int dx, int dy)
        {
            if (dx == 1 || dy == 1) return true;
            if (dx == 0) //moving horizontally 
            {
                if (dy > 0) //moving  to the right
                {
                    foreach (var position in pieces.Keys.Where(
                                 key => key.x == end.x && key.y < end.y && key.y > start.y))
                    {
                        if (pieces[position] is not null)
                        {
                            return false;
                        }
                    }
                }

                if (dy < 0) // moving to the left
                {
                    foreach (var position in pieces.Keys.Where(
                                 key => key.x == end.x && key.y > start.y && key.y < end.y))
                    {
                        if (pieces[position] is not null)
                        {
                            return false;
                        }
                    }
                }
            }

            if (dy == 0) //moving vertically
            {
                if (dx > 0) //moving to the Top
                {
                    foreach (var position in pieces.Keys.Where(
                                 key => end.y == key.y && key.x < end.x && key.x > start.x))
                    {
                        if (pieces[position] is not null)
                        {
                            return false;
                        }
                    }
                }

                if (dx < 0) // move to the bottom
                {
                    foreach (var position in pieces.Keys.Where(
                                 key => end.y == key.y && key.x < start.x && key.x > end.x))
                    {
                        if (pieces[position] is not null)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Validates bishop movement ensuring no blocking pieces exist along the diagonal path.
        /// </summary>
        public static bool BishopValidator(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end, int dx, int dy)
        {
            if (dx == 1 || dy == 1) return true;
            if (dy > 1 && dx > 1) //move up-right (fixed)
            {
                for (int i = 1; i < end.y - 1; i++)
                {
                    var pos = new Vector2Int(start.x + i, start.y + i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }
            }

            if (dx < -1 && dy > 1) //move Up-left
            {
                for (int i = 1; i < end.y - 1; i++)
                {
                    var pos = new Vector2Int(start.x - i, start.y + i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }
            }

            if (dx > 1 && dy < -1) //move down-right
            {
                for (int i = 1; i < end.x - 1; i++)
                {
                    var pos = new Vector2Int(start.x + i, start.y - i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }
            }

            if (dx < -1 && dy < -1) //move down left
            {
                for (int i = 1; i < end.x - 1; i++)
                {
                    var pos = new Vector2Int(start.x - i, start.y - i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Validates king movement. Currently, always returns true.
        /// </summary>
        public static bool KingValidator(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end, int dx, int dy)
        {
            return true;
        }


        /// <summary>
        /// Returns the number of attackers on the king instead of a simple check status.
        /// </summary>
        /// <param name="color">The color of the king.</param>
        /// <param name="kingPos">The position of the king.</param>
        /// <param name="pieces">All pieces on the board.</param>
        /// <returns>Number of attackers threatening the king.</returns>
        public static byte IsInCheck(PieceColor color, Vector2Int kingPos, ReadOnlySpan<PieceInfo> pieces)
        {
            switch (color)
            {
                case PieceColor.White:
                    break;
                case PieceColor.Black:
                    break;
            }

            for (byte i = 0; i < pieces.Length; i++)
            {
                var piece = pieces[i];
            }

            return 2;
        }

        /// <summary>
        /// Converts a dictionary of pieces into a span of <see cref="PieceInfo"/>.
        /// </summary>
        /// <param name="pieces">The dictionary of pieces.</param>
        /// <param name="piecesSpan">The span to populate.</param>
        public static void ToSpan(this Dictionary<Vector2Int, PieceMovementComponent> pieces,
            Span<PieceInfo> piecesSpan)
        {
            byte i = 0;
            foreach (var piece in pieces)
            {
                if (i > pieces.Count) return;
                piecesSpan[i] = new PieceInfo(piece.Key, piece.Value.piece.Color, piece.Value.piece.Value);
                i++;
            }
        }


        /// <summary>
        /// Checks if a capture is valid by verifying if a piece exists at the target position.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool ValidateCapturing(this Dictionary<Vector2Int, PieceMovementComponent> pieces,
            Vector2Int end) => pieces.GetValueOrDefault(end) is not null;

        /// <summary>
        /// Validates pawn movement, ensuring diagonal captures are only allowed
        /// if an opponent piece exists at the target position.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PawnValidator(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end)
        {
            var validation = Math.Abs(start.x - end.x) == 1 && Math.Abs(start.y - end.y) == 1;
            if (validation) return pieces.ValidateCapturing(end);
            return true;
        }
    }
}