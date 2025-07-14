using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    /// <summary>
    /// Base class of all Chess pieces
    /// </summary>
    public abstract class Piece : MonoBehaviour
    {
        protected abstract uint Value { get; }
        protected PieceColor Color;
    }
}