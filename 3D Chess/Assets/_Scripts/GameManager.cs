using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public BoardSetupManager boardSetupManager;

    public GameData gameData;

    [Header("Active Board")]
    public BoardSetup activeBoard;


    protected override void Awake()
    {
	    base.Awake();
        
        gameData = GameData.NormalStartingData;
    }

    private void Start()
    {
        boardSetupManager.SpawnPieces(activeBoard);
    }


    public void MakeMove()
    {


        // switch turnPlayer
    }
}
