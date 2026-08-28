using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.PieceComponent;
using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    /// <summary>
    /// Represents a Bishop entity with diagonal sliding movement logic.
    /// </summary>
    /// <remarks>
    /// The Bishop's move-set is mathematically defined by the intersection 
    /// of the board's grid and the lines $y = x + c$ and $y = -x + c$.
    /// </remarks>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PieceMovementComponent))]
    [RequireComponent(typeof(PieceSelectionComponent))]
    public sealed class Bishop : Piece
    {
        /// <summary>
        /// Material value of the Bishop. 
        /// Typically weighted as 3 in standard evaluation, though often 
        /// valued slightly higher (3.25) in "Bishop Pair" heuristics.
        /// </summary>
        public override byte Value => 4;

        /// <summary>
        /// Internal buffer for pre-calculated legal coordinates. 
        /// Initialized with a capacity of 14, the maximum possible moves for a Bishop.
        /// </summary>
        private readonly List<Vector2Int> _possibleMoves = new(14);

        /// <inheritdoc cref="Piece.PossibleMoves"/>
        public override IReadOnlyList<Vector2Int> PossibleMoves => _possibleMoves;

        /// <inheritdoc/>
        [field: SerializeField]
        public override PieceColor Color { get; protected set; }

        /// <summary>
        /// Performs a radial search along the four diagonal vectors to 
        /// determine current legal move coordinates.
        /// </summary>
        /// <param name="position">The current world-space transform of the piece.</param>
        /// <remarks>
        /// This implementation uses a simple iterative expansion. 
        /// Note: Currently lacks "collision blocking" logic (e.g., stopping when hitting another piece).
        /// </remarks>
        public override void CalculateLegalMoves(Vector3 position)
        {
            _possibleMoves.Clear();

            // Vector quantization: Convert world-space to discrete grid coordinates
            Vector2Int positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);

            for (var i = 1; i <= Board.Size; i++)
            {
                // North-East, South-East, North-West, South-West diagonals
                _possibleMoves.AddIfValid(positionCell.x + i, positionCell.y + i);
                _possibleMoves.AddIfValid(positionCell.x + i, positionCell.y - i);
                _possibleMoves.AddIfValid(positionCell.x - i, positionCell.y + i);
                _possibleMoves.AddIfValid(positionCell.x - i, positionCell.y - i);
            }
        }
    }
}