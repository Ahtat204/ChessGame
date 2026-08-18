using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Assets.Scripts.Classes.PieceComponent;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    public static partial class Utility
    {
        /// <summary>
        ///     Checks if a capture is valid by verifying if a piece exists at the target position.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool ValidateCapturing(this Dictionary<Vector2Int, PieceMovementComponent> pieces,
            Vector2Int end)
        {
            return pieces.GetValueOrDefault(end) is not null;
        }

        /// <summary>
        ///     Validates pawn movement, ensuring diagonal captures are only allowed
        ///     if an opponent piece exists at the target position.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool PawnValidator(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end)
        {
            var validation = Math.Abs(start.x - end.x) == 1 && Math.Abs(start.y - end.y) == 1;
            if (validation) return pieces.ValidateCapturing(end);
            return true;
        }

        /// <summary>
        ///     Validates queen movement by combining rook and bishop movement rules.
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
        ///     Validates rook movement ensuring no blocking pieces exist along the path.
        /// </summary>
        public static bool RookValidator(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end, int dx, int dy)
        {
            if (dx == 1 || dy == 1) return true;
            if (dx == 0) //moving horizontally 
            {
                if (dy > 0) //moving  to the right
                    foreach (var position in pieces.Keys.Where(
                                 key => key.x == end.x && key.y < end.y && key.y > start.y))
                        if (pieces[position] is not null)
                            return false;

                if (dy < 0) // moving to the left
                    foreach (var position in pieces.Keys.Where(
                                 key => key.x == end.x && key.y > start.y && key.y < end.y))
                        if (pieces[position] is not null)
                            return false;
            }

            if (dy == 0) //moving vertically
            {
                if (dx > 0) //moving to the Top
                    foreach (var position in pieces.Keys.Where(
                                 key => end.y == key.y && key.x < end.x && key.x > start.x))
                        if (pieces[position] is not null)
                            return false;

                if (dx < 0) // move to the bottom
                    foreach (var position in pieces.Keys.Where(
                                 key => end.y == key.y && key.x < start.x && key.x > end.x))
                        if (pieces[position] is not null)
                            return false;
            }

            return true;
        }

        /// <summary>
        ///     Validates bishop movement ensuring no blocking pieces exist along the diagonal path.
        /// </summary>
        public static bool BishopValidator(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end, int dx, int dy)
        {
            if (dx == 1 || dy == 1) return true;
            if (dy > 1 && dx > 1) //move up-right (fixed)
                for (var i = 1; i < end.y - 1; i++)
                {
                    var pos = new Vector2Int(start.x + i, start.y + i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }

            if (dx < -1 && dy > 1) //move Up-left
                for (var i = 1; i < end.y - 1; i++)
                {
                    var pos = new Vector2Int(start.x - i, start.y + i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }

            if (dx > 1 && dy < -1) //move down-right
                for (var i = 1; i < end.x - 1; i++)
                {
                    var pos = new Vector2Int(start.x + i, start.y - i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }

            if (dx < -1 && dy < -1) //move down left
                for (var i = 1; i < end.x - 1; i++)
                {
                    var pos = new Vector2Int(start.x - i, start.y - i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }

            return true;
        }

        /// <summary>
        ///     Validates king movement. Currently, always returns true.
        /// </summary>
        public static bool KingValidator(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end, int dx, int dy)
        {
            return true;
        }
    }
}