using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
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
        public static bool SwitchTurn(PlayerTurn turn,GameObject gameObject )
        {
            if (turn == PlayerTurn.BlackPlayer)
            {
                if (gameObject.tag.Equals("White"))
                {
                    return false;
                }

                if (gameObject.tag.Equals("Black"))
                {
                    return true;
                }
            }
            if(turn==PlayerTurn.WhitePlayer)
            {
                if (gameObject.tag.Equals("Black"))
                {
                    return false;
                }
            }
            return true;
        }
        public static void Switcher()
        {
            if (GameManager.Instance.Turn == PlayerTurn.BlackPlayer)
            {
               GameManager.Instance.Turn = PlayerTurn.WhitePlayer;
            }
            else if(GameManager.Instance.Turn == PlayerTurn.WhitePlayer)
            {
                GameManager.Instance.Turn = PlayerTurn.BlackPlayer;
            }
           
        }
    }
}