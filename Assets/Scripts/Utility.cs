using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Utility
    {
        public static void AddIfValid(this List<Vector2Int> pieces,int x,int y)
        {
            if (x is >= 1 and <= 8 && y is >= 1 and <= 8 )
            {
                pieces.Add(new Vector2Int(x, y));
            }
            
        }
    }
}