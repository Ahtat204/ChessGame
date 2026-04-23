using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Classes.GameClasses
{
    /// <summary>
    /// Acts as the Central Authority and Spatial Provider for the chess simulation.
    /// </summary>
    /// <remarks>
    /// Implements a Singleton pattern to provide global, high-performance access to 
    /// shared engine resources like the Tilemap and the Main Camera. 
    /// This prevents redundant expensive lookups (e.g., Camera.main) across the entity stack.
    /// </remarks>
    public sealed class Board : MonoBehaviour
    {
        public readonly Vector2Int BlackRightRook=new (8, 8);
        public readonly Vector2Int WhiteRightRook=new (8, 1);
        public readonly Vector2Int BlackLeftRook=new (1, 8);
        public readonly Vector2Int WhiteLeftRook=new (1, 1);
        public readonly Vector2Int BlackKingShortCastlePosition=new (7, 8);
        public readonly Vector2Int WhiteKingShortCastlePosition=new (7, 1);
        public readonly Vector2Int BlackKingLongCastlePosition=new(3, 8);
        public readonly Vector2Int WhiteKingLongCastlePosition=new(3, 1);
        public readonly Vector2Int WhiteRightRookAfterShortCastlePosition=new(6,1);
        public readonly Vector2Int WhiteLeftRookAfterLongCastlePosition=new(5,1);
        public readonly Vector2Int BlackRightRookAfterShortCastlePosition=new(6, 8);
        public readonly Vector2Int BlackLeftRookAfterLongCastlePosition=new (4, 8);
        /// <summary>
        /// The fixed dimension of the chess grid (8x8).
        /// </summary>
        public const uint Size = 8;

        /// <summary>
        /// The primary grid system used for world-to-cell coordinate quantization.
        /// </summary>
        /// <value>Assigned via the Unity Inspector.</value>
        [field: SerializeField] 
        public Tilemap tilemap { get; private set; }

        /// <summary>
        /// Cached reference to the primary rendering camera.
        /// </summary>
        /// <remarks>
        /// Centralizing this reference bypasses the overhead associated with the 
        /// <c>Camera.main</c> property, which performs a tag-based search.
        /// </remarks>
        [field: SerializeField] 
        public Camera MainCamera { get; private set; }

        /// <summary>
        /// Global access point for the Board singleton.
        /// </summary>
        public static Board BoardInstance { get; private set; }

        /// <summary>
        /// Establishes the singleton instance on component initialization.
        /// </summary>
        private void Awake()
        {
            if (BoardInstance != null && BoardInstance != this)
            {
                Destroy(gameObject);
                return;
            }
            BoardInstance = this;
        }
    }
}