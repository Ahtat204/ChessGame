using System;
using System.Collections.Generic;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Classes.Pieces;
using UnityEngine;
using static Assets.Scripts.Utility;

namespace Assets.Scripts.Classes.GameClasses.Validators
{
    /// <summary>
    /// this is class is responsible for preventing a piece from overtake a friendly piece or take its place
    /// </summary>
    public static class PieceMovementValidator
    {
        /// <summary>
        /// Validate Piece Move and stop any penetration ,but it delegates the actual verification to a Utility function,in case of knight ,it always returns true , 
        /// </summary>
        /// <param name="pieces">this is the single source of truth of the positions of all the 32 pieces</param>
        /// <param name="start">the position of the piece</param>
        /// <param name="end">the position where the piece intends to move to</param>
        /// <remarks>we <c>return</c> instead of <c>break</c> to avoid entering another Loop or condition</remarks>
        public static bool ValidatePath(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end)
        {
            var piece = pieces[start].piece;
            int dx = Math.Abs(end.x -
                              start.x); //difference between int the current position's x and target position's x 
            int dy = Math.Abs(end.y -
                              start.y); //difference between int the current position's y and target position's y 
            if (dx == 1 || dy == 1) return true;
            return piece switch
            {
                Pawn => PawnValidator(pieces, start, end, dx, dy),
                Knight => true,
                Bishop => BishopValidator(pieces, start, end, dx, dy),
                Rook => RookValidator(pieces, start, end, dx, dy),
                Queen => QueenValidator(pieces, start, end, dx, dy),
                King => KingValidator(pieces, start, end, dx, dy),
                _ => true
            };
        }
    }
}