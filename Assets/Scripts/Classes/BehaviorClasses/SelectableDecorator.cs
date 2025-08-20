using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.BehaviorClasses
{
    [RequireComponent(typeof(Piece))]
    public class SelectableDecorator : MonoBehaviour, ISelectable
    {
        //UInt32 for consistency across platforms, and hide in the inspector
        public bool IsSelected { get; set; }
        private static List<SelectableDecorator> MovableObjects => new ();
        private void Awake()
        {
            MovableObjects.Add(this);
            IsSelected = false;
        }

        public void OnSelect()
        {
            IsSelected = true;
        }

        public void OnDeselect()
        {
            IsSelected = false;
        }

        private void OnMouseDown()
        {
            //Piece.DebugLog("mouse button down",transform.position);
            if (IsSelected) OnDeselect();
            else OnSelect();
            
            foreach (var obj in MovableObjects.Where(obj => obj != this))
            {
                obj.IsSelected = false;
            }
        }
        }
    }
