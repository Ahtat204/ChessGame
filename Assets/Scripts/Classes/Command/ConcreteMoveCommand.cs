using System;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.Command
{
    /// <summary>
    /// Implements a concrete command execution sequence specifically dedicated to standard chess piece movement operations.
    /// </summary>
    public class ConcreteMoveCommand : AbstractPieceCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConcreteMoveCommand"/> class with a specified movement behavior.
        /// </summary>
        /// <param name="move">The localized movement behavior engine injected to handle piece displacement rules.</param>
        public ConcreteMoveCommand(IMove move) : base(move)
        {
        }

        /// <inheritdoc />
        public override MoveType moveType { get; set; }

        /// <summary>
        /// Executes the concrete movement routine against the system-wide active piece matrix.
        /// Captures and stores the resultant <see cref="MoveType"/> categorization payload from the operation.
        /// </summary>
        /// <param name="target">The destination grid coordinate assigned for the target piece placement.</param>
        public override void Execute(Vector2Int target)
        {
            moveType = Move.MovePiece(GameManager.Instance.Pieces, target);
        }

        /// <summary>
        /// Reverts the side effects of this movement command to safely restore the board back to its previous state.
        /// </summary>
        /// <exception cref="NotImplementedException">Thrown because the structural undo execution workflow has not yet been defined.</exception>
        public override void Undo()
        {
            throw new NotImplementedException();
        }
    }
}
