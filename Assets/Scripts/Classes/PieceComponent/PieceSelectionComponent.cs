using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.PieceComponent
{
    //: this class is working correctly
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Piece))]
    public class PieceSelectionComponent : MonoBehaviour, ISelectable
    {
        public SelectionStatus Status { get; set; }
        public int Count { private set; get; }
        public delegate void onPieceSelected();
        public static event onPieceSelected OnPieceSelected;
        public Vector2 Target => target;
        private static readonly List<PieceSelectionComponent> MovableObjects = new();
        public Vector2 target;
        public float maxDelay = 1.0f;
        private void Start()
        {
            MovableObjects.Add(this);
            Status = SelectionStatus.UnSelected;
        }
        public void OnSelect()
        {
            Status = SelectionStatus.Selected;
            foreach (var obj in MovableObjects.Where(obj => obj != this))
            {
                obj.Status = SelectionStatus.UnSelected;
            }

            Count = 1;
            OnPieceSelected?.Invoke();
        }

        public void OnDeselect()
        {
            Status = SelectionStatus.UnSelected;
            Count = 0;
        }

        private void OnMouseDown()
        {
            if (Status == SelectionStatus.Selected) OnDeselect();
            else OnSelect();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) // Left mouse button
            {
                StartCoroutine(WaitForSecondClick());
            }
        }

        private IEnumerator WaitForSecondClick()
        {
            Debug.Log("First click detected. Waiting for second click...");

            float timer = 0f;
            bool secondClick = false;

            // Wait until second click or timeout
            while (timer < maxDelay)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    secondClick = true;
                    break;
                }

                timer += Time.deltaTime;
                yield return null; // Wait for next frame
            }

            if (secondClick)
            {
                Debug.Log("Second click detected!");
                target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                Debug.Log("Second click not detected in time.");
            }
        }
    }
}