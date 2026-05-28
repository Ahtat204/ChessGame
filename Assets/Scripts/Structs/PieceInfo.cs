using System;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Structs
{
    public struct PieceInfo
    {
        public PieceColor Color;
        public byte MaterialValue;
        public Vector2Int Position;

        public PieceInfo(Vector2Int position, PieceColor color, byte materialValue)
        {
            Color = color;
            MaterialValue = materialValue;
            Position = position;
        }
    }
}