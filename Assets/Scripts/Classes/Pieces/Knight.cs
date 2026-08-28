using System.Collections.Generic;
using Assets.Scripts.Enums;
using Assets.Scripts.Classes.GameClasses;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    /// <summary>
    /// Represents a Knight entity, characterized by non-linear, fixed-distance displacement.
    /// </summary>
    /// <remarks>
    /// The Knight's movement is unique as it is "atomic"—it jumps directly to the target cell 
    /// without interacting with any intermediate units. This bypasses standard collision-checking 
    /// logic required for sliding pieces (Bishops, Rooks, Queens).
    /// </remarks>
    public sealed class Knight : Piece
    {
        /// <summary>
        /// Internal buffer for potential L-shape coordinates.
        /// Fixed capacity of 8 represents the maximum degree of the Knight's vertex in a board graph.
        /// </summary>
        private readonly List<Vector2Int> _possibleMoves = new(8);

        /// <summary>
        /// Standard material value. While nominally 3, Knights are strategically 
        /// valued higher in "closed" positions where sliding pieces are obstructed.
        /// </summary>
        public override byte Value => 3;

        /// <inheritdoc cref="Piece.PossibleMoves"/>
        public override IReadOnlyList<Vector2Int> PossibleMoves => _possibleMoves;

        /// <inheritdoc/>
        [field: SerializeField]
        public override PieceColor Color { get; protected set; }

        /// <summary>
        /// Calculates the Knight's move-set using fixed (2,1) and (1,2) coordinate offsets.
        /// </summary>
        /// <param name="position">The current transform to be quantized into grid-space.</param>
        /// <remarks>
        /// Since Knights "jump," this method only requires a target-cell validity check 
        /// (boundary and allied occupancy) rather than a path-traversal check.
        /// </remarks>
        public override void CalculateLegalMoves(Vector3 position)
        {
            _possibleMoves.Clear();
            Vector2Int positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);

            // The (±2, ±1) permutations
            _possibleMoves.AddIfValid(positionCell.x + 2, positionCell.y + 1);
            _possibleMoves.AddIfValid(positionCell.x + 2, positionCell.y - 1);
            _possibleMoves.AddIfValid(positionCell.x - 2, positionCell.y + 1);
            _possibleMoves.AddIfValid(positionCell.x - 2, positionCell.y - 1);

            // The (±1, ±2) permutations
            _possibleMoves.AddIfValid(positionCell.x + 1, positionCell.y + 2);
            _possibleMoves.AddIfValid(positionCell.x + 1, positionCell.y - 2);
            _possibleMoves.AddIfValid(positionCell.x - 1, positionCell.y + 2);
            _possibleMoves.AddIfValid(positionCell.x - 1, positionCell.y - 2);
        }
    }
}