using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Scripts.Structs;

namespace Assets.Scripts.Classes
{
    public class Board : MonoBehaviour
    {
        public  Tilemap tilemap; // Assign in Inspector
        private Coordinates Position;
        public Vector3 mousePosWorld;
        public Vector3Int mouseCell;
        public Vector3 mouseCellInterpolated;


        private void Awake()
        {
            

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
                Position = GetCoordinates();
                Debug.Log("Mouse clicked at: " + Position.ToString());
            }
        }

        public virtual Coordinates GetCoordinates()
        {
            System.Diagnostics.Debug.Assert(Camera.main != null, "Camera.main != null");
            var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            var cell = tilemap.WorldToCell(mouseWorldPos);
            return new Coordinates(cell.x, cell.y);
        }
    }
}