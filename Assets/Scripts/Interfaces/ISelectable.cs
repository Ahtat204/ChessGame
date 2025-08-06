namespace Assets.Scripts.Interfaces
{
    public interface ISelectable
    {
        public void OnSelect();
        public void OnDeselect();
        public bool IsSelected { get;set; }
    }
}