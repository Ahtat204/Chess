using System.Collections;
using Assets.Scripts.Enums;
using NUnit.Framework;
using static NUnit.Framework.Assert;
using UnityEngine;
using UnityEngine.TestTools;
using static Assets.Scripts.Classes.Utility;


namespace Assets.Scripts.Tests.EditMode
{
    public class UnitTestsCheckDetection
    {
        [UnityTest]
        public IEnumerator KnightCheckDetection()
        {
            yield return null;
            var kingPos = new Vector2Int(5, 5);
            var knightPos = new Vector2Int(7, 6);
            var result = IsAttackedByKnights(kingPos: kingPos, knightPos: knightPos);
            AreEqual(result, 1);
            knightPos = new Vector2Int(6, 7);
            result = IsAttackedByKnights(kingPos: kingPos, knightPos: knightPos);
            AreEqual(result, 1);
            knightPos = new Vector2Int(4, 7);
            result = IsAttackedByKnights(kingPos: kingPos, knightPos: knightPos);
            AreEqual(result, 1);
            knightPos = new Vector2Int(3, 6);
            result = IsAttackedByKnights(kingPos: kingPos, knightPos: knightPos);
            AreEqual(result, 1);
            knightPos = new Vector2Int(3, 4);
            result = IsAttackedByKnights(kingPos: kingPos, knightPos: knightPos);
            AreEqual(result, 1);
            knightPos = new Vector2Int(4, 3);
            result = IsAttackedByKnights(kingPos: kingPos, knightPos: knightPos);
            AreEqual(result, 1);
            knightPos = new Vector2Int(6, 3);
            result = IsAttackedByKnights(kingPos: kingPos, knightPos: knightPos);
            AreEqual(result, 1);
            knightPos = new Vector2Int(7, 4);
            result = IsAttackedByKnights(kingPos: kingPos, knightPos: knightPos);
            AreEqual(result, 1);
            knightPos = new Vector2Int(7, 6);
            result = IsAttackedByKnights(kingPos: kingPos, knightPos: knightPos);
            AreEqual(result, 1);
        }

        [UnityTest]
        public IEnumerator PawnCheckDetection()
        {
            yield return null;
            //black king
            var kingPos = new Vector2Int(5, 5);
            var pawn = new Vector2Int(4, 4);
            var result = IsAttackedByPawns(kingPos, pawn, PieceColor.White);
            AreEqual(result, 1);
            pawn = new Vector2Int(6, 4);
            result = IsAttackedByPawns(kingPos, pawn, PieceColor.White);
            AreEqual(result, 1);
            //white king
            pawn = new Vector2Int(6, 6);
            result = IsAttackedByPawns(kingPos, pawn, PieceColor.Black);
            AreEqual(result, 1);
            pawn = new Vector2Int(4, 6);
            result = IsAttackedByPawns(kingPos, pawn, PieceColor.Black);
            AreEqual(result, 1);
            pawn = new Vector2Int(5, 5);
            result = IsAttackedByPawns(kingPos, pawn, PieceColor.Black);
            AreEqual(result, 0);
        }
    }
}