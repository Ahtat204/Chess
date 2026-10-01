using System.Collections;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.GameClasses.Validators;
using Assets.Scripts.Enums;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using static Assets.Scripts.Tests.PlayMode.Helper;
using static NUnit.Framework.Assert;
namespace Assets.Scripts.Tests.PlayMode
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

            var pieces = GameManager.Instance.Pieces;
            //Arrange
            //// e4
            var e4pawn = pieces[new Vector2Int(5, 2)]; // Pawn At (5.2)
            /*IsNotNull(e4pawn);
            var move1 = e4pawn.MovePiece(pieces, new Vector2Int(5, 4));
            AreEqual(new Vector2Int(5, 4), (Vector2Int)e4pawn.CurrPos);
            AreEqual(move1, MoveType.Normal);*/
            ArrangeAndAssert(pieces, e4pawn,new Vector2Int(5, 4), MoveType.Normal);
            ////e5
            var e5pawn = pieces[new Vector2Int(5, 7)];
            /*IsNotNull(e5pawn);
            var move2 = e5pawn.MovePiece(pieces, new Vector2Int(5, 5));
            AreEqual(new Vector2Int(5, 5), (Vector2Int)e5pawn.CurrPos);
            AreEqual(move2, MoveType.Normal);*/
            ArrangeAndAssert(pieces, e5pawn, new Vector2Int(5, 5), MoveType.Normal);
            ////Nf3
            var whiteRightKnight = pieces[new Vector2Int(7, 1)];
            /*IsNotNull(whiteRightKnight);
            var move3 = whiteRightKnight.MovePiece(pieces, new Vector2Int(6, 3));
            AreEqual(new Vector2Int(6, 3), (Vector2Int)whiteRightKnight.CurrPos);
            AreEqual(move3, MoveType.Normal);*/
            ArrangeAndAssert(pieces, whiteRightKnight, new Vector2Int(6, 3), MoveType.Normal);
            ////Nc6
            var blackLeftKnight = pieces[new Vector2Int(2, 8)];
            /*IsNotNull(blackLeftKnight);
            var move4 = blackLeftKnight.MovePiece(pieces, new Vector2Int(3, 6));
            AreEqual(new Vector2Int(3, 6), (Vector2Int)blackLeftKnight.CurrPos);
            AreEqual(move4, MoveType.Normal);*/
            ArrangeAndAssert(pieces, blackLeftKnight, new Vector2Int(6, 3), MoveType.Normal);
            ////Bb5
            var whiterighBishop = pieces[new Vector2Int(6, 1)];
            IsNotNull(whiterighBishop);
            var move5 = whiterighBishop.MovePiece(pieces, new Vector2Int(2, 5));
            AreEqual(new Vector2Int(2, 5), (Vector2Int)whiterighBishop.CurrPos);
            AreEqual(move5, MoveType.Normal);
            ArrangeAndAssert(pieces, whiterighBishop,  new Vector2Int(2, 5),MoveType.Normal);
            ////a6
            var a7pawn = pieces[new Vector2Int(1, 7)];
            IsNotNull(a7pawn);
            var move6 = a7pawn.MovePiece(pieces, new Vector2Int(1, 6));
            AreEqual(new Vector2Int(1, 6), (Vector2Int)a7pawn.CurrPos);
            AreEqual(move6, MoveType.Normal);
            //// Ba4
            var move7 = whiterighBishop.MovePiece(pieces, new Vector2Int(1, 4));
            AreEqual(new Vector2Int(1, 4), (Vector2Int)whiterighBishop.CurrPos);
            AreEqual(move7, MoveType.Normal);
            ////Nf6
            var blackRightKnight2 = pieces[new Vector2Int(7, 8)];
            IsNotNull(blackRightKnight2);
            var move8 = blackRightKnight2.MovePiece(pieces, new Vector2Int(6, 6));
            AreEqual(new Vector2Int(6, 6), (Vector2Int)blackRightKnight2.CurrPos);
            AreEqual(move8, MoveType.Normal);
            ////O-0
            var whiteKing = pieces[new Vector2Int(5, 1)];
            IsNotNull(whiteKing);
            var move9 = whiteKing.MovePiece(pieces, new Vector2Int(7, 1));
            AreEqual(new Vector2Int(7, 1), (Vector2Int)whiteKing.CurrPos);
            AreEqual(move9, MoveType.ShortCastle);
            ////Be7
            var blackDarkBishop = pieces[new Vector2Int(6, 8)];
            IsNotNull(blackDarkBishop);
            var move10 = blackDarkBishop.MovePiece(pieces, new Vector2Int(5, 7));
            AreEqual(new Vector2Int(5, 7), (Vector2Int)blackDarkBishop.CurrPos);
            AreEqual(move10, MoveType.Normal);
            //// Edge case testing that the queen at (4,1) cannot go to neither to (3,2) nor to (2,3) nor to (5,2) nor to (6,3) since there pawns in the way 
            var whiteQueen = pieces[new Vector2Int(4, 1)];
            IsNotNull(whiteQueen);
            var canMove = pieces.ValidatePath(new Vector2Int(4, 1), new Vector2Int(3, 2));
            IsTrue(canMove);
            canMove = pieces.ValidatePath(new Vector2Int(4, 1), new Vector2Int(2, 3));
            IsFalse(canMove);
            canMove = pieces.ValidatePath(new Vector2Int(4, 1), new Vector2Int(6, 3));
            IsTrue(canMove);
            AreEqual(new Vector2Int(4, 1), (Vector2Int)whiteQueen.CurrPos);
        }

        [UnityTest]
        public IEnumerator TestQueensGambitOpening()
        {
            SceneManager.LoadScene("GameScene");
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == "GameScene");
            yield return new WaitForSeconds(1.1f);
            var pieces = GameManager.Instance.Pieces;
            IsNotNull(pieces);
            var d4Pawn = pieces[new Vector2Int(4, 2)];
            IsNotNull(d4Pawn);
            var result=d4Pawn.MovePiece(pieces, new Vector2Int(4, 4));
            AreEqual(result,MoveType.Normal);
            AreEqual(new Vector2Int(4, 4), (Vector2Int)d4Pawn.CurrPos); 
            var d7pawn=pieces[new Vector2Int(4, 7)];
            IsNotNull(d7pawn);
            var result2=d7pawn.MovePiece(pieces, new Vector2Int(4, 5));
            AreEqual(result2, MoveType.Normal);
            AreEqual(new Vector2Int(4, 5), (Vector2Int)d7pawn.CurrPos);
            var c4pawn=pieces[new Vector2Int(3, 2)];
            IsNotNull(c4pawn);
            var result3=c4pawn.MovePiece(pieces, new Vector2Int(3, 4));
            AreEqual(result3, MoveType.Normal);
            AreEqual(new Vector2Int(3, 4), (Vector2Int)c4pawn.CurrPos);
        }
    }
}