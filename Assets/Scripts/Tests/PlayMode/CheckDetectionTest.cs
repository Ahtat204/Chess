using System;
using System.Collections;
using Assets.Scripts.Classes;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using static Assets.Scripts.Classes.Utility;

namespace Assets.Scripts.Tests.PlayMode
{
    public class CheckDetectionTest
    {
        [UnitySetUp]
        public IEnumerator Initialize()
        {
            SceneManager.LoadScene("GameScene");
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "GameScene");
            yield return new WaitForSeconds(1.1f);
        }
        [Test]
        public void TestLegalMoves()
        {
            
            var pieces = GameManager.Instance.Pieces;
            pieces.TryGetValue(new Vector2Int(4, 1), out var queenComponent);
            Assert.IsNotNull(queenComponent);
            var queen = queenComponent.piece;
            Assert.IsNotNull(queen);
        }
        [Test]
        public void Rook()
        {
           
            var pieces = GameManager.Instance.Pieces;
            Span<PieceInfo> board= stackalloc PieceInfo[pieces.Count];
            pieces.ToSpan(board);
            var rookPos = new Vector2Int(1, 4);
            var kingPos = new Vector2Int(5, 4);
            var king = new PieceInfo(kingPos,PieceColor.Black,0);
            var rook= new PieceInfo(rookPos,PieceColor.White,5);
            var result=IsAttackedByRooks(king, rook,board);
            Assert.Equals(result,1);

        }
    }
}