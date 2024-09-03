using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum File
{
    a, b, c, d, e, f, g, h
}

[System.Serializable]
public class PiecePosition
{
    [Range(1, 8)] public int row;
    public File file;
}

public class GameMaster : MonoBehaviour
{
    public Unit selectedUnit;
    public int turn;

    public GameObject[] tiles;

    public Vector2 boardOffset  = new Vector2(-4.5f,-4.5f);

    public Transform pieceParent;



    public class Piece
    {
        public File file;
        public int row;
    }

    [System.Serializable]
    public class PieceList
    {
        public List<Piece> pawns;
        public List<Piece> knights;
        public List<Piece> bishops;
        public List<Piece> rooks;
        public List<Piece> queens;
        public Piece king;
    }

    
    [Header("Prefabs")]
    public GameObject pawnPrefab;
    public GameObject kingPrefab;
    public GameObject queenPrefab;
    public GameObject rookPrefab;
    public GameObject knightPrefab;
    public GameObject bishopPrefab;
    

    public PieceList playerWhite; // = new GameObject[32]
    public PieceList playerBlack; // = new GameObject[32]

    private List<GameObject> pieces = new List<GameObject>();


    void Start()
    {
        SetupBoard();
        SetupPieces();
    }

    public void SwitchTurns ()
    {
        if(turn == 1) {
            turn = 2;
        }
        else {
            turn = 1;
        }
    }

    public void SetupBoard ()
    {
        int file = 1;
        int row = 1;

        foreach(GameObject tile in tiles)
        {
            tile.transform.position = new Vector2(file, row) + boardOffset;

            file++;
            if(file > 8)
            {
                file = 1;
                row++;
            }
        }
    }

    public void SetupPieces ()
    {
        Vector2 position = new Vector2(0,0);

        //White's pieces
        foreach(Piece piece in playerWhite.pawns)
        {
            position = SetCoords(piece);
            pieces.Add(Instantiate(pawnPrefab, position, Quaternion.identity, pieceParent));
        }
        foreach(Piece piece in playerWhite.knights)
        {
            position = SetCoords(piece);
            pieces.Add(Instantiate(knightPrefab, position, Quaternion.identity, pieceParent));
        }
        foreach(Piece piece in playerWhite.bishops)
        {
            position = SetCoords(piece);
            pieces.Add(Instantiate(bishopPrefab, position, Quaternion.identity, pieceParent));
        }
        foreach(Piece piece in playerWhite.rooks)
        {
            position = SetCoords(piece);
            pieces.Add(Instantiate(rookPrefab, position, Quaternion.identity, pieceParent));
        }
        foreach(Piece piece in playerWhite.queens)
        {
            position = SetCoords(piece);
            pieces.Add(Instantiate(queenPrefab, position, Quaternion.identity, pieceParent));
        }
        
        position = SetCoords(playerWhite.king);
        pieces.Add(Instantiate(kingPrefab, position, Quaternion.identity, pieceParent));

        //Black's pieces
        foreach(Piece piece in playerBlack.pawns)
        {
            position = SetCoords(piece);
            pieces.Add(Instantiate(pawnPrefab, position, Quaternion.identity, pieceParent));
        }
        foreach(Piece piece in playerBlack.knights)
        {
            position = SetCoords(piece);
            pieces.Add(Instantiate(knightPrefab, position, Quaternion.identity, pieceParent));
        }
        foreach(Piece piece in playerBlack.bishops)
        {
            position = SetCoords(piece);
            pieces.Add(Instantiate(bishopPrefab, position, Quaternion.identity, pieceParent));
        }
        foreach(Piece piece in playerBlack.rooks)
        {
            position = SetCoords(piece);
            pieces.Add(Instantiate(rookPrefab, position, Quaternion.identity, pieceParent));
        }
        foreach(Piece piece in playerBlack.queens)
        {
            position = SetCoords(piece);
            pieces.Add(Instantiate(queenPrefab, position, Quaternion.identity, pieceParent));
        }
        
        position = SetCoords(playerBlack.king);
        pieces.Add(Instantiate(kingPrefab, position, Quaternion.identity, pieceParent));
        
    }

    public GameObject CreatePiece(int side, Piece pos)
    {
        GameObject obj = Instantiate(pawnPrefab, new Vector2(0,0), Quaternion.identity, pieceParent);
        Unit unit = obj.GetComponent<Unit>();
        unit.side = side;
        unit.transform.position = SetCoords(pos);

        return obj;
    }

    public Vector2 SetCoords(Piece _piece)
    {
        int x = 1;
        int y = 1;
        if(_piece.row > 8){
            y = 8;
        }
        else if(_piece.row < 1){
            y = 1;
        }
        else {
            y = _piece.row;
        }

        switch (_piece.file)
        {
            case File.a:
                x = 1;
                break;
            case File.b:
                x = 2;
                break;
            case File.c:
                x = 3;
                break;
            case File.d:
                x = 4;
                break;
            case File.e:
                x = 5;
                break;
            case File.f:
                x = 6;
                break;
            case File.g:
                x = 7;
                break;
            case File.h:
                x = 8;
                break;
        }

        Vector2 result = new Vector2(x,y) + boardOffset;
        return result;
    }
}
