namespace Assets.Scripts.Enums
{
    /// <summary>
    /// an Enum representing the Move type , whether it's capturing , Normal move , Check ...
    /// </summary>
    public enum MoveType : uint
    {
        Normal,
        Capture,
        Castling,
        EnPassant,
        Promotion
    }

}