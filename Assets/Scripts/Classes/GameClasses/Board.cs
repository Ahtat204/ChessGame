using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Classes.GameClasses
{
    /// <summary>
    /// this class will be used to create a singleton instance of the TileMap and the camera to avoid multiple unnecessary instances
    /// </summary>
    public class Board : MonoBehaviour
    {
        public static readonly uint Size = 8;
        /// <summary>
        /// a tile map field
        /// </summary>
        [Space] [SerializeField] private Tilemap tilemap; // Assign in Inspector
        /// <summary>
        /// to avoid overusing Camera.main , better centralize it <remarks>assign in the inspector</remarks> 
        /// </summary>
        [Space] [SerializeField] private Camera cam;

        public Tilemap Tilemap => tilemap;

        /// <summary>
        /// to avoid creating camera inside the Update method with Camera.main , which is expensive in terms of resources , we create it once 
        /// </summary>
        public Camera MainCamera => cam;

        public static Board BoardInstance;

        private void Awake()
        {
            // If there is an instance, and it's not me, delete myself.

            if (BoardInstance is null && BoardInstance != this)
            {
                Destroy(this);
                return;
            }
            else
            {
                BoardInstance = this;
                DontDestroyOnLoad(this);
            }
        }
    }
}