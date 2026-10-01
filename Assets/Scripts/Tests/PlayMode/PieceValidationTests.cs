using System;
using System.Collections;
using Assets.Scripts.Classes.GameClasses;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using static Assets.Scripts.Classes.Utility;

namespace Assets.Scripts.Tests.PlayMode
{
    public class PieceValidationTests
    {
        [UnityTest]
        public IEnumerator TestPawnCapturing()
        {
            SceneManager.LoadScene("GameScene");
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "GameScene");
            yield return new WaitForSeconds(1.1f);
            var pieces = GameManager.Instance.Pieces;
            var e4Pawn = pieces[new Vector2Int(5, 2)] ??
                         throw new ArgumentNullException("pieces[new Vector2Int(5, 2)]"); // Pawn At (5.2)
            Assert.IsNotNull(e4Pawn);
            var move1 = PawnValidator(pieces, (Vector2Int)e4Pawn.CurrPos, new Vector2Int(6, 3));
            Assert.AreEqual(move1, false);
            move1 = PawnValidator(pieces, (Vector2Int)e4Pawn.CurrPos, new Vector2Int(4, 3));
            Assert.AreEqual(move1, false);
            var e7Pawn = pieces[new Vector2Int(5, 7)];
            Assert.IsNotNull(e7Pawn);
            move1 = PawnValidator(pieces, (Vector2Int)e7Pawn.CurrPos, new Vector2Int(6, 6));
            Assert.AreEqual(move1, false);
            move1 = PawnValidator(pieces, (Vector2Int)e7Pawn.CurrPos, new Vector2Int(4, 6));
            Assert.AreEqual(move1, false);
        }
    }
}