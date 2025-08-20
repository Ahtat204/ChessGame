using System;

namespace ChessGame.Assets.Scripts.Enums
{
    /// <summary>
    /// an Enum representing the Move type , whether it's capturing , Normal move , Check ...
    /// </summary>
    public enum MoveType : UInt16
    {
        Normal,
        Capture,
        Castling,
        EnPassant,
        Promotion
    }

}