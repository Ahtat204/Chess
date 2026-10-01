using System.Collections;
using Assets.Scripts.Classes.GameClasses;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Assets.Scripts.Tests.PlayMode
{
    public class QueenTest
    {
        [UnityTest]
        public IEnumerator TestLegalMoves()
        {
            SceneManager.LoadScene("GameScene");
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "GameScene");
            yield return new WaitForSeconds(1.1f);
            var pieces = GameManager.Instance.Pieces;
            pieces.TryGetValue(new Vector2Int(4, 1), out var queenComponent);
            Assert.IsNotNull(queenComponent);
            var queen = queenComponent.piece;
            Assert.IsNotNull(queen);
        }
    }
}