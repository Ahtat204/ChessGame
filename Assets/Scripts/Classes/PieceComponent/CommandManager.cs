using Assets.Scripts.Classes.Command;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.PieceComponent
{
    /// <summary>
    /// Orchestrates the instantiation and execution of movement commands for a specific piece.
    /// </summary>
    /// <remarks>
    /// This class serves as the 'Client' and 'Invoker' bridge in the Command Pattern. 
    /// It listens for global selection events and routes them through a 
    /// <see cref="CommandInvoker"/> to maintain a clean history and execution flow.
    /// </remarks>
    /// <inheritdoc cref="MonoBehaviour"/>
    public sealed class CommandManager : MonoBehaviour
    {
        #region Private Dependencies

        /// <summary>
        /// Reference to the movement engine (e.g., PieceMovementComponent).
        /// </summary>
        private IMove _move;

        /// <summary>
        /// The specific command instance to be executed when a move is triggered.
        /// </summary>
        private ICommand _command;

        /// <summary>
        /// Reference to the selection state of the piece.
        /// </summary>
        private ISelectable _pieceSelectionComponent;

        /// <summary>
        /// The execution wrapper that triggers the command lifecycle.
        /// </summary>
        private CommandInvoker _invoker;

        #endregion

        #region Lifecycle Methods

        /// <summary>
        /// Initializes dependencies and prepares the Concrete Command instance.
        /// </summary>
        private void Start()
        {
            // Resolve interface-based dependencies from the current GameObject
            _pieceSelectionComponent = GetComponent<ISelectable>();
            _move = GetComponent<IMove>();

            // Initialize the invoker with the selection context
            _invoker = new CommandInvoker(_pieceSelectionComponent);

            // Create the specific movement command using a Factory-style pattern
            _command = AbstractPieceCommand.Create<ConcreteMoveCommand>(_move);
        }

        /// <summary>
        /// Subscribes to the global piece selection event when the component is active.
        /// </summary>
        public void OnEnable() => PieceSelectionComponent.OnPieceSelectedEvent += DoWork;

        /// <summary>
        /// Unsubscribes from the global event to prevent memory leaks or null reference exceptions.
        /// </summary>
        public void OnDisable() => PieceSelectionComponent.OnPieceSelectedEvent -= DoWork;

        #endregion

        #region Execution Logic

        /// <summary>
        /// Callback method triggered by <see cref="PieceSelectionComponent.OnPieceSelectedEvent"/>.
        /// Hands off the movement command to the invoker for execution.
        /// </summary>
        private void DoWork()
        {
            {
                if (PieceSelectionComponent.SelectedPiece == (PieceSelectionComponent)_pieceSelectionComponent)
                    // Execute the pre-configured command (typically a ConcreteMoveCommand)
                    _invoker.ExecuteCommand(_command);
                Debug.Log($"{nameof(DoWork)} executed");
            }
        }

        #endregion
    }
}