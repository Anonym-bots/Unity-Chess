using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] protected bool selected;
    protected private GameMaster gm;

    [SerializeField] protected bool hasMoved;

    [SerializeField] protected int side;

    [SerializeField] protected GameObject selectionMark;

    public enum files
    {
        a,b,c,d,e,f,g,h
    }

    [System.Serializable]
    public class Position
    {
        public files file;
        public int row;
    }

    public Position position;


    protected virtual void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    protected virtual void OnMouseDown()
    {
        if(gm.turn != side){
            return;
        }

        if(selected == true)
        {
            Deselect();
            gm.selectedUnit = null;
        }
        else
        {
            if(gm.selectedUnit != null){
                gm.selectedUnit.Deselect();
            }
            Select();
            GetWalkableTiles();
        }
    }

    public void Select()
    {
        selectionMark.SetActive(true);
        selected = true;
        gm.selectedUnit = this;
    }

    public void Deselect()
    {
        selectionMark.SetActive(false);
        selected = false;
    }

    protected virtual void GetWalkableTiles()
    {
        
    }

    protected virtual void Move()
    {
        //transform.position = destination.position;
    }
}
