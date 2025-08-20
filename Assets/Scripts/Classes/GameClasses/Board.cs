#define UNITY_EDITOR
#define UNITY_ANDROID

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
        public static uint Size=8;
        /// <summary>
        /// a tile map field
        /// </summary>
        [Space] [SerializeField] private Tilemap tilemap; // Assign in Inspector

        [Space] [SerializeField] private Camera cam;
        private Piece _selectedPiece;
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
            _selectedPiece = piece;
          
        }
        private void Awake()
        {
            if (BoardInstance && !Equals(BoardInstance, this))
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                BoardInstance = this;
            }
        }
       
    }
}