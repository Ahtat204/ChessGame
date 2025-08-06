using System.Collections.Generic;
using Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Classes
{
    /// <summary>
    /// Base class of all Chess pieces
    /// </summary>
    public abstract class Piece : MonoBehaviour
    {
        protected Tilemap Tilemap;
        protected Camera CameraMain;

        public abstract List<Vector2Int> PossibleMoves { get; protected set; }

        /// <summary>
        /// this property represent the Piece color , and should be initialized from the inspector in derived classes
        /// </summary>
        public abstract PieceColor Color { get; }

        /// <summary>
        /// a property that represent the Value of The piece 
        /// </summary>
        public abstract uint Value { get; }

        /// <summary>
        /// this method used to move the piece
        /// </summary>
        /// <param name="to"> to is the square where the piece it should move to </param>
        protected virtual void Move(Vector3Int to)
        {
        }

        /// <summary>
        /// this method used to calculate the legal moves for the piece , 
        /// </summary>
        /// <param name="position">position is the current position of the Piece </param>
        /// <returns>Squares is an Alias for a List of a 2D vector</returns>
        protected abstract List<Vector2Int> CalculateLegalMoves(Vector3 position);

        /// <summary>
        /// since the cam and Tilemap will be used in all 6 classes of the Assets.Scripts.Classes.Pieces namespace,
        /// following the DRY rule ,
        /// it would be better to use a centralized instance and use it across all derived classes 
        /// </summary>
        public virtual void Awake()
        {
            Tilemap = Board.BoardInstance.Tilemap;
            CameraMain = Camera.main;
        }
/// <summary>
/// for testing only
/// </summary>
/// <param name="ITEM">a generic type to log in the console </param>
/// <param name="m">an explnation message </param>
        public static void DebugLog<T> (string m,T ITEM )
        {
#if UNITY_EDITOR
            Debug.Log("Selected Piece" + ITEM);
#endif
        }
    }

   
}
