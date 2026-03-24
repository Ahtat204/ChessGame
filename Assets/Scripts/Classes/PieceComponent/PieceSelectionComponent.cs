using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.Scripts.Classes.PieceComponent
{
    //: this class is working correctly
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Piece))]
    
    
    public class PieceSelectionComponent : MonoBehaviour, ISelectable
    {
        public SelectionStatus Status { get; set; }

        public Vector2 Target => target;
        public int Count { get; set; }
        public int ClickCount{ get; set; }
        private static readonly List<PieceSelectionComponent> MovableObjects = new();
        public Vector2 target;
        private void Awake()
        {
            MovableObjects.Add(this);
            Status = SelectionStatus.UnSelected;
            
        }
        private void Start()
        {
            target = transform.position;
            ClickCount = 0;
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
        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && Status == SelectionStatus.Selected && !target.Equals(transform.position))
            {
                target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

}