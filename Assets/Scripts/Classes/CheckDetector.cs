using System;
using Assets.Scripts.Classes.GameClasses;
using Assets.Scripts.Enums;
using Assets.Scripts.Structs;
using UnityEngine;

namespace Assets.Scripts.Classes
{
    public static partial class Utility
    {
        public static byte IsAttackedByKnights(in Vector2Int kingPos, in Vector2Int knightPos)
        {
            byte count = 0;
            if (knightPos.x == kingPos.x + 2 && knightPos.y == kingPos.y + 1) count++;
            if (knightPos.x == kingPos.x + 2 && knightPos.y == kingPos.y - 1) count++;
            if (knightPos.x == kingPos.x - 2 && knightPos.y == kingPos.y + 1) count++;
            if (knightPos.x == kingPos.x - 2 && knightPos.y == kingPos.y - 1) count++;
            if (knightPos.x == kingPos.x + 1 && knightPos.y == kingPos.y + 2) count++;
            if (knightPos.x == kingPos.x + 1 && knightPos.y == kingPos.y - 2) count++;
            if (knightPos.x == kingPos.x - 1 && knightPos.y == kingPos.y + 2) count++;
            if (knightPos.x == kingPos.x - 1 && knightPos.y == kingPos.y - 2) count++;
            return count;
        }

        public static byte IsAttackedByBishops(in PieceInfo king, in PieceInfo bishop, in Span<PieceInfo> pieces)
        {
            var size = pieces.Length;
            if (size == 0) return 0;
            byte count = 0;
            var kingPos = king.Position;
            var bishopPos = bishop.Position;
            var diagonal = Math.Abs(kingPos.x - bishopPos.x) - Math.Abs(kingPos.y - bishopPos.y);
            if (diagonal == 0) // the bishop is on the same diagonal as the king
            {
                if (bishopPos.x - kingPos.x == bishopPos.y - kingPos.y) // bishop is in top right of the king
                {
                    for (byte i = 1; i < size; i++)
                    {
                        var piece = pieces[i];
                        var x = piece.Position.x;
                        var y = piece.Position.y;
                        if (piece.Color == king.Color) // found a friendly piece
                        {
                            if (x - kingPos.x ==
                                y - kingPos.y) // the friendly piece is on the same diagonal as the king
                            {
                                if (x > kingPos.x && x < bishopPos.x && y > kingPos.y &&
                                    y < bishopPos.y) // it is between the king and the bishop
                                {
                                    break;
                                }
                            }
                        }
                    }

                    count++;
                }

                if (bishopPos.x - kingPos.x == kingPos.y - bishopPos.y) // bishop is in down right of the king
                {
                    for (byte i = 1; i < size; i++)
                    {
                        var piece = pieces[i];
                        var x = piece.Position.x;
                        var y = piece.Position.y;
                        if (piece.Color == king.Color) // found a friendly piece
                        {
                            if (x - kingPos.x ==
                                kingPos.y-y) // the friendly piece is on the same diagonal as the king
                            {
                                if (x > kingPos.x && x < bishopPos.x && y < kingPos.y &&
                                    y > bishopPos.y) // it is between the king and the bishop
                                {
                                    break;
                                }
                            }
                        }
                    }

                    count++;
                }
                
                if ( kingPos.x -bishopPos.x  == kingPos.y - bishopPos.y) // bishop is in down left of the king
                {
                    for (byte i = 1; i < size; i++)
                    {
                        var piece = pieces[i];
                        var x = piece.Position.x;
                        var y = piece.Position.y;
                        if (piece.Color == king.Color) // found a friendly piece
                        {
                            if ( kingPos.x -x ==
                                kingPos.y-y) // the friendly piece is on the same diagonal as the king
                            {
                                if (x < kingPos.x && x > bishopPos.x && y < kingPos.y &&
                                    y > bishopPos.y) // it is between the king and the bishop
                                {
                                    break;
                                }
                            }
                        }
                    }

                    count++;
                }
                if ( kingPos.x-bishopPos.x == bishopPos.y - kingPos.y) // bishop is in top right of the king
                {
                    for (byte i = 1; i < size; i++)
                    {
                        var piece = pieces[i];
                        var x = piece.Position.x;
                        var y = piece.Position.y;
                        if (piece.Color == king.Color) // found a friendly piece
                        {
                            if ( kingPos.x -x ==
                                y - kingPos.y) // the friendly piece is on the same diagonal as the king
                            {
                                if (x < kingPos.x && x > bishopPos.x && y > kingPos.y &&
                                    y < bishopPos.y) // it is between the king and the bishop
                                {
                                    break;
                                }
                            }
                        }
                    }

                    count++;
                }
            }

            return count;
        }

        public static byte IsAttackedByQueens(in Vector2Int kingPos, in Vector2Int queenPos)
        {
            byte count = 0;
            return count;
        }

        public static byte IsAttackedByPawns(in Vector2Int kingPos, in Vector2Int pawnPos, in PieceColor pieceColor)
        {
            var yDir = pieceColor == PieceColor.White ? 1 : -1;

            // Check if the vertical distance is exactly 1 in the pawn's forward direction
            var correctY = kingPos.y - pawnPos.y == yDir;

            // Check if the horizontal distance is exactly 1 (either side)
            var correctX = Math.Abs(kingPos.x - pawnPos.x) == 1;

            // Return 1 if both conditions are met, 0 otherwise
            return (byte)(correctX && correctY ? 1 : 0);
        }

        public static byte IsAttackedByRooks(in PieceInfo king, in PieceInfo rook, Span<PieceInfo> pieces)
        {
            var size = pieces.Length;
            if (size == 0) return 0;
            byte count = 0;
            var kingPos = king.Position;
            var rookPos = rook.Position;
            if (kingPos.x ==
                rookPos.x)
            {
                var kingY = king.Position.y;
                var rookY = rook.Position.y;
                if (kingY > rookY) // if the rook is behind the king
                {
                    for (byte i = 1; i < size; i++)
                    {
                        var piece = pieces[i];
                        if (piece.Color == king.Color && (piece.Position.x == rookPos.x && piece.Position.y < kingY &&
                                                          piece.Position.y > rookY))
                        {
                            break; //if we found a friendly piece that covering the king from behind , a piece that sits between the king and the rook}
                        }
                    }

                    count++;
                }

                if (kingY < rookY) // if the rook is in front of the king
                {
                    for (byte i = 1; i < size; i++)
                    {
                        var piece = pieces[i];
                        if (piece.Color == king.Color && (piece.Position.x == rookPos.x &&
                                                          piece.Position.y > kingY && piece.Position.y < rookY))
                        {
                            break; //if we found a friendly piece that covering the king from the front , a piece that sits between the king and the rook}
                        }
                    }

                    count++;
                }
            }

            if (kingPos.y ==
                rookPos.y)
            {
                var kingX = king.Position.x;
                var rookX = rook.Position.x;
                if (kingX > rookX) // if the rook is in the left of the king
                {
                    for (byte i = 1; i < size; i++)
                    {
                        var piece = pieces[i];
                        if (piece.Color == king.Color && (piece.Position.y == rookPos.y && piece.Position.x > kingX &&
                                                          piece.Position.x < rookX))
                        {
                            break; //if we found a friendly piece that covering the king from left , a piece that sits between the king and the rook}
                        }
                    }

                    count++;
                }

                if (kingX < rookX) // if the rook is in the right of the king
                {
                    for (byte i = 1; i < size; i++)
                    {
                        var piece = pieces[i];
                        if (piece.Color == king.Color && (piece.Position.y == rookPos.y && piece.Position.x < kingX &&
                                                          piece.Position.x > rookX))
                        {
                            break; //if we found a friendly piece that covering the king from right , a piece that sits between the king and the rook}
                        }
                    }

                    count++;
                }
            }

            return count;
        }
    }
}