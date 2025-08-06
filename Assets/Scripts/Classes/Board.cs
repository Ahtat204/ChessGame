#define UNITY_EDITOR
#define UNITY_ANDROID

using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Scripts.Structs;

namespace Assets.Scripts.Classes
{
    /// <summary>
    /// this class will be used to create a singleton instance of the TileMap and the camera to avoid multiple unnecessary instances
    /// </summary>
    public class Board : MonoBehaviour
    {
        /// <summary>
        /// a tile map field
        /// </summary>
        [Space] [SerializeField] private Tilemap tilemap; // Assign in Inspector

        [Space] [SerializeField] private Camera cam;
        private Piece selectedPiece;
        public Tilemap Tilemap
        {
            get => tilemap;
            private set => tilemap = value;
        }
        /// <summary>
        /// to avoid creating camera inside the Update method with Camera.main , which is expensive in terms of resources , we create it once 
        /// </summary>
        public Camera MainCamera
        {
            get => cam;
            private set => cam = value;
        }

        public static Board BoardInstance { get; private set; }

        public void SelectPiece(Piece piece)
        {
            selectedPiece = piece;
          
        }
        private void Awake()
        {
            if (BoardInstance != null && BoardInstance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                BoardInstance = this;
            }
        }
       
    }
}