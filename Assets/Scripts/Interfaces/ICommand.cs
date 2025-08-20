namespace Assets.Scripts.Interfaces
{
    /// <summary>
    /// this is a generic command pattern interface for all suitable use cases
    /// </summary>
    public interface ICommand
    {
        public void Execute();
        public void Undo();
    }
}