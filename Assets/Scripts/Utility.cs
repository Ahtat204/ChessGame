using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// A set of Utility methods to keep instance methods cleaner
    /// </summary>
    public static class Utility
    {
        public delegate void OnPieceSelected();

        public static void AddIfValid(this List<Vector2Int> pieces, int x, int y)
        {
            if (x is >= 1 and <= 8 && y is >= 1 and <= 8)
            {
                pieces.Add(new Vector2Int(x, y));
            }
        }

        public static int Mapper(PieceColor color, PlayerTurn turn)
        {
            if (turn == PlayerTurn.BlackPlayer && color == PieceColor.Black) return 1;
            if (turn == PlayerTurn.WhitePlayer && color == PieceColor.White) return 1;
            return 0;
        }

        public static bool QueenValidator(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end, int dx, int dy)
        {
            RookValidator(pieces, start, end, dx, dy);
            BishopValidator(pieces, start, end, dx, dy);
            return true;
        }


        public static bool RookValidator(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end, int dx, int dy)
        {
            if (dx == 0) //moving horizontally 
            {
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

            if (dy == 0) //moving vertically
            {
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

            return true;
        }

        public static bool BishopValidator(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end, int dx, int dy)
        {
            if (dy > 1 && dx > 1) //move up-right (fixed)
            {
                Debug.Log("dy > 0 && dx > 0");
                for (int i = 2; i < end.y - 1; i++)
                {
                    var pos = new Vector2Int(start.x + i, start.y + i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }
            }

            if (dx < 1 && dy > 1) //move Up-left
            {
                Debug.Log("dy > 0 && dx < 0");
                for (int i = 2; i < end.y - 1; i++)
                {
                    var pos = new Vector2Int(start.x - i, start.y + i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }
            }

            if (dx > 0 && dy < 0) //move down-right
            {
                Debug.Log("dy < 0 && dx > 0");
                for (int i = 2; i < end.x - 1; i++)
                {
                    var pos = new Vector2Int(start.x + i, start.y - i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }
            }

            if (dx < 1 && dy < 1) //move down left
            {
                Debug.Log("dy < 0 && dx < 0");
                for (int i = 2; i < end.x - 1; i++)
                {
                    var pos = new Vector2Int(start.x - i, start.y - i);
                    var found = pieces.ContainsKey(pos);
                    if (found) return false;
                }
            }

            return true;
        }

        public static bool KingValidator(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end, int dx, int dy)
        {
            return true;
        }

        public static bool PawnValidator(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int start,
            Vector2Int end, int dx, int dy)
        {
            return true;
        }
    }
}