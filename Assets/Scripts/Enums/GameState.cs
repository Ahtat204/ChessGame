using System;

namespace Assets.Scripts.Enums
{
    /// <summary>
    /// Represents the current state of the Game
    /// </summary>
    public enum GameState : byte
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
    public enum PlayerTurn : byte
    {
        WhitePlayer = 0,
        BlackPlayer = 1
    }

}