using System;
using Assets.Scripts.Interfaces;

using UnityEngine;


namespace Assets.Scripts.Classes
{
    public class SelectableDecorator : MonoBehaviour, ISelectable
    {
        private SelectableDecorator _decorator;
        private Piece _piece;

        public bool IsSelected { get; set; }

        // Start is called before the first frame update


        private void Awake()
        {
            _piece = GetComponent<Piece>();
        }

        private void Update()
        {
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
            if (IsSelected) OnDeselect();
            else OnSelect();
        }

        private void OnMouseUp()
        {
            throw new NotImplementedException();
        }
    }
}