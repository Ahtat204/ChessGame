using System;

namespace Assets.Scripts.Enums
{
    /// <summary>
    /// Represents the two sides in a chess game.
    /// </summary>
    public enum GameState : ushort
    {
        WaitingForPlayer,
        Check,
        Checkmate,
        Stalemate,
        Draw
    }
    /// <summary>
    /// enum representing who's turn is to play
    /// </summary>
    public enum Turn : ushort
    {
        WhitePlayer,
        BlackPlayer
    }

}