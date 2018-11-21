using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    [SerializeField]
    BoardCoordinate boardCoordinate;
    
    int column;
    int row;

    public void setColumn(int _column)
    {
        column = _column;
    }

    public void setRow(int _row)
    {
        row = _row;
    }

	// Use this for initialization
	void Start () {
        boardCoordinate = new BoardCoordinate(column, row, transform.position);
        Debug.Log(boardCoordinate.getColumn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[System.Serializable]
public class BoardCoordinate
{
    [SerializeField]
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

    [SerializeField]
    int row;
    [SerializeField]
    ColumnEmun column;
    [SerializeField]
    Vector3 pos;

    public BoardCoordinate(int _column, int _row, Vector3 _pos)
    {
        row = _row;
        column = (ColumnEmun)_column;
        pos = _pos;

        
    }

    public int getRow()
    {
        return row;
    }

    public ColumnEmun getColumn()
    {
        return column;
       
    }

    public Vector3 getPos()
    {
        return pos;
    }

  
}


