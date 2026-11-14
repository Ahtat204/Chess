using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Classes;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.PieceComponent;
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
        private Dictionary<Vector2Int, PieceMovementComponent> pieces;
        [UnitySetUp]
        public IEnumerator Initialize()
        {
            SceneManager.LoadScene("GameScene");
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "GameScene");
            yield return new WaitForSeconds(1.1f);
            pieces = GameManager.Instance.Pieces;
        }
        [Test]
        public void TestLegalMoves()
        {
            
            pieces.TryGetValue(new Vector2Int(4, 1), out var queenComponent);
            Assert.IsNotNull(queenComponent);
            var queen = queenComponent.piece;
            Assert.IsNotNull(queen);
        }
        [Test]
        public void Rook()
        {
           
            Span<PieceInfo> board= stackalloc PieceInfo[pieces.Count];
            pieces.ToSpan(board);
            var rookPos = new Vector2Int(1, 4);
            var kingPos = new Vector2Int(5, 4);
            var king = new PieceInfo(kingPos,PieceColor.Black,0);
            var rook= new PieceInfo(rookPos,PieceColor.White,5);
            var result=IsAttackedByRooks(king, rook,board);
            Assert.AreEqual(result,1);
        }

        [Test]
        public void Bishop()
        {
            var bishopPos = new Vector2Int(7, 6);
            var protector = new Vector2Int(6, 5);
            var kingPos = new Vector2Int(5, 4);
            var king = new PieceInfo(kingPos,PieceColor.White,0);
            var bishop = new PieceInfo(bishopPos,PieceColor.Black,4);
            var pawn = new PieceInfo(protector,PieceColor.White,1);
            Span<PieceInfo> board= stackalloc PieceInfo[3];
            board[0] = pawn;
            board[1] = king;    
            board[2] = bishop;
            var result=IsAttackedByBishops(king,bishop,board);
            Assert.AreEqual(result,0);
        }
    }
}