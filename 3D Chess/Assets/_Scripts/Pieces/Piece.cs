using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public Side Owner { get; protected set; }

    protected BoardPosition currentPos;

    public abstract Dictionary<(BoardPosition, BoardPosition), Move> GetPossibleMoves(Board board);


    public override string ToString() => $"{Owner} {GetType().Name}";
    public string ToTextArt() => this switch
    {
        Bishop { Owner: Side.White } => "♝",
        Bishop { Owner: Side.Black } => "♗",
        King { Owner: Side.White } => "♚",
        King { Owner: Side.Black } => "♔",
        Knight { Owner: Side.White } => "♞",
        Knight { Owner: Side.Black } => "♘",
        Queen { Owner: Side.White } => "♛",
        Queen { Owner: Side.Black } => "♕",
        Pawn { Owner: Side.White } => "♟",
        Pawn { Owner: Side.Black } => "♙",
        Rook { Owner: Side.White } => "♜",
        Rook { Owner: Side.Black } => "♖",
        _ => "."
    };

    public void MoveTo(BoardPosition pos)
    {
        Vector3 realPos = BoardSetupManager.GetWorldPosition(pos);

        // animation

        transform.position = realPos;
    }
}
