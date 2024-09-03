using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{



    public override BoardPosition[] GetPossibleMoves()
    {
        List<BoardPosition> possibleMoves = new List<BoardPosition>();


        if (currentPos.color == Side.White)
        {
            // !! check front space occupied
            if (true)
            {
                possibleMoves.Add(new BoardPosition(currentPos.position.Rank + 1, currentPos.position.file));

                // 
                if (currentPos.position.Rank == 2)
                {
                    possibleMoves.Add(new BoardPosition(currentPos.position.Rank + 2, currentPos.position.file));
                }
            }

            // check diag space black piece
            if(false)
            {
                possibleMoves.Add(new BoardPosition(currentPos.position.Rank + 2, currentPos.position.file - 1));
            }
        }




        // not white


        return possibleMoves.ToArray();
    }
}
