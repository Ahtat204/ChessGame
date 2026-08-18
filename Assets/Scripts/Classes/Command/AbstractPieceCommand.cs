using System;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;
namespace Assets.Scripts.Classes.Command
{
    /// <summary>
    /// Serves as the foundational base class for all piece-specific command implementations.
    /// Integrates the <see cref="ICommand"/> contract with encapsulated movement mechanics (<see cref="IMove"/>).
    /// </summary>
    public abstract class AbstractPieceCommand : ICommand
    {
        /// <summary>
        /// The underlying movement logic engine associated with this command configuration.
        /// </summary>
        protected readonly IMove Move;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractPieceCommand"/> class with a specific movement implementation.
        /// </summary>
        /// <param name="move">The core movement rule engine utilized by this command.</param>
        protected AbstractPieceCommand(IMove move)
        {
            Move = move;
        }

        /// <inheritdoc />
        public abstract MoveType moveType { get; set; }

        /// <inheritdoc />
        public abstract void Execute(Vector2Int target);

        /// <inheritdoc />
        public abstract void Undo();

        /// <summary>
        /// Factory method that dynamically instantiates a derived piece command configuration using reflection.
        /// </summary>
        /// <typeparam name="T">The concrete class type derived from <see cref="AbstractPieceCommand"/> to instantiate.</typeparam>
        /// <param name="move">The movement behavior configuration dependency to inject into the constructor.</param>
        /// <returns>A newly allocated concrete command instance of type <typeparamref name="T"/>.</returns>
        /// <exception cref="MissingMethodException">Thrown if the target type lacks a matching constructor signature.</exception>
        public static T Create<T>(IMove move) where T : AbstractPieceCommand
        {
            return (T)Activator.CreateInstance(typeof(T), move);
        }
    }
}
