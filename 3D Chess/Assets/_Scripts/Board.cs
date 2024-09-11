using System;
using UnityEngine;

[Serializable]
public class Board
{

    private readonly Piece[,] boardMatrix;

    public Piece this[BoardPosition position]
    {
        get
        {
            if (position.IsValid()) return boardMatrix[position.File - 1, position.Rank - 1];
            else throw new ArgumentOutOfRangeException($"Position was out of range: {position}");
        }

        set
        {
            if (position.IsValid()) boardMatrix[position.File - 1, position.Rank - 1] = value;
            else throw new ArgumentOutOfRangeException($"Position was out of range: {position}");
        }
    }
    public Piece this[int file, int rank]
    {
        get => this[new BoardPosition(file, rank)];
        set => this[new BoardPosition(file, rank)] = value;
    }

    public Board(params PiecePosition[] squarePiecePairs)
    {
        boardMatrix = new Piece[8, 8];

        foreach (PiecePosition pos in squarePiecePairs)
        {
            this[pos.position] = pos.piece;
        }
    }
    public Board(Board board)
    {
        // TODO optimize this method
        // Creates deep copy (makes copy of each piece and deep copy of their respective ValidMoves lists) of board (list of BasePiece's)
        // this may be a memory hog since each Board has a list of Piece's, and each piece has a list of Movement's
        // avg number turns/Board's per game should be around ~80. usual max number of pieces per board is 32
        boardMatrix = new Piece[8, 8];
        for (int file = 1; file <= 8; file++)
        {
            for (int rank = 1; rank <= 8; rank++)
            {
                Piece pieceToCopy = board[file, rank];
                if (pieceToCopy == null) { continue; }

                // * this[file, rank] = pieceToCopy.DeepCopy();
            }
        }
    }

    internal bool IsOccupiedAt(BoardPosition position) => this[position] != null;

    internal bool IsOccupiedBySideAt(BoardPosition position, Side side) => this[position] is Piece piece && piece.Owner == side;



    //public static readonly (BoardPosition, Piece)[] StartingPositionPieces = {
    //        (new BoardPosition("a1"), new Rook(Side.White)),
    //        (new BoardPosition("b1"), new Knight(Side.White)),
    //        (new BoardPosition("c1"), new Bishop(Side.White)),
    //        (new BoardPosition("d1"), new Queen(Side.White)),
    //        (new BoardPosition("e1"), new King(Side.White)),
    //        (new BoardPosition("f1"), new Bishop(Side.White)),
    //        (new BoardPosition("g1"), new Knight(Side.White)),
    //        (new BoardPosition("h1"), new Rook(Side.White)),

    //        (new BoardPosition("a2"), new Pawn(Side.White)),
    //        (new BoardPosition("b2"), new Pawn(Side.White)),
    //        (new BoardPosition("c2"), new Pawn(Side.White)),
    //        (new BoardPosition("d2"), new Pawn(Side.White)),
    //        (new BoardPosition("e2"), new Pawn(Side.White)),
    //        (new BoardPosition("f2"), new Pawn(Side.White)),
    //        (new BoardPosition("g2"), new Pawn(Side.White)),
    //        (new BoardPosition("h2"), new Pawn(Side.White)),

    //        (new BoardPosition("a8"), new Rook(Side.Black)),
    //        (new BoardPosition("b8"), new Knight(Side.Black)),
    //        (new BoardPosition("c8"), new Bishop(Side.Black)),
    //        (new BoardPosition("d8"), new Queen(Side.Black)),
    //        (new BoardPosition("e8"), new King(Side.Black)),
    //        (new BoardPosition("f8"), new Bishop(Side.Black)),
    //        (new BoardPosition("g8"), new Knight(Side.Black)),
    //        (new BoardPosition("h8"), new Rook(Side.Black)),

    //        (new BoardPosition("a7"), new Pawn(Side.Black)),
    //        (new BoardPosition("b7"), new Pawn(Side.Black)),
    //        (new BoardPosition("c7"), new Pawn(Side.Black)),
    //        (new BoardPosition("d7"), new Pawn(Side.Black)),
    //        (new BoardPosition("e7"), new Pawn(Side.Black)),
    //        (new BoardPosition("f7"), new Pawn(Side.Black)),
    //        (new BoardPosition("g7"), new Pawn(Side.Black)),
    //        (new BoardPosition("h7"), new Pawn(Side.Black)),
    //    };
}
