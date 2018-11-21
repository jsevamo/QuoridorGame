using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuoridorController : MonoBehaviour {

    public GameObject BoardPiece;
    int boardSize;
    GameObject Board;


	// Use this for initialization
	void Start () {

        CreateBoard();
        

		
	}

    void CreateBoard()
    {
        Board = new GameObject();
        Board.name = "BoardContainer";

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
                dX = dX + BoardPiece.transform.localScale.x + offset;
            }
            dX = 0;
            dZ = dZ + BoardPiece.transform.localScale.z + offset;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
