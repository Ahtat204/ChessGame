
namespace Assets.Scripts.Enums
{
    /// <summary>
    /// an Enum representing the Move type , whether it's capturing , Normal move , Check ...
    /// </summary>
    public enum MoveType : byte
    {
        None = 0,
        Normal= 1 ,
        Capture=2,
        Check=1<<3,
        LongCastle =1<<4,
        ShortCastle=1<<5,
        EnPassant=1<<6,
        Promotion=1<<7,
    }


}