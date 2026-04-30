using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Enums;
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

            yield return new WaitForSeconds(1.1f);

            Dictionary<Vector2Int, PieceMovementComponent> pieces = GameManager.Instance.Pieces;
            //Arrange
            //// e4
            var e4pawn = pieces[new Vector2Int(5, 2)]; // Pawn At (5.2)
            Assert.IsNotNull(e4pawn);
            var move1 = e4pawn.MovePiece(pieces, new Vector2Int(5, 4));
            Assert.AreEqual(new Vector2Int(5, 4), (Vector2Int)e4pawn.CurrPos);
            Assert.AreEqual(move1, MoveType.Normal);
            ////e5
            var e5pawn = pieces[new Vector2Int(5, 7)];
            Assert.IsNotNull(e5pawn);
            var move2 = e5pawn.MovePiece(pieces, new Vector2Int(5, 5));
            Assert.AreEqual(new Vector2Int(5, 5), (Vector2Int)e5pawn.CurrPos);
            Assert.AreEqual(move2, MoveType.Normal);
            ////Nf3
            var whiteRightKnight = pieces[new Vector2Int(7, 1)];
            Assert.IsNotNull(whiteRightKnight);
            var move3 = whiteRightKnight.MovePiece(pieces, new Vector2Int(6, 3));
            Assert.AreEqual(new Vector2Int(6, 3), (Vector2Int)whiteRightKnight.CurrPos);
            Assert.AreEqual(move3, MoveType.Normal);
            ////Nc6
            var blackLeftKnight = pieces[new Vector2Int(2, 8)];
            Assert.IsNotNull(blackLeftKnight);
            var move4 = blackLeftKnight.MovePiece(pieces, new Vector2Int(3, 6));
            Assert.AreEqual(new Vector2Int(3, 6), (Vector2Int)blackLeftKnight.CurrPos);
            Assert.AreEqual(move4, MoveType.Normal);
            ////Bb5
            var whiterighBishop = pieces[new Vector2Int(6, 1)];
            Assert.IsNotNull(whiterighBishop);
            var move5 = whiterighBishop.MovePiece(pieces, new Vector2Int(2, 5));
            Assert.AreEqual(new Vector2Int(2, 5), (Vector2Int)whiterighBishop.CurrPos);
            Assert.AreEqual(move5, MoveType.Normal);
            ////a6
            var a7pawn = pieces[new Vector2Int(1, 7)];
            Assert.IsNotNull(a7pawn);
            var move6 = a7pawn.MovePiece(pieces, new Vector2Int(1, 6));
            Assert.AreEqual(new Vector2Int(1, 6), (Vector2Int)a7pawn.CurrPos);
            Assert.AreEqual(move6, MoveType.Normal);
            //// Ba4
            var move7 = whiterighBishop.MovePiece(pieces, new Vector2Int(1, 4));
            Assert.AreEqual(new Vector2Int(1, 4), ((Vector2Int)whiterighBishop.CurrPos));
            Assert.AreEqual(move1, MoveType.Normal);
            ////Nf6
            var blackRightKnight2 = pieces[new Vector2Int(7, 8)];
            Assert.IsNotNull(blackRightKnight2);
            var move8 = blackRightKnight2.MovePiece(pieces, new Vector2Int(6, 6));
            Assert.AreEqual(new Vector2Int(6, 6), (Vector2Int)blackRightKnight2.CurrPos);
            Assert.AreEqual(move8, MoveType.Normal);
            ////O-0
            var whiteKing = pieces[new Vector2Int(5, 1)];
            Assert.IsNotNull(whiteKing);
            var move9 = whiteKing.MovePiece(pieces, new Vector2Int(7, 1));
            Assert.AreEqual(new Vector2Int(7, 1), (Vector2Int)whiteKing.CurrPos);
            Assert.AreEqual(move9, MoveType.ShortCastle);
            ////Be7
            var blackDarkBishop = pieces[new Vector2Int(6, 8)];
            Assert.IsNotNull(blackDarkBishop);
            var move10 = blackDarkBishop.MovePiece(pieces, new Vector2Int(5, 7));
            Assert.AreEqual(new Vector2Int(5, 7), (Vector2Int)blackDarkBishop.CurrPos);
            Assert.AreEqual(move10, MoveType.Normal);
            //// testing that the queen at (4,1) cannot go to neither to (3,2) nor to (2,3) nor to (5,2) nor to (6,3)
            var whiteQueen = pieces[new Vector2Int(4, 1)];
            Assert.IsNotNull(whiteQueen);
            var move11 = whiteQueen.MovePiece(pieces, new Vector2Int(3, 2));
            Assert.AreEqual(new Vector2Int(4, 1), (Vector2Int)whiteQueen.CurrPos);
            Assert.AreEqual(move11,MoveType.None);
        }
    }
}