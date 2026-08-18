using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Classes.Pieces;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    /// <summary>
    ///     A set of utility methods to simplify chess piece movement validation
    ///     and board-related operations. These helpers keep instance methods cleaner.
    /// </summary>
    public static partial class Utility
    {
        /// <summary>
        ///     Delegate invoked when a piece is selected.
        /// </summary>
        public delegate void OnPieceSelected();

        /// <summary>
        ///     Adds a position to the list if the coordinates are valid (within 1–8).
        /// </summary>
        /// <param name="pieces">The list of positions.</param>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddIfValid(this List<Vector2Int> pieces, int x, int y)
        {
            if (x is >= 1 and <= 8 && y is >= 1 and <= 8) pieces.Add(new Vector2Int(x, y));
        }

        /// <summary>
        ///     Maps a piece color and player turn to an integer value.
        ///     Returns 1 if the piece color matches the current turn, otherwise 0.
        /// </summary>
        /// <param name="color">The color of the piece.</param>
        /// <param name="turn">The current player's turn.</param>
        /// <returns>1 if valid, otherwise 0.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Mapper(PieceColor color, PlayerTurn turn)
        {
            if (turn == PlayerTurn.BlackPlayer && color == PieceColor.Black) return 1;
            if (turn == PlayerTurn.WhitePlayer && color == PieceColor.White) return 1;
            return 0;
        }




    }
}