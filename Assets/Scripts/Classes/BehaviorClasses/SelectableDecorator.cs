using System;
using Assets.Scripts.Interfaces;
using UnityEngine;


namespace Assets.Scripts.Classes.BehaviorClasses
{
    [RequireComponent(typeof(Piece))]
    public class SelectableDecorator : MonoBehaviour, ISelectable
    {
        
        private SelectableDecorator _decorator;
        /// <summary>
        /// for referencing the piece class that's attached to this gameObject together with this class ,whether it's Queen, Rook ....
        /// </summary>
        private Piece _piece;
        public SelectedPiece SelectedPiece { get; private set; }
        public uint ClickCount { get; set; }//UInt32 for consistency across platforms, and hide in the inspector
        public bool IsSelected { get; set; }

        // Start is called before the first frame update


        private void Awake()
        {
            ClickCount = 0;
            _piece = GetComponent<Piece>();
            SelectedPiece = SelectedPiece.Instance;
        }

       

        public void OnSelect()
        {
            IsSelected = true;
            
            ClickCount++;
        }

       public void OnDeselect()
        {
           IsSelected = false;
           ClickCount--;
        }

        private void OnMouseDown()
        {
            //Piece.DebugLog("mouse button down",transform.position);
            if (IsSelected) OnDeselect();
            else OnSelect();
        }

        private void OnMouseUp()
        {
            throw new NotImplementedException();
        }
    }
}