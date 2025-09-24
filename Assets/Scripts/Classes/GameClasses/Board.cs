using System;
using System.Drawing;
using Assets.Scripts.Classes.Pieces;
using Unity.VisualScripting;
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
        [field:SerializeField] public Tilemap tilemap { get; private set; } // Assign in Inspector
        /// <summary>
        /// to avoid overusing Camera.main , better centralize it <remarks>assign in the inspector</remarks> 
        /// </summary>
        [field:SerializeField] public Camera MainCamera { get; private set; }

        public static Board BoardInstance;

        private void Awake()
        {
            BoardInstance = this;
        }
    }
}