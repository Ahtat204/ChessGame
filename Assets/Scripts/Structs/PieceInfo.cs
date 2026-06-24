using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Structs
{
    public struct PieceInfo
    {
        public readonly PieceColor Color;
        public readonly byte MaterialValue;
        public readonly Vector2Int Position;

        public PieceInfo(Vector2Int position, PieceColor color, byte materialValue)
        {
            Color = color;
            MaterialValue = materialValue;
            Position = position;
        }
    }
}