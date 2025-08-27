using System.Runtime.InteropServices;
using Assets.Scripts.Classes.GameClasses;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    public static class UtilityClass
    {
        /// <summary>
        /// for testing only
        /// </summary>
        /// <param name="item">a generic type to log it in the console </param>
        /// <param name="m">an explanation message </param>
        public static void DebugLog<T>(string m, [Optional] T item)
        {
#if UNITY_EDITOR
            Debug.Log(m + item);
#endif
        }



        public static void NewPosition(this Rigidbody2D rb, Vector3Int target)
        {
            var pos= Board.BoardInstance.Tilemap.CellToWorld(target);
            rb.AddForce(pos);
        }
    }
}