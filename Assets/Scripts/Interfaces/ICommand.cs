using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    /// <summary>
    /// Defines a generic abstraction for the Command behavioral design pattern.
    /// Provides structural contracts to execute and revert operations across suitable use cases.
    /// </summary>
    ///<list type="bullet|number|table">
    ///<listheader>
    /// <term>Contract</term>
    ///<description>Responsibility</description>
    ///</listheader>
    ///<item>
    ///<term><see cref="moveType"/></term>
    ///<description>the nature of the move to be executed <see cref="MoveType"/></description>
    ///</item>
    ///<item>
    ///<term><see cref="Execute"/></term>
    ///<description>Updates the <see cref="GameManager.CommandStack"/> by appending a new <see cref="Vector2Int"/> to the stack</description>
    ///</item>
    ///<item>
    ///<term><see cref="Undo"/></term>
    ///<description>undo the previous move by popping the Latest element from the stack <see cref="GameManager.CommandStack"/> .</description>
    ///</item>
    ///</list>
    public interface ICommand
    {
        /// <summary>
        /// Gets or sets the specific movement categorization classification for this command.
        /// </summary>
        public MoveType moveType { get; set; }

        /// <summary>
        /// Executes the command action toward the designated coordinate destination.
        /// </summary>
        /// <param name="target">The target grid coordinates for the command execution.</param>
        public void Execute(Vector2Int target);

        /// <summary>
        /// Reverts the side effects of the executed command and restores the previous state.
        /// </summary>
        public void Undo();
    }
}