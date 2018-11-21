using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    BoardCoordinate boardCoordinate;

	// Use this for initialization
	void Start () {
        boardCoordinate = new BoardCoordinate(8, 1);
        Debug.Log(boardCoordinate.getColumn());
        Debug.Log(boardCoordinate.getRow());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public class BoardCoordinate
{
    

    public enum ColumnEmun
    {
        a,
        b,
        c,
        d,
        e,
        f,
        g,
        h,
        i
    }

    int row;
    ColumnEmun column;

    public BoardCoordinate(int _column, int _row)
    {
        row = _row;
        column = (ColumnEmun)_column;
    }

    public int getRow()
    {
        return row;
    }

    public ColumnEmun getColumn()
    {
        return column;
    }

  
}
