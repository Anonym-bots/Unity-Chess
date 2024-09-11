using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Rook : Piece
{
    public override Dictionary<(BoardPosition, BoardPosition), Move> GetPossibleMoves(Board board)
    {
        Dictionary<(BoardPosition, BoardPosition), Move> result = null;

        BoardPosition offset, endTile;
        for (int i = 0; i < ChessUtils.CardinalOffsets.Length; i++)
        {
            offset = ChessUtils.CardinalOffsets[i];
            endTile = currentPos + offset;

            while (endTile.IsValid())
            {
                Move testMove = new Move(currentPos, endTile);

                // if (Rules.MoveObeysRules(board, testMove, Owner))
                {
                    if (result == null)
                    {
                        result = new Dictionary<(BoardPosition, BoardPosition), Move>();
                    }

                    result[(testMove.start, testMove.end)] = new Move(testMove);
                }

                if (board.IsOccupiedAt(endTile))
                {
                    break;
                }

                endTile += offset;
            }
        }


        return result;
    }
}
