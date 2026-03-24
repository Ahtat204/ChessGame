using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface ISelectable
    {
        public void OnSelect();
        public void OnDeselect();
        public SelectionStatus Status { get;set; }
        public Vector2 Target{ get; }

    }
}