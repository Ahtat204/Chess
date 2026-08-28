using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    /// <summary>
    /// Represents a Rook entity, a major piece characterized by rectilinear sliding movement.
    /// </summary>
    /// <remarks>
    /// The Rook's influence is mapped across the cardinal axes (X and Y). 
    /// It is a critical component for endgame "ladder" maneuvers and King safety (Castling).
    /// </remarks>
    public sealed class Rook : Piece
    {
        /// <summary>
        /// Material value of the Rook. 
        /// Valued at 5, reflecting its superior "Heavy Piece" status and its ability 
        /// to force checkmate with only King support.
        /// </summary>
        public override byte Value => 5;

        /// <summary>
        /// Internal buffer for pre-calculated orthogonal coordinates.
        /// Initialized with a capacity of 14, the maximum possible moves on a standard 8x8 board.
        /// </summary>
        private readonly List<Vector2Int> _possibleMoves = new(14);

        /// <inheritdoc cref="Piece.PossibleMoves"/>
        public override IReadOnlyList<Vector2Int> PossibleMoves => _possibleMoves;

        /// <inheritdoc/>
        [field: SerializeField]
        public override PieceColor Color { get; protected set; }

        /// <summary>
        /// Calculates legal moves by projecting rays along the horizontal and vertical axes.
        /// </summary>
        /// <param name="position">The current transform coordinates to be quantized into the tilemap grid.</param>
        /// <remarks>
        /// Like other sliding units, this radial expansion currently lacks occupancy-detection 
        /// to stop the vector upon collision with another entity.
        /// </remarks>
        public override void CalculateLegalMoves(Vector3 position)
        {
            _possibleMoves.Clear();

            // Quantization of world-space to discrete grid-space
            var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);

            for (var i = 1; i <= Board.Size; i++)
            {
                _possibleMoves.AddIfValid(positionCell.x + i, positionCell.y);
                _possibleMoves.AddIfValid(positionCell.x - i, positionCell.y);
                _possibleMoves.AddIfValid(positionCell.x, positionCell.y + i);
                _possibleMoves.AddIfValid(positionCell.x, positionCell.y - i);
            }
        }
    }
}