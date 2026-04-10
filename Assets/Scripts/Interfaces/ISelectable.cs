using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    /// <summary>
    /// Defines the interaction lifecycle for objects that can be focused or targeted by the user.
    /// </summary>
    /// <remarks>
    /// This interface is critical for the "Selection -> Targeting" input flow. 
    /// It provides the 'Source' data required to populate a <see cref="ICommand"/>.
    /// </remarks>
    public interface ISelectable
    {
        /// <summary>
        /// Triggers the logic for when an object gains focus (e.g., highlighting, enabling move-previews).
        /// </summary>
        void OnSelect();

        /// <summary>
        /// Triggers the logic for when an object loses focus or completes an action.
        /// </summary>
        void OnDeselect();

        /// <summary>
        /// The current interaction state of the entity (e.g., Selected, Unselected).
        /// </summary>
        SelectionStatus Status { get; set; }

        /// <summary>
        /// The retrieved target coordinate from the latest interaction, 
        /// to be exported to the movement engine.
        /// </summary>
        Vector2 Target { get; }
    }
}