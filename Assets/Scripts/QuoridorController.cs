using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Boo.Lang.Runtime.DynamicDispatching;
using UnityEngine;
using UnityEngine.Analytics;
using UnityTemplateProjects;
using UnityEngine.UI;

public class QuoridorController : MonoBehaviour
{
    
    //TODO: FIX NUMBER OF STEPS BUG
    public GameObject BoardPiece;
    public GameObject PlayPiece;
    public GameObject BlockerPiece;
    public GameObject PlaceBlockerPiece;
    public int NumberOfPlayers;

    int boardSize;

    //Placeholder to keep all board pieces parented in the inspector.
    private GameObject Board, Piece, Blocker, BlockerPlace;

    public GameObject BlockerPlace1
    {
        get { return BlockerPlace; }
        set { BlockerPlace = value; }
    }

    Vector3 side1Start, side2Start, side3Start, side4Start;
    List<Vector3> sidesToPlacePiece = new List<Vector3>();
    List<Piece> piecesOnBoard = new List<Piece>();

    private bool isplaying;
    [SerializeField] private int actualTurn;

    [SerializeField] private Piece movablePiece;
    private Board board;
    private Camera cam;

    private int numberOfTurns;
    private GameObject centerPiece;

    [SerializeField] private Blocker actualBlocker;

    public Blocker ActualBlocker
    {
        get { return actualBlocker; }
        set { actualBlocker = value; }
    }

    public Text BlockerCountText;
    public Text PlayerTurnText;
    
    List<PlaceBlocker> placeBlockerList = new List<PlaceBlocker>();

    public List<PlaceBlocker> PlaceBlockerList
    {
        get { return placeBlockerList; }
        set { placeBlockerList = value; }
    }


    // Use this for initialization
    void Start()
    {
        CreateBoard();
        SetPlayPieces(NumberOfPlayers);
        ChooseOrder(NumberOfPlayers);
        numberOfTurns = 0;
        ChangeTurn();

        Debug.Log("THE GAME HAS STARTED :D");
        
       

    }

    void setBlockerCount()
    {
        BlockerCountText.text = "x " + movablePiece.NumOfBlockPieces;
    }

    void CreateBoard()
    {
        Board = new GameObject();
        Blocker = new GameObject();
        BlockerPlace = new GameObject();
        Board.name = "BoardContainer";
        Blocker.name = "BlockerContainer";
        BlockerPlace.name = "BlockerPlacementPiecesContainer";

        isplaying = true;
        actualTurn = 0;
        actualBlocker = null;

        board = new Board();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();


        side1Start = side2Start = side3Start = side4Start = new Vector3(-1, -1, -1);

        boardSize = 9;
        float dX = 0;
        float dZ = 0;
        float offset = 0.2f;
        
       

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                GameObject boardPiece =
                    Instantiate(BoardPiece, new Vector3(dX, 0, dZ), Quaternion.identity) as GameObject;
                boardPiece.transform.parent = Board.gameObject.transform;
                BoardPiece b = boardPiece.GetComponent<BoardPiece>();
                b.setColumn(j);
                b.setRow(i + 1);
                b.setPos(boardPiece.gameObject.transform.position);
                board.AddPiece(b);

                //Set starting pieces positions.

                if (i == 0 && j == 4)
                {
                    side1Start = boardPiece.gameObject.transform.position;
                }

                if (i == 4 && j == boardSize - 1)
                {
                    side2Start = boardPiece.gameObject.transform.position;
                }

                if (i == 4 && j == 0)
                {
                    side3Start = boardPiece.gameObject.transform.position;
                }

                if (i == boardSize - 1 && j == 4)
                {
                    side4Start = boardPiece.gameObject.transform.position;
                }

                if (j == 4 && i == 4)
                {
                    centerPiece = boardPiece.gameObject;
                }


                dX = dX + BoardPiece.transform.localScale.x + offset;
            }

            dX = 0;
            dZ = dZ + BoardPiece.transform.localScale.z + offset;
        }

        sidesToPlacePiece.Add(side1Start);
        sidesToPlacePiece.Add(side4Start);
        sidesToPlacePiece.Add(side2Start);
        sidesToPlacePiece.Add(side3Start);

        
        
    }

    void SetPlayPieces(int _numOfPlayers)
    {
        Piece = new GameObject();
        Piece.name = "PiecesContainer";

        if (_numOfPlayers <= 1 || _numOfPlayers > 4)
        {
            Debug.LogError("You can only have from 2 to 4 players");
            return;
        }

        for (int i = 0; i < _numOfPlayers; i++)
        {
            GameObject playPiece = Instantiate(PlayPiece, sidesToPlacePiece[i] +
                                                          new Vector3(0, 0.5f, 0),
                Quaternion.RotateTowards(transform.rotation, Quaternion.identity, 1)) as GameObject;
            playPiece.transform.parent = Piece.gameObject.transform;

            playPiece.transform.LookAt(centerPiece.transform.position + new Vector3(0, 0.5f, 0));


            piecesOnBoard.Add(playPiece.GetComponent<Piece>());
        }
    }

    void ChooseOrder(int _numOfPlayers)
    {
        //int chooseStarter = Random.Range(0, 2);
        int chooseStarter = 0;

        if (chooseStarter == 0)
        {
            int order = 1;
            foreach (var piece in piecesOnBoard)
            {
                piece.setOrderInTurn(order);
                order++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        StartGame();
        
    }

    private void ChangeTurn()
    {
        
        if (numberOfTurns > 0)
        {
            movablePiece.IsCurrentlyPlaying = false;
            movablePiece.CurrentBoardPiece = null;
        }

        if (actualTurn > NumberOfPlayers - 1) actualTurn = 0;


        actualTurn++;

        for (var i = 0; i < NumberOfPlayers; i++)
        {
            if (piecesOnBoard[i].getOrderInTurn() == actualTurn)
            {
                Debug.Log("Player's " + piecesOnBoard[i].getOrderInTurn() + " turn.");
                movablePiece = piecesOnBoard[i];
            }

           
            piecesOnBoard[i].IsTurnDone = false;

            
            
        }

        foreach (var _pieceOfBoard in board.BoardPieces)
        {
            _pieceOfBoard.HasActivePlayerOnTop = false;
            _pieceOfBoard.HasPlayerOnTop = false;
            _pieceOfBoard.PieceCanBeMovedHere = false;
        }

        numberOfTurns++;
    }

    void StartGame()
    {
        if (isplaying)
        {
            CheckIfWin();
            CheckWhoIsCurrentlyPlaying();
            CheckWhereIsPlayer();
            setPlayerTurnText();         
            setBlockerCount();
            

            if (actualBlocker)
            {
                findSpotToPlaceBlock();
                CheckIfBlockDeleted();
                BlockerPlace.SetActive(true);
            }
            else
            {
                WaitForPlayerMove();
                HighlightBoardPiece();
                BlockerPlace.SetActive(false);
            }
            

            if (movablePiece.IsTurnDone)
            {
                ChangeTurn();
            }
        }
    }

    void setPlayerTurnText()
    {
        if (isplaying)
        {
            PlayerTurnText.text = "Player's " + movablePiece.getOrderInTurn() + " Turn"; 
        }
        else
        {
            foreach (var _piece in piecesOnBoard)
            {
                if (_piece.NumPlaysForward == boardSize - 1)
                {

                    PlayerTurnText.text = "Player " + _piece.getOrderInTurn() + " wins!"; 
                
                }
            }
        }
        
    }

    void CheckIfBlockDeleted()
    {
        if (actualBlocker)
        {
            if (actualBlocker.ShouldBeDeleted)
            {
                Destroy(GameObject.FindWithTag("Blocker"));
                actualBlocker = null;
            }
        }
        
    }

    public void addBlockerPiece()
    {

        if (movablePiece.NumOfBlockPieces > 0)
        {
            GameObject blockerPiece= Instantiate(BlockerPiece, Vector3.zero, Quaternion.identity) as GameObject;
            blockerPiece.transform.parent = Blocker.gameObject.transform;

            actualBlocker = blockerPiece.GetComponent<Blocker>();

            actualBlocker.IsBeingDragged = true;
        }
        else
        {
            Debug.Log("Cannot add");
        }
        

    }

    public void findSpotToPlaceBlock()
    {

        if (movablePiece.NumOfBlockPieces > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    return;
                }

                if (hit.transform.gameObject.tag == "PlaceBlocker")
                {
                    PlaceBlocker blockerPlace = hit.transform.GetComponent<PlaceBlocker>();                   
                    actualBlocker.PlaceBlockerOnBoard(hit.transform.gameObject);  
                    movablePiece.IsTurnDone = true;
                    blockerPlace.setBlocker(actualBlocker);
                    actualBlocker = null;
                    movablePiece.NumOfBlockPieces--;
                    
                    blockerPlace.HasBlocker = true;

                    blockerPlace.Bp1.HasSurroundingBlocker = true;
                    blockerPlace.Bp2.HasSurroundingBlocker = true;
                    blockerPlace.Bp3.HasSurroundingBlocker = true;
                    blockerPlace.Bp4.HasSurroundingBlocker = true;

                }

            }
        }

    }



    void CheckIfWin()
    {
        foreach (var _piece in piecesOnBoard)
        {
            if (_piece.NumPlaysForward == boardSize - 1)
            {
                isplaying = false;
                Debug.Log("Player " + _piece.getOrderInTurn() + " has won the game!");   
            }
        }
        
        
    }


    void CheckWhoIsCurrentlyPlaying()
    {
        if (movablePiece.getOrderInTurn() == actualTurn)
        {
            movablePiece.IsCurrentlyPlaying = true;
        }
    }

    void CheckWhereIsPlayer()
    {
        for (int i = 0; i < NumberOfPlayers; i++)
        {
            foreach (var _boardPiece in board.BoardPieces)
            {
                _boardPiece.checkIfActivePlayerOnTop(movablePiece);
            }

            foreach (var _boardPiece in board.BoardPieces)
            {
                _boardPiece.checkIfAnyPlayerOnTop(piecesOnBoard[i]);
            }
        }
        
        
        

        for (int i = 0; i < board.GetNumberOfPieces(); i++)
        {
            if (board.BoardPieces[i].HasActivePlayerOnTop)
            {
                movablePiece.CurrentBoardPiece = board.BoardPieces[i];
            }
        }
    }

    void WaitForPlayerMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (!Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit))
            {
                return;
            }

            if (hit.transform.gameObject.tag == "Board_Segment")
            {
                BoardPiece _boardPiece = hit.transform.GetComponent<BoardPiece>();
                
                if (!_boardPiece.HasPlayerOnTop)
                {
                    movablePiece.MakeAMove(_boardPiece, movablePiece.transform.forward);
                }
               
            }
       
        }
    }

    void HighlightBoardPiece()
    {
        foreach (var _boardPiece in board.BoardPieces)
        {
            if (_boardPiece.HasActivePlayerOnTop)
            {
                if (_boardPiece.FrontBoard != null)
                {
                    if (!_boardPiece.HasSurroundingBlocker)
                    {
                        if (!_boardPiece.FrontBoard.HasPlayerOnTop)
                        {
                            _boardPiece.FrontBoard.PieceCanBeMovedHere = true;
                        }
                        else
                        {
                            _boardPiece.FrontFrontBoard.PieceCanBeMovedHere = true;
                        }
                    }
                    else if(_boardPiece.HasSurroundingBlocker)
                    {
                        RaycastHit hit2;
                        if (Physics.Raycast(movablePiece.transform.position, 
                            transform.TransformDirection(Vector3.forward), 
                            out hit2,1))
                        {

                            _boardPiece.FrontBoard.PieceCanBeMovedHere = false;
                        }
                        else
                        {
                            
                            if (_boardPiece.FrontBoard.HasPlayerOnTop)
                            {
                                _boardPiece.FrontFrontBoard.PieceCanBeMovedHere = true;
                            }
                            else
                            {
                                _boardPiece.FrontBoard.PieceCanBeMovedHere = true;
                            }
                        }
                        
                    }
   
                }

                
                

                if (_boardPiece.RightBoard != null)
                {
                    if (!_boardPiece.HasSurroundingBlocker)
                    {
                        if (!_boardPiece.RightBoard.HasPlayerOnTop)
                        {
                            _boardPiece.RightBoard.PieceCanBeMovedHere = true;
                        }
                        else
                        {
                            _boardPiece.RightRightBoard.PieceCanBeMovedHere = true;
                        }
                        
                    }
                    else
                    {
                        RaycastHit hit2;
                        if (Physics.Raycast(movablePiece.transform.position, 
                            transform.TransformDirection(Vector3.right), 
                            out hit2,1))
                        {

                            _boardPiece.RightBoard.PieceCanBeMovedHere = false;
                        }
                        else
                        {
                            if (_boardPiece.RightBoard.HasPlayerOnTop)
                            {
                                _boardPiece.RightRightBoard.PieceCanBeMovedHere = true;
                            }
                            else
                            {
                                _boardPiece.RightBoard.PieceCanBeMovedHere = true;
                            }
                        }
                    }
                    
                }

                if (_boardPiece.LeftBoard != null)
                {
                    if (!_boardPiece.HasSurroundingBlocker)
                    {
                        if (!_boardPiece.LeftBoard.HasPlayerOnTop)
                        {
                            _boardPiece.LeftBoard.PieceCanBeMovedHere = true;
                        }
                        else
                        {
                            _boardPiece.LeftLeftBoard.PieceCanBeMovedHere = true;
                        }
                       
                    }
                    else
                    {
                        RaycastHit hit2;
                        if (Physics.Raycast(movablePiece.transform.position, 
                            transform.TransformDirection(Vector3.left), 
                            out hit2,1))
                        {

                            _boardPiece.LeftBoard.PieceCanBeMovedHere = false;
                        }
                        else
                        {
                            if (_boardPiece.LeftBoard.HasPlayerOnTop)
                            {
                                _boardPiece.LeftLeftBoard.PieceCanBeMovedHere = true;
                            }
                            else
                            {
                                _boardPiece.LeftBoard.PieceCanBeMovedHere = true;
                            }
                        }
                    }
                    
                }

                if (_boardPiece.BackBoard != null)
                {

                    if (!_boardPiece.HasSurroundingBlocker)
                    {
                        if (!_boardPiece.BackBoard.HasPlayerOnTop)
                        {
                            _boardPiece.BackBoard.PieceCanBeMovedHere = true;
                        }
                        else
                        {
                            _boardPiece.BackBackBoard.PieceCanBeMovedHere = true;
                        }
                        
                    }
                    else
                    {
                        RaycastHit hit2;
                        if (Physics.Raycast(movablePiece.transform.position, 
                            transform.TransformDirection(Vector3.back), 
                            out hit2,1))
                        {

                            _boardPiece.BackBoard.PieceCanBeMovedHere = false;
                        }
                        else
                        {
                            if (_boardPiece.BackBoard.HasPlayerOnTop)
                            {
                                _boardPiece.BackBackBoard.PieceCanBeMovedHere = true;
                            }
                            else
                            {
                                _boardPiece.BackBoard.PieceCanBeMovedHere = true;
                            }
                        }
                    }
                    
         
                }
            }
        }
        
       
        int layerMask =  LayerMask.GetMask("BoardPieces");

        RaycastHit hit;

        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit , Mathf.Infinity, layerMask))

        {
            BoardPiece _boardPiece = hit.transform.GetComponent<BoardPiece>();

            if (!cam.GetComponent<SimpleCameraController>().IsCameraMoving)
            {
                _boardPiece.setHighlight(true, movablePiece, board);
            }

            
        }
        else
        {
            foreach (var boardPiece in board.BoardPieces)
            {
                boardPiece.setHighlight(false, movablePiece, board);
            }
        }
    }
}