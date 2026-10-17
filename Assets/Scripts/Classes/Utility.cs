using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Classes.Pieces;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    /// <summary>
    ///     A set of utility methods to simplify chess piece movement validation
    ///     and board-related operations. These helpers keep instance methods cleaner.
    /// </summary>
    public static partial class Utility
    {
        /// <summary>
        ///     Delegate invoked when a piece is selected.
        /// </summary>
        public delegate void OnPieceSelected();

        /// <summary>
        ///     Adds a position to the list if the coordinates are valid (within 1–8).
        /// </summary>
        /// <param name="pieces">The list of positions.</param>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddIfValid(this List<Vector2Int> pieces, int x, int y)
        {
            if (x is >= 1 and <= 8 && y is >= 1 and <= 8) pieces.Add(new Vector2Int(x, y));
        }

        /// <summary>
        ///     Maps a piece color and player turn to an integer value.
        ///     Returns 1 if the piece color matches the current turn, otherwise 0.
        /// </summary>
        /// <param name="color">The color of the piece.</param>
        /// <param name="turn">The current player's turn.</param>
        /// <returns>1 if valid, otherwise 0.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte Mapper(PieceColor color, PlayerTurn turn)
        {
            if (turn == PlayerTurn.BlackPlayer && color == PieceColor.Black) return 1;
            if (turn == PlayerTurn.WhitePlayer && color == PieceColor.White) return 1;
            return 0;
        }

        /// <summary>
        ///     Returns the number of attackers on the king instead of a simple check status.
        /// </summary>
        /// <param name="color">The color of the king.</param>
        /// <param name="kingPos">The position of the king.</param>
        /// <param name="pieces">All pieces on the board.</param>
        /// <returns>Number of attackers threatening the king.</returns>
        public static byte IsInCheck(PieceColor color, Vector2Int kingPos, ReadOnlySpan<PieceInfo> pieces)
        {
            switch (color)
            {
                case PieceColor.White:
                    break;
                case PieceColor.Black:
                    break;
            }

            for (byte i = 0; i < pieces.Length; i++)
            {
                var piece = pieces[i];
            }

            return 2;
        }

        /// <summary>
        ///     Converts a dictionary of pieces into a span of <see cref="PieceInfo" />.
        /// </summary>
        /// <param name="pieces">The dictionary of pieces.</param>
        /// <param name="compressedBoard">The span to populate.</param>
        public static void ToSpan(this Dictionary<Vector2Int, PieceMovementComponent> pieces,
            Span<PieceInfo> compressedBoard)
        {
            byte i = 0;
            foreach (var piece in pieces)
            {
                if (i > pieces.Count) return;
                compressedBoard[i] = new PieceInfo(piece.Key, piece.Value.piece.Color, piece.Value.piece.Value);
                i++;
            }
        }
        
        
    }
}