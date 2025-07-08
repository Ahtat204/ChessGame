namespace Assets.Scripts.Enums
{
    /// <summary>
    /// Represents the two sides in a chess game.
    /// </summary>
    public enum GameState:uint
    {
        WaitingForPlayer,
        Check,
        Checkmate,
        Stalemate,
        Draw
    }
}