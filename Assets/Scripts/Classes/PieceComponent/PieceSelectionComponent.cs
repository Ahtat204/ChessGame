using System;
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

        public Vector2 Target => _target;

        private static readonly List<PieceSelectionComponent> MovableObjects = new();
        public Vector2 _target;
        public static PieceSelectionComponent Instance { get; private set; }

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
            if (Input.GetMouseButtonDown(0) && Status == SelectionStatus.Selected)
            {
                _target = Board.BoardInstance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
                var t=Board.BoardInstance.tilemap.WorldToCell(_target);
                Debug.Log($"the position is \t :{t}");
            }
        }
    }

}