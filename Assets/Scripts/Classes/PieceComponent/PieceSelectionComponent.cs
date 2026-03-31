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

        public delegate void onPieceSelected();
        
        private int count = 0;
        public static event onPieceSelected OnPieceSelected;
        public Vector2 Target => target;
        private static readonly List<PieceSelectionComponent> MovableObjects = new();
        public Vector2 target;
        int choice = -1; // valid choices for this example are 0 and 1
        /*void Start()
        {
            StartCoroutine(DoSomeLogic());
        }*/
        /*IEnumerator DoSomeLogic()
        {
            print("Starting..."); // print message to the console
             // wait for player to press '0' or '1'
            print("Continuing..."); // print message to the console
        }*/
        IEnumerator WaitForKeyDown()
        {
            while (!Input.GetMouseButtonDown(0))
            {
                yield return null; // yield return pauses the coroutine
            }

            target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
    
   

        private void Start()
        {
            MovableObjects.Add(this);
            Status = SelectionStatus.UnSelected;
        }

        public void OnSelect()
        {
            Status = SelectionStatus.Selected;
            Debug.Log($"{gameObject.name}: OnSelect");
            count=1;
            OnPieceSelected?.Invoke();
        }

        public void OnDeselect()
        {
            Status = SelectionStatus.UnSelected;
            count=0;
        }

        private void OnMouseDown()
        {
            if (Status == SelectionStatus.Selected) OnDeselect();
            else OnSelect();
            foreach (var obj in MovableObjects.Where(obj => obj != this))
            {
                obj.Status = SelectionStatus.UnSelected;
            }
        }

        private void Update()
        {
            if (Status == SelectionStatus.Selected)
            {
                {
                    StartCoroutine(WaitForKeyDown());
                    
                }
            }
        }
    }
}