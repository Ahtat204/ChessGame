using System.Collections.Generic;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.Features.KingCastling
{
    public class CastlingMovementComponent:IMove
    {
        public void MovePiece(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2 targetPos)
        {
            throw new System.NotImplementedException();
        }
    }
}