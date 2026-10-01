using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Enums;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Scripts.Tests.PlayMode
{
    public static class Helper
    {
        public static void ArrangeAndAssert(Dictionary<Vector2Int, PieceMovementComponent> pieces,
            PieceMovementComponent targetPiece, 
            Vector2Int targetPosition,
            MoveType moveType)
        {
            Assert.NotNull(targetPiece);
            var move = targetPiece.MovePiece(pieces, targetPosition);
            Assert.AreEqual(targetPosition,(Vector2Int) targetPiece.CurrPos);
            Assert.AreEqual(moveType, move);
            
        }
    }
}