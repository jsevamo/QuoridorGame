using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuoridorController : MonoBehaviour {

    public GameObject BoardPiece;
    public GameObject PlayPiece;
    int boardSize;
    //Placeholder to keep all board pieces.
    GameObject Board, Piece;
    ArrayList wholeBoard = new ArrayList();

    Vector3 side1Start, side2Start, side3Start, side4Start;
    public List<Vector3> sidesToPlacePiece = new List<Vector3>();


	// Use this for initialization
	void Start () {

        CreateBoard();
        setPlayPieces(2);
		
	}

    void CreateBoard()
    {
        Board = new GameObject();
        Board.name = "BoardContainer";


        side1Start = side2Start = side3Start = side4Start = new Vector3(-1, - 1, - 1);

        boardSize = 9;
        float dX = 0;
        float dZ = 0;
        float offset = 0.2f;

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                GameObject boardPiece = Instantiate(BoardPiece, new Vector3(dX, 0, dZ), Quaternion.identity) as GameObject;
                boardPiece.transform.parent = Board.gameObject.transform;
                Board b = boardPiece.GetComponent<Board>();
                b.setColumn(j);
                b.setRow(i+1);
                b.setPos(boardPiece.gameObject.transform.position);                

                if (i == 0 && j == 4)
                {
                    side1Start = boardPiece.gameObject.transform.position;
                    sidesToPlacePiece.Add(side1Start);
                }

                if (i == 4 && j == boardSize - 1)
                {
                    side2Start = boardPiece.gameObject.transform.position;
                    sidesToPlacePiece.Add(side2Start);
                }

                

                if(i == 4 && j == 0)
                {
                    side3Start = boardPiece.gameObject.transform.position;
                    sidesToPlacePiece.Add(side3Start);
                }

                if (i == boardSize - 1 && j == 4)
                {
                    side4Start = boardPiece.gameObject.transform.position;
                    sidesToPlacePiece.Add(side4Start);
                }



                //wholeBoard.Add(boardPiece.GetComponent<Board>().getBoardCoordinate().);
                dX = dX + BoardPiece.transform.localScale.x + offset;
            }
            dX = 0;
            dZ = dZ + BoardPiece.transform.localScale.z + offset;
        }

    }

    void setPlayPieces(int _numOfPlayers)
    {

        Piece = new GameObject();
        Piece.name = "PiecesContainer";

        if (_numOfPlayers <= 1 || _numOfPlayers > 4)
        {
            Debug.LogError("You can only have from 2 to 4 players");
            return;
        }

        for(int i = 0; i < _numOfPlayers; i++)
        {
            GameObject playPiece = Instantiate(PlayPiece, sidesToPlacePiece[i] + 
                new Vector3(0,0.5f,0), Quaternion.identity) as GameObject;
            playPiece.transform.parent = Piece.gameObject.transform;
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
