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
    [RequireComponent(typeof(PieceMovementComponent))]
    
    internal class PieceSelectionComponent : MonoBehaviour, ISelectable
    {
        //UInt32 for consistency across platforms, and hide in the inspector
        public SelectionStatus Status { get; set; }
        private static readonly List<PieceSelectionComponent> MovableObjects = new();
        public Vector2 _target { get; private set; }
        public static PieceSelectionComponent Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            MovableObjects.Add(this);
            Status = SelectionStatus.UnSelected;
            
        }
        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0) && Status == SelectionStatus.Selected)
            {
                _target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                Debug.Log("Mouse Clicked");
            }
        }

        private void Start()
        {
            _target = transform.position;
        }

        public void OnSelect()
        {
            Status = SelectionStatus.Selected;
        }

        public void OnDeselect()
        {
            Status = SelectionStatus.UnSelected;
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
    }

}