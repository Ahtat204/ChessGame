using Assets.Scripts.Enums;

namespace Assets.Scripts.Interfaces
{
    public interface ISelectable
    {
        public void OnSelect();
        public void OnDeselect();
        public SelectionStatus Status { get;set; }
    }
}