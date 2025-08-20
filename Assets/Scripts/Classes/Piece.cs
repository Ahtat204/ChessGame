using System.Collections.Generic;
using System.Runtime.InteropServices;
using Assets.Scripts.Classes.GameClasses;
using ChessGame.Assets.Scripts.Enums;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Classes
{
    /// <summary>
    /// Base class of all Chess pieces
    /// </summary>
    public abstract class Piece : MonoBehaviour
    {
        /// <summary>
        /// since the cam and Tilemap will be used in all 6 classes of the <code>Assets.Scripts.Classes.Pieces</code>  namespace,
        /// following the DRY rule ,
        /// it would be better to use a centralized instance and use it across all derived classes 
        /// </summary>
        protected Tilemap Tilemap;

        /// <summary>
        /// just like the <code> protected Tilemap Tilemap;</code> , we create a single camera instance which will be accessible for all pieces classes 
        /// </summary>
        protected Camera CameraMain;

        public abstract List<Vector2Int> PossibleMoves { get;  }

        /// <summary>
        /// this property represent the Piece color , and should be initialized from the inspector in derived classes
        /// </summary>
        public abstract PieceColor Color { get; }

        /// <summary>
        /// a property that represent the Value of The piece 
        /// </summary>
        public abstract uint Value { get; }

        /// <summary>
        /// this method used to calculate the legal moves for the piece , 
        /// </summary>
        /// <param name="position">position is the current position of the Piece </param>
        /// <returns>Squares is an Alias for a List of a 2D vector</returns>
        protected abstract List<Vector2Int> CalculateLegalMoves(Vector3 position);


        public virtual void Awake()
        {
            Tilemap = Board.BoardInstance.Tilemap;
            CameraMain = Camera.main;
        }

        /// <summary>
        /// for testing only
        /// </summary>
        /// <param name="item">a generic type to log it in the console </param>
        /// <param name="m">an explanation message </param>
        public static void DebugLog<T>(string m,[Optional] T item)
        {
#if UNITY_EDITOR
            Debug.Log(m + item);
#endif
        }
    }
}