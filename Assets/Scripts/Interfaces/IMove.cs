using System.Collections.Generic;
using Assets.Scripts.Classes.BehaviorClasses;
using UnityEngine;


namespace Assets.Scripts.Interfaces
{
    public interface IMove
    {
        public void MovePiece(Dictionary<Vector2Int, MovementManager> pieces, Vector2 targetPos);
    }
}