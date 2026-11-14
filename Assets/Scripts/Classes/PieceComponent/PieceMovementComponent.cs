using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Classes.Pieces;
using static Assets.Scripts.Classes.GameClasses.GameManager;
using Assets.Scripts.Enums;
using Assets.Scripts.Interfaces;
using UnityEngine;
using static UnityEngine.Vector2;
namespace Assets.Scripts.Classes.PieceComponent
{
    /// <summary>
    /// Manages movement and capture logic for a chess piece.
    /// Updates piece position, validates legal moves, and resolves captures.
    /// </summary>
    /// <remarks>
    /// This component requires <see cref="Piece"/>, <see cref="BoxCollider2D"/>,
    /// <see cref="SpriteRenderer"/>, and <see cref="PieceSelectionComponent"/> to function correctly.
    /// </remarks>
    [RequireComponent(typeof(Piece))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(PieceSelectionComponent))]
    public class PieceMovementComponent : MonoBehaviour, IMove
    {
        #region fields&props

        /// <summary>
        /// Reference to the <see cref="Piece"/> attached to this <c>GameObject</c>.
        /// </summary>
        public Piece piece { get; private set; }

        /// <summary>
        /// Reference to the <see cref="PieceSelectionComponent"/> attached to this <c>GameObject</c>.
        /// Used to handle selection and deselection events.
        /// </summary>
        protected PieceSelectionComponent SelectionComponent;

        /// <summary>
        /// Flag indicating whether the piece is allowed to move.
        /// Combines multiple validations (e.g., pinned to the King).
        /// </summary>
        private bool CanMove { get; set; }

        /// <summary>
        /// Cached position of the piece in grid coordinates.
        /// Avoids repeated calls to <c>transform.position</c>.
        /// </summary>
        public Vector3Int CurrPos { get; protected set; }

        #endregion

        #region methods

        /// <summary>
        /// Unity lifecycle method. Initializes references and sets default state.
        /// </summary>
        private void Awake()
        {
            piece = GetComponent<Piece>();
            CanMove = true;
        }

        /// <summary>
        /// Unity lifecycle method. Registers the piece on the board and caches its position.
        /// </summary>
        private void Start()
        {
            CurrPos = Board.BoardInstance.tilemap.WorldToCell(transform.position);
            Instance.Pieces ??= new();
            Instance.Pieces?.TryAdd((Vector2Int)CurrPos, this);
            SelectionComponent = GetComponent<PieceSelectionComponent>();
        }

        /// <inheritdoc />
        /// <summary>
        /// Attempts to move the piece to the target position.
        /// Validates legal moves, handles captures, and updates board state.
        /// </summary>
        /// <param name="pieces">Dictionary mapping positions to pieces on the board.</param>
        /// <param name="targetPos">Target position in grid coordinates.</param>
        /// <returns>
        /// A <see cref="MoveType"/> indicating the result:
        /// <list type="bullet">
        /// <item><see cref="MoveType.Normal"/> if moved to an empty square.</item>
        /// <item><see cref="MoveType.Capture"/> if an opponent piece was captured.</item>
        /// <item><see cref="MoveType.None"/> if the move was invalid.</item>
        /// </list>
        /// </returns>
        public virtual MoveType MovePiece(Dictionary<Vector2Int, PieceMovementComponent> pieces, Vector2Int targetPos)
        {
            var position = transform.position;
            CurrPos = Board.BoardInstance.tilemap.WorldToCell(position);
            piece.CalculateLegalMoves(position);

            if (!CanMove) return MoveType.None;

            var pos = new Vector3Int(targetPos.x, targetPos.y, 0);
            var worldCellCenter = Board.BoardInstance.tilemap.GetCellCenterWorld(pos);

            if (!piece.PossibleMoves.Contains(targetPos)) return MoveType.None;

            PieceMovementComponent occupied = pieces.GetValueOrDefault(targetPos);

            if (occupied is null)
                return MoveToEmptySquare(pieces, targetPos, worldCellCenter, pos, position);

            if (occupied.piece.Color == piece.Color)
                return MoveType.None;

            if (occupied.piece is King)
                return MoveType.None;

            return CapturePiece(pieces, targetPos, worldCellCenter, pos, position, occupied);
        }

        /// <summary>
        /// Moves the piece to an empty square.
        /// Updates board state and deselects the piece.
        /// </summary>
        /// <param name="pieces">Dictionary of all pieces on the board.</param>
        /// <param name="targetSquare">Target square in grid coordinates.</param>
        /// <param name="targetPos">World position of the target square.</param>
        /// <param name="pos">Target position as <see cref="Vector3Int"/>.</param>
        /// <param name="currentPos">Current world position of the piece.</param>
        /// <returns><see cref="MoveType.Normal"/> if successful.</returns>
        private MoveType MoveToEmptySquare(Dictionary<Vector2Int, PieceMovementComponent> pieces,
            Vector2Int targetSquare, Vector3 targetPos, Vector3Int pos, Vector3 currentPos)
        {
            transform.position = MoveTowards(currentPos, targetPos, 10);
            Instance.CommandStack.Push(targetSquare);
            SelectionComponent.OnDeselect();

            if (!pos.Equals(CurrPos))
            {
                pieces.Remove((Vector2Int)CurrPos);
                CurrPos = pos;
                pieces[targetSquare] = this;
            }

            return MoveType.Normal;
        }

        /// <summary>
        /// Captures an opponent piece at the target square.
        /// Updates board state, removes the captured piece, and deselects the mover.
        /// </summary>
        /// <param name="pieces">Dictionary of all pieces on the board.</param>
        /// <param name="targetSquare">Target square in grid coordinates.</param>
        /// <param name="targetPos">World position of the target square.</param>
        /// <param name="pos">Target position as <see cref="Vector3Int"/>.</param>
        /// <param name="currentPos">Current world position of the piece.</param>
        /// <param name="occupied">The opponent piece occupying the target square.</param>
        /// <returns><see cref="MoveType.Capture"/> if successful.</returns>
        private MoveType CapturePiece(Dictionary<Vector2Int, PieceMovementComponent> pieces,
            Vector2Int targetSquare,
            Vector3 targetPos,
            Vector3Int pos,
            Vector3 currentPos,
            PieceMovementComponent occupied)
        {
            transform.position = MoveTowards(currentPos, targetPos, 10);
            Instance.CommandStack.Push(targetSquare);
            SelectionComponent.OnDeselect();

            pieces.Remove(targetSquare);
            pieces.Add(targetSquare, this);
            Destroy(occupied.gameObject);

            if (!pos.Equals(CurrPos))
            {
                pieces.Remove((Vector2Int)CurrPos);
                CurrPos = pos;
                pieces[targetSquare] = this;
            }

            return MoveType.Capture;
        }

        #endregion
    }
}
