using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece : MonoBehaviour
{
    [SerializeField] BoardCoordinate boardCoordinate;
    public GameObject PlaceBlockerPiece;

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


    [SerializeField] private BoardPiece frontFrontBoard;

    public BoardPiece FrontFrontBoard
    {
        get { return frontFrontBoard; }
        set { frontFrontBoard = value; }
    }

    public BoardPiece RightRightBoard
    {
        get { return rightRightBoard; }
        set { rightRightBoard = value; }
    }

    public BoardPiece LeftLeftBoard
    {
        get { return leftLeftBoard; }
        set { leftLeftBoard = value; }
    }

    public BoardPiece BackBackBoard
    {
        get { return backBackBoard; }
        set { backBackBoard = value; }
    }

    [SerializeField] private BoardPiece rightRightBoard;
    [SerializeField] private BoardPiece leftLeftBoard;
    [SerializeField] private BoardPiece backBackBoard;
    

    [SerializeField] private bool hasActivePlayerOnTop;
    [SerializeField] private bool hasPlayerOnTop;

    public bool HasPlayerOnTop
    {
        get { return hasPlayerOnTop; }
        set { hasPlayerOnTop = value; }
    }


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
        hasPlayerOnTop = false;
        pieceCanBeMovedHere = false;


        FindNeighbors();
        setBlockerPlacePiece();


    }

    public void setBlockerPlacePiece()
    {

        if (rightBoard && frontBoard)
        {
            GameObject boardPlacePiece = Instantiate(PlaceBlockerPiece, transform.position + 
            new Vector3(transform.localScale.x/2 + 0.1f, 0, transform.localScale.z/2+ 0.1f), Quaternion.identity)
                as GameObject;

            QuoridorController QC = GameObject.FindWithTag("GameController").GetComponent<QuoridorController>();

            boardPlacePiece.transform.parent = QC.BlockerPlace1.gameObject.transform;
            QC.PlaceBlockerList.Add(boardPlacePiece.GetComponent<PlaceBlocker>());
            
        }
        
        
            

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
    
    public void checkIfAnyPlayerOnTop(Piece _piece)
    {
        if (Mathf.Approximately(transform.position.x, _piece.gameObject.transform.position.x) &&
            Mathf.Approximately(transform.position.z, _piece.gameObject.transform.position.z))
        {
            hasPlayerOnTop = true;
        }
    }
    
    

    void FindNeighbors()
    {
        RaycastHit hit;
        GameObject frontBoard2 = null;
        GameObject rightBoard2 = null;
        GameObject leftBoard2 = null;
        GameObject backBoard2 = null;
        

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            frontBoard = hit.transform.gameObject.GetComponent<BoardPiece>();
            frontBoard2 = hit.transform.gameObject;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit))
        {
            rightBoard = hit.transform.gameObject.GetComponent<BoardPiece>();
            rightBoard2 = hit.transform.gameObject;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit))
        {
            leftBoard = hit.transform.gameObject.GetComponent<BoardPiece>();
            leftBoard2 = hit.transform.gameObject;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit))
        {
            backBoard = hit.transform.gameObject.GetComponent<BoardPiece>();
            backBoard2 = hit.transform.gameObject;
        }
        
        //-----------------------------------------------------------
        
        //TODO: FIX THE BOARD2'S POSITION ON THE IFS
        if (frontBoard != null)
        {
            if (Physics.Raycast(frontBoard2.transform.position, 
                transform.TransformDirection(Vector3.forward), out hit))
            {
                frontFrontBoard = hit.transform.gameObject.GetComponent<BoardPiece>();
            }
        }

        if (rightBoard == null) return;
        if (Physics.Raycast(rightBoard2.transform.position, 
            transform.TransformDirection(Vector3.right), out hit))
        {
            rightRightBoard = hit.transform.gameObject.GetComponent<BoardPiece>();
        }

        if (leftBoard == null) return;
        if (Physics.Raycast(leftBoard2.transform.position, 
            transform.TransformDirection(Vector3.left), out hit))
        {
            leftLeftBoard = hit.transform.gameObject.GetComponent<BoardPiece>();
        }

        if (backBoard == null) return;
        if (Physics.Raycast(backBoard2.transform.position, 
            transform.TransformDirection(Vector3.back), out hit))
        {
            backBackBoard = hit.transform.gameObject.GetComponent<BoardPiece>();
        }


        /*if (Physics.Raycast(rightBoard.transform.position, 
            rightBoard.transform.TransformDirection(Vector3.right), out hit))
        {
            rightRightBoard = hit.transform.gameObject.GetComponent<BoardPiece>();
        }
        
        if (Physics.Raycast(leftBoard.transform.position, 
            leftBoard.transform.TransformDirection(Vector3.left), out hit))
        {
            leftLeftBoard = hit.transform.gameObject.GetComponent<BoardPiece>();
        }
        
        if (Physics.Raycast(backBoard.transform.position, 
            backBoard.transform.TransformDirection(Vector3.back), out hit))
        {
            backBackBoard = hit.transform.gameObject.GetComponent<BoardPiece>();
        }*/
        
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

