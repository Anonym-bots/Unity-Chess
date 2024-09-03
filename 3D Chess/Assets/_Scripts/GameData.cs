using System;

[Serializable]
public class GameData
{
    public static GameData NormalStartingData = new GameData(
        sideToMove: Side.White,
        whiteCanCastleKingside: true,
        whiteCanCastleQueenside: true,
        blackCanCastleKingside: true,
        blackCanCastleQueenside: true,
        turnNumber: 1
    );

    public Side SideToMove;
    public bool WhiteCanCastleKingside;
    public bool WhiteCanCastleQueenside;
    public bool BlackCanCastleKingside;
    public bool BlackCanCastleQueenside;
    public int TurnNumber;

    public GameData(
        Side sideToMove,
        bool whiteCanCastleKingside,
        bool whiteCanCastleQueenside,
        bool blackCanCastleKingside,
        bool blackCanCastleQueenside,
        int turnNumber
    )
    {
        SideToMove = sideToMove;
        WhiteCanCastleKingside = whiteCanCastleKingside;
        WhiteCanCastleQueenside = whiteCanCastleQueenside;
        BlackCanCastleKingside = blackCanCastleKingside;
        BlackCanCastleQueenside = blackCanCastleQueenside;
        TurnNumber = turnNumber;
    }
}
