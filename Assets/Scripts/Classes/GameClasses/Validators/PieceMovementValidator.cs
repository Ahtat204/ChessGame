using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Classes.Pieces;
using UnityEngine;

namespace Assets.Scripts.Classes.GameClasses.Validators
{
    /// <summary>
    /// this is class is responsible for preventing a piece from overtake a friendly piece or take its place
    /// </summary>
    public static class PieceMovementValidator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pieces">this is the single source of truth of the positions of all the 32 pieces</param>
        /// <param name="start">the position of the piece</param>
        /// <param name="end">the position where the piece intends to move to</param>
        /// <remarks>we <c>return</c> instead of <c>break</c> to avoid entering another Loop or condition</remarks>
        public static bool ValidatePath(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end)
        {
            var piece = pieces[start].piece;
            if (piece is Knight) return true;
            int dx = Math.Abs(end.x - start.x); //difference between int the current position's x and target position's x 
            int dy = Math.Abs(end.y - start.y); //difference between int the current position's y and target position's y 

            if (dx == 0) //moving horizontally 
            {
                if (piece is Bishop) return true;
                if (dy > 0) //moving  to the right
                {
                    foreach (var position in pieces.Keys.Where(
                                 key => key.x == end.x && key.y < end.y && key.y > start.y))
                    {
                        if (pieces[position] is not null)
                        {
                            return false;
                        }
                    }
                }

                if (dy < 0) // moving to the left
                {
                    foreach (var position in pieces.Keys.Where(
                                 key => key.x == end.x && key.y > start.y && key.y < end.y))
                    {
                        if (pieces[position] is not null)
                        {
                            return false;
                        }
                    }
                }
            }

            if (end.y == start.y) //moving vertically
            {
                if (piece is Bishop ) return true;
                if (dx > 0) //moving to the Top
                {
                    foreach (var position in pieces.Keys.Where(
                                 key => end.y == key.y && key.x < end.x && key.x > start.x))
                    {
                        if (pieces[position] is not null)
                        {
                            return false;
                        }
                    }
                }

                if (dx < 0) // move to the bottom
                {
                    foreach (var position in pieces.Keys.Where(
                                 key => end.y == key.y && key.x < start.x && key.x > end.x))
                    {
                        if (pieces[position] is not null)
                        {
                            return false;
                        }
                    }
                }
            }

// TODO:still need to implement diagonal check,I commented it out because it was incorrectly stopping Queen and bishop from moving
            if (piece is Rook) return true;
            if (dy > 1 && dx > 0) //move up-right (fixed)
            {
                Debug.Log("dy > 0 && dx > 0");
                for (int i = 2; i < end.y-1; i++)
                {
                    var pos = new Vector2Int(start.x + i, start.y + i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }
            }

            if (dx < 0 && dy > 1) //move Up-left
            {
                Debug.Log("dy > 0 && dx < 0");
                for (int i = 2; i < end.y-1; i++)
                {
                    var pos = new Vector2Int(start.x - i, start.y + i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }
            }

            if (dx > 1 && dy < 0) //move down-right
            {
                Debug.Log("dy < 0 && dx > 0");
                for (int i = 2; i < end.y-1; i++)
                {
                    var pos = new Vector2Int(start.x + i, start.y - i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }
            }

            if (dx < -1 && dy < 0) //move down left
            {
                Debug.Log("dy < 0 && dx < 0");
                for (int i = 2; i <end.y-1; i++)
                {
                    var pos = new Vector2Int(start.x - i, start.y - i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }
            }


            return true;
        }
    }
}