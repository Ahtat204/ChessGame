using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Classes.GameClasses
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        public Vector3 MousDirection { get; private set; }
        public Vector3Int MousePosition { get; private set; }

        private void Awake()
        {
            Instance = this;
            MousDirection = new Vector3();
        } 

        private void Update()
        {
            MousDirection = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            MousePosition = Board.BoardInstance.Tilemap.WorldToCell(MousePosition);
        }
    }
}