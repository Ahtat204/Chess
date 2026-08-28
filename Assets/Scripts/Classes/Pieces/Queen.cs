using System.Collections.Generic;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Classes.Pieces
{
    /// <summary>
    /// Represents the Queen entity, the most powerful sliding unit in the simulation.
    /// </summary>
    /// <remarks>
    /// The Queen's move-set is a composite of linear (Rook) and diagonal (Bishop) vectors.
    /// It possesses the highest mobility coefficient, capable of controlling up to 27 squares 
    /// simultaneously from a central position.
    /// </remarks>
    public sealed class Queen : Piece

    {
    /// <summary>
    /// Internal buffer for pre-calculated legal coordinates.
    /// Initialized with a capacity of 28 to accommodate the maximum theoretical reach.
    /// </summary>
    private readonly List<Vector2Int> _possibleMoves = new(28);

    /// <summary>
    /// Standard material value. In strategic evaluation, the Queen is the 
    /// primary force-multiplier for tactical combinations.
    /// </summary>
    public override byte Value => 9;

    /// <inheritdoc cref="Piece.PossibleMoves"/>
    public override IReadOnlyList<Vector2Int> PossibleMoves => _possibleMoves;

    /// <inheritdoc/>
    [field: SerializeField]
    public override PieceColor Color { get; protected set; }

    /// <summary>
    /// Executes an omnidirectional radial search, populating both orthogonal 
    /// and diagonal vectors.
    /// </summary>
    /// <param name="position">The world-space transform to be quantized into grid coordinates.</param>
    /// <remarks>
    /// Current iteration performs a full-board sweep. Like the Bishop, this requires 
    /// collision-interruption logic to prevent "jumping" over occupied cells.
    /// </remarks>
    public override void CalculateLegalMoves(Vector3 position)
    {
        _possibleMoves.Clear();
        var positionCell = (Vector2Int)Board.BoardInstance.tilemap.WorldToCell(position);

        for (var i = 1; i <= Board.Size; i++)
        {
            // Straight Vectors (Rook-like)
            _possibleMoves.AddIfValid(positionCell.x + i, positionCell.y);
            _possibleMoves.AddIfValid(positionCell.x - i, positionCell.y);
            _possibleMoves.AddIfValid(positionCell.x, positionCell.y + i);
            _possibleMoves.AddIfValid(positionCell.x, positionCell.y - i);

            // Diagonal Vectors (Bishop-like)
            _possibleMoves.AddIfValid(positionCell.x + i, positionCell.y + i);
            _possibleMoves.AddIfValid(positionCell.x + i, positionCell.y - i);
            _possibleMoves.AddIfValid(positionCell.x - i, positionCell.y + i);
            _possibleMoves.AddIfValid(positionCell.x - i, positionCell.y - i);
        }
    }
    }
}