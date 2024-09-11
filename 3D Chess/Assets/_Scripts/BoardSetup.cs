using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class BoardSetup : IDisposable
{
    public BoardPosition whiteKingPosition, blackKingPosition;
    public List<PiecePosition> pieces;

    
    
    public BoardSetup(BoardPosition whiteKing, BoardPosition blackKing, params PiecePosition[] piecePos)
    {
        whiteKingPosition = whiteKing;
        blackKingPosition = blackKing;
        for (int i = 0; i < piecePos.Length; i++)
        {
            AddPiece(piecePos[i]);
        }
    }

    /// <summary>
    /// Add a pieceType to a board setup.
    /// </summary>
    /// <param name="piece">The pieceType and its position</param>
    /// <param name="ownerColor">The color of the pieceType</param>
    /// <returns>Whether the call was successfull</returns>
    public bool AddPiece(PiecePosition piece)
    {
        // each side can only have one king
        if (piece.pieceType == PieceType.King)
        {
            Side col;
            for (int i = 0; i < Enum.GetNames(typeof(PieceType)).Length; i++)
            {
                col = (Side)i;
                if(piece.color == col && pieces.Any(x => x.pieceType == PieceType.King && x.color == col))
                {
                    // error: each side can only have one king
                    return false;
                }
            }
        }

        if(pieces.Any(x => x.GetTileNr() == piece.GetTileNr()))
        {
            // position already occupied
            return false;
        }

        pieces.Add(piece);
        return true;
    }

    public void Dispose()
    {
        // Debug.Log("Dispose called.");
        // perform cleanup here
    }
}


public enum File
{
    A, B, C, D, E, F, G, H
}

public enum PieceType
{
    Pawn, Knight, Bishop, Rook, Queen, King
}

public enum Side
{
    White, Black, None
}


[Serializable]
public class BoardPosition
{
    [SerializeField][Range(1, 8)] private int rank = 1;
    [SerializeField][Range(1, 8)] private int file = 1;

    public int Rank { get => rank; set => rank = value; }
    public int File { get => file; set => file = value; }


    public BoardPosition(int rank, File file)
    {
        rank = Mathf.Clamp(rank, 1, 8);
        this.file = (int)file;
        this.rank = rank;
    }
    public BoardPosition(int rank, int file)
    {
        this.rank = Mathf.Clamp(rank, 1, 8);
        this.file = Mathf.Clamp(file, 1, 8);
    }


    public File GetFile() => (File)(file-1);
    public int GetTileNr() => (rank - 1) * 8 + file;
    public bool IsValid() => rank > 0 && rank < 9 && file > 0 && file < 9;

    #region Operator Overrides
    public override string ToString() => $"{GetFile().ToString()}{Rank}";
    public static BoardPosition operator +(BoardPosition lhs, BoardPosition rhs) => new BoardPosition(lhs.rank + rhs.Rank, (File)((int)lhs.file + (int)rhs.file));
    public static bool operator ==(BoardPosition a, BoardPosition b) => a.rank == b.rank && a.file == b.file;
    public static bool operator !=(BoardPosition a, BoardPosition b) => a.rank != b.rank || a.file != b.file;

    public override bool Equals(object obj)
    {
        return obj is BoardPosition position &&
               Rank == position.Rank &&
               file == position.file;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    #endregion
}

[Serializable]
public class PiecePosition
{
    public BoardPosition position;
    public PieceType pieceType;
    public Side color;
    public Piece piece;


    public PiecePosition(BoardPosition pos, PieceType pieceType, Side col)
    {
        position = pos;
        this.pieceType = pieceType;
        color = col;
    }

    public int GetTileNr() => position.GetTileNr();
}