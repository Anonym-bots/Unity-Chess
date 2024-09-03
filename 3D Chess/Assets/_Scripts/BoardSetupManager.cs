using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class BoardSetupManager : MonoBehaviour
{


    [Header("Current Board")]
    public BoardSetup currentBoard;

    [Header("Dependencies")]
    public Transform pieceParent;
    public Piece wPawn, wKnight, wBishop, wRook, wQueen, wKing, bPawn, bKnight, bBishop, bRook, bQueen, bKing;

    public PieceType[,] board;  // This doens't seem right


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
        PiecePosition piece;
        Piece prefab;
        Vector3 pos;

        // spawn kings
        prefab = wKing;
        pos = GetWorldPosition(setup.whiteKingPosition);

        Instantiate(prefab, pos, Quaternion.identity, pieceParent);

        prefab = bKing;
        pos = GetWorldPosition(setup.blackKingPosition);

        Instantiate(prefab, pos, Quaternion.identity, pieceParent);



        for (int i = 0; i < setup.pieces.Count; i++)
        {
            piece = setup.pieces[i];
            if (piece.piece == PieceType.King) continue;    // no double kings!
            prefab = GetPrefab(piece);
            pos = GetWorldPosition(piece);

            Piece go = Instantiate(prefab, pos, Quaternion.identity, pieceParent);

            board[piece.position.Rank, piece.position.file] = piece.piece;
        }
    }
    #endregion

    #region Helpers
    public static Vector3 GetWorldPosition(PiecePosition pos) => GetWorldPosition(pos.position);

    public static Vector3 GetWorldPosition(BoardPosition pos)
    {
        pos.Rank = Math.Clamp(pos.Rank, 1, 8);
        int x = (pos.file - 1) * 2;
        int z = (pos.Rank - 1) * 2;

        return new Vector3(x, 0, z);
    }

    public Piece GetPrefab(PiecePosition piece) => GetPrefab(piece.piece, piece.color);

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
