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
    internal class SelectableDecorator : MonoBehaviour, ISelectable
    {
        //UInt32 for consistency across platforms, and hide in the inspector

        public SelectionStatus Status { get; set; }
        private static List<SelectableDecorator> MovableObjects => new();

        private void Awake()
        {
            MovableObjects.Add(this);
            Status = SelectionStatus.UnSelected;
        }

        public void OnSelect()
        {
            //IsSelected = true;
            Status = SelectionStatus.Selected;
          //  Debug.Log(Board.BoardInstance.tilemap.WorldToCell(transform.position)+gameObject.name);
        }

        public void OnDeselect()
        {
            //  IsSelected = false;
            Status = SelectionStatus.UnSelected;
        }

        private void OnMouseDown()
        {
            //Piece.DebugLog("mouse button down",transform.position);
            if (Status == SelectionStatus.Selected) OnDeselect();
            else OnSelect();

            foreach (var obj in MovableObjects.Where(obj => obj != this))
            {
                obj.Status = SelectionStatus.UnSelected;
                //   obj.IsSelected = false;
                // obj.Status = _unSelected ? SelectionStatus.UnSelected : SelectionStatus.Selected;
            }
        }
    }
}