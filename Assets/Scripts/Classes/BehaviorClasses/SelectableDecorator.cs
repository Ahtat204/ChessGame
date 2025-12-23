using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.BehaviorClasses
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Piece))]
    [RequireComponent(typeof(MovementManager))]
    internal class SelectableDecorator : MonoBehaviour, ISelectable
    {
        //UInt32 for consistency across platforms, and hide in the inspector
        public SelectionStatus Status { get; set; }
        private static readonly List<SelectableDecorator> MovableObjects = new();
        public Vector2 _target { get; private set; }
        public static SelectableDecorator Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            MovableObjects.Add(this);
            Status = SelectionStatus.UnSelected;
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

        /// <summary>
        /// Handles mouse input when the piece is selected.
        /// Updates the target position based on the cursor location.
        /// </summary>
        public void HandleInput()
        {
            if (Input.GetMouseButtonDown(0) && Status == SelectionStatus.Selected)
            {
                _target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
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