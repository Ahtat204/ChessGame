using System;

namespace Assets.Scripts.Enums
{
    /// <summary>
    /// an Enum representing the Move type , whether it's capturing , Normal move , Check ...
    /// </summary>
    public enum MoveType : ushort
    {
        None = 0,
        Normal,
        Capture,
        Castling,
        EnPassant,
        Promotion
    }

    public enum CastlingType : ushort
    {
        LongCastle,
        ShortCastle,
    }
    

}