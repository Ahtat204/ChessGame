using System;

namespace Assets.Scripts.Enums
{
    /// <summary>
    /// an Enum representing the Move type , whether it's capturing , Normal move , Check ...
    /// </summary>
    public enum MoveType : ushort
    {
        Normal,
        Capture,
        Castling,
        EnPassant,
        Promotion
    }

}