using System;
using System.Collections.Generic;
using Assets.Scripts.Classes.PieceComponent;
using UnityEngine;

namespace Assets.Scripts.Classes.GameClasses.Validators
{
    public static class PawnValidator
    {
        public static bool ValidateCapturing(this Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end)
        {
            var piece = pieces.GetValueOrDefault(end);
            
            return piece is not null;
        }
    }
}