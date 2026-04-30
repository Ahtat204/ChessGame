using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.PieceComponent;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class GameTests
    {
        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        // this test will simulate a Chess Game Match (Ruy Lopez opening,Morphy Defense)
        [UnityTest]
        public IEnumerator TestRuyLopezOpening()
        {
            
            SceneManager.LoadScene("GameScene");
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "GameScene");

            yield return new WaitForSeconds(2.1f);

            Dictionary<Vector2Int, PieceMovementComponent> pieces = GameManager.Instance.Pieces;
            //Arrange
            //// e4
            var e4pawn = pieces[new Vector2Int(5,2)] ; // Pawn At (5.2)
            Assert.IsNotNull(e4pawn);
            e4pawn.MovePiece(pieces,new Vector2Int(5,4));
            Assert.AreEqual(new Vector2Int(5,4),(Vector2Int) e4pawn.CurrPos);
            ////e5
            var e5pawn = pieces[new Vector2Int(5,7)] ;
            Assert.IsNotNull(e5pawn);
            e5pawn.MovePiece(pieces,new Vector2Int(5,5));
            Assert.AreEqual(new Vector2Int(5,5),(Vector2Int) e5pawn.CurrPos);
            ////Nf3
            var whiteRightKnight = pieces[new Vector2Int(7,1)] ;
            Assert.IsNotNull(whiteRightKnight);
            whiteRightKnight.MovePiece(pieces,new Vector2Int(6,3));
            Assert.AreEqual(new Vector2Int(6,3),(Vector2Int) whiteRightKnight.CurrPos);
            ////Nc6
            var blackLeftKnight = pieces[new Vector2Int(2,8)] ;
            Assert.IsNotNull(blackLeftKnight);
            blackLeftKnight.MovePiece(pieces,new Vector2Int(3,6));
            Assert.AreEqual(new Vector2Int(3,6),(Vector2Int) blackLeftKnight.CurrPos);
            ////Bb5
            var whiterighBishop = pieces[new Vector2Int(6,1)] ;
            Assert.IsNotNull(whiterighBishop);
            whiterighBishop.MovePiece(pieces,new Vector2Int(2,5));
            Assert.AreEqual(new Vector2Int(2,5),(Vector2Int) whiterighBishop.CurrPos);
            ////a6
            var a7pawn = pieces[new Vector2Int(1,7)] ;
            Assert.IsNotNull(a7pawn);
            a7pawn.MovePiece(pieces,new Vector2Int(1,6));
            Assert.AreEqual(new Vector2Int(1,6),(Vector2Int) a7pawn.CurrPos);
            //// Ba4
            whiterighBishop.MovePiece(pieces,new Vector2Int(1,4));
            Assert.AreEqual(new Vector2Int(1, 4), ((Vector2Int)whiterighBishop.CurrPos));
            ////Nf6
            var blackRightKnight2 = pieces[new Vector2Int(7,8)];
            Assert.IsNotNull(blackRightKnight2);
            blackRightKnight2.MovePiece(pieces,new Vector2Int(6,6));
            Assert.AreEqual(new Vector2Int(6,6),(Vector2Int) blackRightKnight2.CurrPos);
            ////O-0
            var whiteKing = pieces[new Vector2Int(5,1)];
            Assert.IsNotNull(whiteKing);
            whiteKing.MovePiece(pieces,new Vector2Int(7,1));
            Assert.AreEqual(new Vector2Int(7,1),(Vector2Int) whiteKing.CurrPos);

        }
    }
}