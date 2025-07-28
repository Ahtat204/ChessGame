#define UNITY_EDITOR
#define UNITY_ANDROID

using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Scripts.Structs;

namespace Assets.Scripts.Classes
{
    public class Board : MonoBehaviour
    {
        /// <summary>
        /// a tile map field
        /// </summary>
        [Space] [SerializeField] private Tilemap tilemap; // Assign in Inspector

        private Coordinates _position;

        /// <summary>
        /// a tile map field
        /// </summary>
        [Space] [SerializeField] private Vector3 mousePosWorld;

        /// <summary>
        /// a tile map field
        /// </summary>
        [Space] [SerializeField] private Vector3Int mouseCell;

        /// <summary>
        /// a tile map field
        /// </summary>
        [Space] [SerializeField] private Vector3 mouseCellInterpolated;

        /// <summary>
        /// to avoid creating camera inside the Update method with Camera.main , which is expensive in terms of resources , we create it once 
        /// </summary>
        private Camera _camera;


        private void Start()
        {
            _camera = Camera.main;
        }


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                /*
                Debug.Log("Mouse clicked");
                // Convert screen to world
                mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosWorld.z = 0f; // Important in 2D

                // Get cell position
                mouseCell = tilemap.WorldToCell(mousePosWorld);
                mouseCellInterpolated = tilemap.LocalToCellInterpolated(tilemap.transform.InverseTransformPoint(mousePosWorld));

                   Debug.Log("Cell: " + mouseCell);
                  Debug.Log("Interpolated: " + mouseCellInterpolated);
                  */
                _position = GetCoordinates();
                
            }
        }

        public Coordinates GetCoordinates()
        {
            
            System.Diagnostics.Debug.Assert(_camera, "Camera.main != null");
            var mouseWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            var cell = tilemap.WorldToCell(mouseWorldPos);
            return new Coordinates(cell.x, cell.y);
        }
    }
}