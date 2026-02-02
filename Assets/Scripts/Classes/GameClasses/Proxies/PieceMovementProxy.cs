using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.BehaviorClasses;
using UnityEngine;

namespace Assets.Scripts.Classes.GameClasses.Proxies
{
    /// <summary>
    /// this is class is responsible for preventing a piece from overtake a friendly piece or take its place
    /// </summary>
    public class PieceMovementProxy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pieces">this is the single source of truth of the positions of all the 32 pieces</param>
        /// <param name="start">the position of the piece</param>
        /// <param name="end">the position where the piece intends to move to</param>
        /// <param name="permission">to avoid using one bool (which can be hard to debug if we forget to reset it to true after set it to false)</param>
        /// <remarks>we <c>return</c> instead of <c>break</c> to avoid entering another Loop or condition</remarks>
        public void CheckPath(Dictionary<Vector2Int, MovementManager> pieces, Vector2Int start, Vector2Int end, out bool permission)
        {
            if (end.x == start.x) //moving horizontally 
            {
                var direction = end.y - start.y;
                if (direction > 0) //moving  to the right
                {
                    foreach (var position in pieces.Keys.Where(
                                 key => key.x == end.x && key.y < end.y && key.y > start.y))
                    {
                        if (pieces[position] is not null)
                        {
                            permission = false;
                            return;
                        }
                    }
                }
                if (direction < 0) // moving to the left
                {
                    foreach (var position in pieces.Keys.Where(
                                 key => key.x == end.x && key.y > start.y && key.y < end.y))
                    {
                        if (pieces[position] is not null)
                        {
                            permission = false;
                            return;
                        }
                    }
                }
            }

            if (end.y == start.y) //moving vertically
            {
                var direction = end.x - start.x;
                if (direction > 0) //moving to the Top
                {
                    foreach (var position in pieces.Keys.Where(
                                 key => end.y == key.y && key.x < end.x && key.x > start.x))
                    {
                        if (pieces[position] is not null)
                        {
                            permission = false;
                            return;
                        }
                    }
                }

                if (direction < 0) // move to the bottom
                {
                    foreach (var position in pieces.Keys.Where(
                                 key => end.y == key.y && key.x < start.x && key.x > end.x))
                    {
                        if (pieces[position] is not null)
                        {
                            permission = false;
                            return;
                        }
                    }
                }
            }

            permission = true;
        }
    }
}