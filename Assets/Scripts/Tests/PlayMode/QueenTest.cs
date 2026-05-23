using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.GameClasses.Validators;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Enums;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using static Assets.Scripts.Utility;

namespace Tests.PlayMode
{
    public class QueenTest
    {
        [UnityTest]
        public IEnumerator TestLegalMoves()
        {
            SceneManager.LoadScene("GameScene");
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "GameScene");
            yield return new WaitForSeconds(1.1f);
            Dictionary<Vector2Int, PieceMovementComponent> pieces = GameManager.Instance.Pieces;
            pieces.TryGetValue(new Vector2Int(4, 1), out var queenComponent);
            Assert.IsNotNull(queenComponent);
            var queen = queenComponent.piece;
            Assert.IsNotNull(queen);

        }
    }
}