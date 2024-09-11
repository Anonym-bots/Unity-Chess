using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{



    public override Dictionary<(BoardPosition, BoardPosition), Move> GetPossibleMoves(Board board)
    {
        List<BoardPosition> possibleMoves = new List<BoardPosition>();


        if (Owner == Side.White)
        {
            // !! check front space occupied
            if (true)
            {
                possibleMoves.Add(new BoardPosition(currentPos.Rank + 1, currentPos.File));

                // 
                if (currentPos.Rank == 2)
                {
                    possibleMoves.Add(new BoardPosition(currentPos.Rank + 2, currentPos.File));
                }
            }

            // check diag space black pieceType
            if(false)
            {
                possibleMoves.Add(new BoardPosition(currentPos.Rank + 2, currentPos.File - 1));
            }
        }




        // not white


        return null;
    }
}
