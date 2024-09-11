using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Move
{
    public BoardPosition start, end;
    public PieceType pieceType;

    
    public Move(BoardPosition start, BoardPosition end)
    {
        this.start = start;
        this.end = end;
    }

    public Move(Move move)
    {
        this.start = move.start;
        this.end = move.end;
        this.pieceType = move.pieceType;
    }


    #region Overrides

    public override string ToString() => $"{start}->{end}";


    public static string PieceTypeToString(PieceType piece)
    {
        string result = "";
        switch (piece)
        {
            case PieceType.Pawn:
                result = "";
                break;
            case PieceType.Knight:
                result = "N";
                break;
            case PieceType.Bishop:
                result = "B";
                break;
            case PieceType.Rook:
                result = "R";
                break;
            case PieceType.Queen:
                result = "Q";
                break;
            case PieceType.King:
                result = "K";
                break;
            default:
                break;
        }

        return result;
    }
    #endregion
}
