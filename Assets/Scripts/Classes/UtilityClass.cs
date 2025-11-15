using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Assets.Scripts.Classes.BehaviorClasses;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
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

         

        
    }
}