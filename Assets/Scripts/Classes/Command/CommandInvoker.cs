using Assets.Scripts.Interfaces;
namespace Assets.Scripts.Classes.Command
{
    /// <summary>
    /// Acts as the invoker component within the Command design pattern architecture.
    /// Coordinates the execution pipeline of commands by bridging user selections with system actions.
    /// </summary>
    public class CommandInvoker
    {
        /// <summary>
        /// The tracking subsystem component responsible for evaluating and holding active piece selections.
        /// </summary>
        private readonly ISelectable _pieceSelectionComponent;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandInvoker"/> class.
        /// </summary>
        /// <param name="pieceSelectionComponent">The selection state dependency to resolve execution targets.</param>
        public CommandInvoker(ISelectable pieceSelectionComponent)
        {
            _pieceSelectionComponent = pieceSelectionComponent;
        }

        /// <summary>
        /// Dispatches and processes the execution workflow of the provided command configuration.
        /// Maps the target location from the underlying selection tracking state directly into the execution payload.
        /// </summary>
        /// <param name="command">The concrete command payload containing the implementation logic to execute.</param>
        public void ExecuteCommand(ICommand command)
        {
            command.Execute(_pieceSelectionComponent.Target);
        }
    }
}
