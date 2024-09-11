using System;
using System.Linq;
using UnityEngine;

public class BoardSetupManager : MonoBehaviour
{
    [Header("Current Board")]
    public BoardSetup currentBoard;

    [Header("Dependencies")]
    public Transform pieceParent;
    public Piece wPawn, wKnight, wBishop, wRook, wQueen, wKing, bPawn, bKnight, bBishop, bRook, bQueen, bKing;

    public Piece[,] board = new Piece[8,8];  // This doens't seem right


    public bool TileOccupied(BoardPosition tile)
    {
        bool result = currentBoard.pieces.Any();
        result |= currentBoard.whiteKingPosition == tile;
        result |= currentBoard.blackKingPosition == tile;

        return result;
    }

    #region Setup#
    /// <summary>
    /// Spawn Pieces according to setup.
    /// </summary>
    /// <param name="setup">The boardstate to spawn</param>
    public void SpawnPieces(BoardSetup setup)
    {
        // spawn kings
        SpawnPiece(wKing, setup.whiteKingPosition);
        SpawnPiece(bKing, setup.blackKingPosition);

        PiecePosition piece;
        for (int i = 0; i < setup.pieces.Count; i++)
        {
            piece = setup.pieces[i];
            if (piece.pieceType == PieceType.King) continue;    // no double kings!

            SpawnPiece(piece);
        }
    }

    private void SpawnPiece(PiecePosition piece) => SpawnPiece(GetPrefab(piece), piece.position);
    private void SpawnPiece(Piece prefab, BoardPosition boardPos)
    {
        Vector3 pos = GetWorldPosition(boardPos);
        Piece go = Instantiate(prefab, pos, Quaternion.identity, pieceParent);
        board[boardPos.Rank - 1, boardPos.File - 1] = go;

        Debug.Log($"{boardPos} - {GetPieceFromTile(boardPos)}");
    }
    #endregion

    #region Helpers

    public Piece GetPieceFromTile(BoardPosition pos) => GetPieceFromTile(pos.Rank, pos.File);
    public Piece GetPieceFromTile(int rank, int file)
    {
        return board[rank-1, file-1];
    }

    public static Vector3 GetWorldPosition(PiecePosition pos) => GetWorldPosition(pos.position);
    public static Vector3 GetWorldPosition(BoardPosition pos)
    {
        pos.Rank = Math.Clamp(pos.Rank, 1, 8);
        int x = (pos.File - 1) * 2;
        int z = (pos.Rank - 1) * 2;

        return new Vector3(x, 0, z);
    }

    public Piece GetPrefab(PiecePosition piece) => GetPrefab(piece.pieceType, piece.color);
    public Piece GetPrefab(PieceType piece, Side col) => piece switch
    {
        PieceType.Pawn => col == Side.White ? wPawn : bPawn,
        PieceType.Knight => col == Side.White ? wKnight : bKnight,
        PieceType.Bishop => col == Side.White ? wBishop : bBishop,
        PieceType.Rook => col == Side.White ? wRook : bRook,
        PieceType.Queen => col == Side.White ? wQueen : bQueen,
        PieceType.King => col == Side.White ? wKing : bKing,
        _ => throw new ArgumentOutOfRangeException(nameof(piece), $"Not expected direction value: {piece}"),
    };
    #endregion
}
