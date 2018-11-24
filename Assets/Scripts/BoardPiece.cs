using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    [SerializeField] BoardCoordinate boardCoordinate;

    int column;
    int row;
    Vector3 pos;
    private Color col;

    [SerializeField] private bool isHighlighted;
    [SerializeField] private bool pieceCanBeMovedHere;

    public bool PieceCanBeMovedHere
    {
        get { return pieceCanBeMovedHere; }
        set { pieceCanBeMovedHere = value; }
    }

    [SerializeField] private BoardPiece frontBoard;

    public BoardPiece FrontBoard
    {
        get { return frontBoard; }
        set { frontBoard = value; }
    }

    public BoardPiece RightBoard
    {
        get { return rightBoard; }
        set { rightBoard = value; }
    }

    public BoardPiece LeftBoard
    {
        get { return leftBoard; }
        set { leftBoard = value; }
    }

    public BoardPiece BackBoard
    {
        get { return backBoard; }
        set { backBoard = value; }
    }

    [SerializeField] private BoardPiece rightBoard;
    [SerializeField] private BoardPiece leftBoard;
    [SerializeField] private BoardPiece backBoard;

    [SerializeField] private bool hasActivePlayerOnTop;

    public bool HasActivePlayerOnTop
    {
        get { return hasActivePlayerOnTop; }
        set { hasActivePlayerOnTop = value; }
    }


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
    void Start()
    {
        boardCoordinate = new BoardCoordinate(column, row, pos);
        isHighlighted = false;
        col = gameObject.GetComponent<Renderer>().material.color;
        frontBoard = leftBoard = rightBoard = backBoard = null;
        hasActivePlayerOnTop = false;
        pieceCanBeMovedHere = false;

        

        FindNeighbors();
        

        //Debug.Log(boardCoordinate.getPos());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkIfActivePlayerOnTop(Piece _piece)
    {
        

        if (Mathf.Approximately(transform.position.x, _piece.gameObject.transform.position.x) &&
            Mathf.Approximately(transform.position.z, _piece.gameObject.transform.position.z))
        {
            hasActivePlayerOnTop = true;
        }
       
        
    }

    void FindNeighbors()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            frontBoard = hit.transform.gameObject.GetComponent<BoardPiece>();
        }
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit))
        {
            rightBoard = hit.transform.gameObject.GetComponent<BoardPiece>();
        }
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit))
        {
            leftBoard = hit.transform.gameObject.GetComponent<BoardPiece>();
        }
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit))
        {
            backBoard = hit.transform.gameObject.GetComponent<BoardPiece>();
        }
        
    }

    public Vector3 getBoardPiecePos()
    {
        return boardCoordinate.getPos();
    }

    public void setHighlight(bool _isHighlighted, Piece _piece, Board _board)
    {
        isHighlighted = _isHighlighted;
        
        
        

        if (isHighlighted)
        {
            if (transform.position.x == _piece.transform.position.x &&
                transform.position.z == _piece.transform.position.z)
            {
                gameObject.GetComponent<Renderer>().material.color = col;
            }

            else
            {
                if (pieceCanBeMovedHere)
                {
                    gameObject.GetComponent<Renderer>().material.color = new Color(0, 0.74f, 0);
                }
                
            }
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = col;
        }
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

    [SerializeField] int row;
    [SerializeField] ColumnEmun column;
    [SerializeField] Vector3 pos;

    public BoardCoordinate(int _column, int _row, Vector3 _pos)
    {
        row = _row;
        column = (ColumnEmun) _column;
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


