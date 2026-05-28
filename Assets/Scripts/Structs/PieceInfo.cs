using System;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Structs
{
    public struct PieceInfo
    {
        public PieceColor Color;
        public ushort MaterialValue;
        public Vector2Int Position;

        public PieceInfo(PieceColor color, ushort materialValue, Vector2Int position)
        {
            Color = color;
            MaterialValue = materialValue;
            Position = position;
        }
    }
}