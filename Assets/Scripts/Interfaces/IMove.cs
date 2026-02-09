using System.Collections.Generic;
using Assets.Scripts.Classes.PieceComponent;
using UnityEngine;


namespace Assets.Scripts.Interfaces
{
    public interface IMove
    {
        public void MovePiece(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2 targetPos);
    }
}