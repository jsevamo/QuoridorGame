using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour {

    [SerializeField]
    public BoardCoordinate boardCoordinate;
    
    int column;
    int row;
    Vector3 pos;

    public void setColumn(int _column)
    {
        column = _column;
    }

    public void setRow(int _row)
    {
        row = _row;
    }

    public void setPos(Vector3 _pos)
    {
        pos = _pos;
    }

    public Vector3 getPos()
    {
        return pos;
    }
    

	// Use this for initialization
	void Start () {
        boardCoordinate = new BoardCoordinate(column, row, pos);
        //Debug.Log(boardCoordinate.getPos());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 getBoardPiecePos()
    {
        return boardCoordinate.getPos();
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


