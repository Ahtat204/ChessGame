using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    public class GameManager:MonoBehaviour
    {
        [SerializeField] protected GameState gameState;
        [SerializeField] protected MoveType moveType;
    }
}