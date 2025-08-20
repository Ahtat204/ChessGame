using System;

namespace ChessGame.Assets.Scripts.Enums
{
    /// <summary>
    /// Represents the two sides in a chess game.
    /// </summary>
    public enum GameState : UInt16
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
    public enum Turn : UInt16
    {
        Player,
        Opponent
    }

}