using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{

    /// <summary>
    /// interface for pieces that can be promoted
    /// </summary>
    public interface IPromotable
    {
        public List<Vector2Int> PossibleMoves { get;  }
        public  byte Value { get; }
    }

    public interface IPromote
    {
        public void Promotable(IPromotable newPiece);
    }
}