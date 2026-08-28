using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    /// <summary>
    /// Represents a Pawn entity, the fundamental unit of board structure and pawn-chain dynamics.
    /// </summary>
    /// <remarks>
    /// Unlike other pieces, the Pawn exhibits asymmetric behavior:
    /// 1. Forward displacement is restricted to non-capturing moves.
    /// 2. Diagonal displacement is restricted to capturing maneuvers.
    /// 3. Initial state allows for double-impulse movement (2 cells).
    /// </remarks>
    public sealed class Pawn : Piece
    {
        /// <summary>
        /// Internal buffer for potential moves, including standard, double-step, 
        /// captures, and specialized maneuvers.
        /// </summary>
        private List<Vector2Int> _possibleMoves = new(5);

        /// <summary>
        /// Base unit value. In endgame heuristics, this value scales non-linearly 
        /// as the Pawn approaches the promotion rank.
        /// </summary>
        public override byte Value => 1;

        /// <inheritdoc cref="Piece.PossibleMoves"/>
        public override IReadOnlyList<Vector2Int> PossibleMoves => _possibleMoves;

        /// <inheritdoc/>
        [field: SerializeField]
        public override PieceColor Color { get; protected set; }

        /// <summary>
        /// Returns the directional scalar based on faction color.
        /// </summary>
        /// <returns>1 for White (incrementing Y), -1 for Black (decrementing Y).</returns>
        private int Sign(PieceColor color) => color == PieceColor.White ? 1 : -1;

        /// <summary>
        /// Calculates legal moves based on the Pawn's current rank and surrounding occupancy.
        /// </summary>
        /// <param name="position">Current transform to be quantized into grid-space.</param>
        /// <remarks>
        /// Note: This method populates "possible" coordinates. Actual legality (e.g., verifying 
        /// a piece exists for a diagonal capture) is deferred to the movement arbiter.
        /// </remarks>
        public override void CalculateLegalMoves(Vector3 position)
        {
            _possibleMoves.Clear();
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);
            var forward = Sign(Color);

            // 1. Standard Forward Advance (requires empty target)
            _possibleMoves.AddIfValid(positionCell.x, positionCell.y + forward);

            // 2. Initial Double-Step (only valid from starting rank)
            _possibleMoves.AddIfValid(positionCell.x, positionCell.y + (2 * forward));

            // 3. Diagonal Captures (requires opponent occupancy or En Passant state)
            _possibleMoves.AddIfValid(positionCell.x + 1, positionCell.y + forward);
            _possibleMoves.AddIfValid(positionCell.x - 1, positionCell.y + forward);
        }

        public static explicit operator Queen(Pawn piece)
        {
            return piece.GetComponent<Queen>();
        }
    }
}