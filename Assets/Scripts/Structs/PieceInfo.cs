using System.Runtime.InteropServices;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Structs
{
    /// <summary>
    /// Represents a highly optimized, immutable snapshot of chess piece state data.
    /// </summary>
    /// <remarks>
    /// This structure acts as a lightweight memory representation of the board state designed to optimize 
    /// nested iteration loops. By packing critical state into a contiguous block, a sequence of these structs 
    /// can be cached within approximately 3 standard CPU L1/L2 cache lines. 
    /// <para>
    /// This design purposefully circumvents the high cache-miss overhead associated with querying the 
    /// <c>GameManager.Pieces</c> (<see cref="System.Collections.Generic.Dictionary{Vector2Int, PieceMovementComponent}"/>) 
    /// collection, which introduces repetitive pointer indirection and poor reference locality during heavy evaluation cycles.
    /// </para>
    /// </remarks>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PieceInfo
    {
        /// <summary>
        /// The active 2D grid coordinates of the piece on the board matrix.
        /// </summary>
        public readonly Vector2Int Position;

        /// <summary>
        /// The team color affiliation of the chess piece.
        /// </summary>
        public readonly PieceColor Color;

        /// <summary>
        /// The static algorithmic value weight assigned to this piece type (e.g., Pawn=1, Queen=9).
        /// </summary>
        public readonly byte MaterialValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="PieceInfo"/> snapshot struct.
        /// </summary>
        /// <param name="position">The current grid coordinates of the piece.</param>
        /// <param name="color">The color orientation of the piece.</param>
        /// <param name="materialValue">The byte-sized evaluation weight value of the piece.</param>
        public PieceInfo(Vector2Int position, PieceColor color, byte materialValue)
        {
            Color = color;
            MaterialValue = materialValue;
            Position = position;
        }
    }
}
