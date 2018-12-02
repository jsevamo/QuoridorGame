using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class Piece : MonoBehaviour
{
	[SerializeField] private Color colorOfPiece;

	[SerializeField] private int orderInTurn;

	private bool isTurnDone;
	[SerializeField] private bool isCurrentlyPlaying;
	[SerializeField] private bool hasWon;
	[SerializeField] private Vector3 forwardVector;
	private int numPlaysForward;
	[SerializeField] private int numOfBlockPieces;

	public int NumOfBlockPieces
	{
		get { return numOfBlockPieces; }
		set { numOfBlockPieces = value; }
	}

	public int NumPlaysForward
	{
		get { return numPlaysForward; }
		set { numPlaysForward = value; }
	}

	[SerializeField] private BoardPiece currentBoardPiece;

	public BoardPiece CurrentBoardPiece
	{
		get { return currentBoardPiece; }
		set { currentBoardPiece = value; }
	}

	public bool IsCurrentlyPlaying
	{
		get { return isCurrentlyPlaying; }
		set { isCurrentlyPlaying = value; }
	}

	public bool IsTurnDone
	{
		get { return isTurnDone; }
		set { isTurnDone = value; }
	}

	public void setOrderInTurn(int _orderInTurn)
	{
		orderInTurn = _orderInTurn;
	}

	public int getOrderInTurn()
	{
		return orderInTurn;
	}

	// Use this for initialization
	void Start()
	{
		setColor();
		isTurnDone = false;
		isCurrentlyPlaying = false;
		hasWon = false;
        setBlockerPieces();

		
	}

	// Update is called once per frame
	void Update()
	{
		forwardVector = transform.forward;
	}

	void setBlockerPieces()
	{
		//TODO: QC.NUMBER OF PLAYERS IS PUBLIC AT THE MOMENT.
		
		QuoridorController QC = GameObject.FindWithTag("GameController").GetComponent<QuoridorController>();

		if (QC.NumberOfPlayers == 2)
		{
			numOfBlockPieces = 10;
		}
		else if (QC.NumberOfPlayers == 4)
		{
			numOfBlockPieces = 5;
		}
	}

	void setColor()
	{
		gameObject.GetComponent<Renderer>().material.color =
			new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		colorOfPiece = GetComponent<Renderer>().material.color;
	}

	public void MakeAMove(BoardPiece _selectedBoardPiece, Vector3 _forwardV)
	{
		if (_selectedBoardPiece.PieceCanBeMovedHere)
		{
			transform.position = _selectedBoardPiece.getPos() + new Vector3(0, transform.localScale.y / 2, 0);
		}

		if (transform.position == _selectedBoardPiece.getPos() + new Vector3(0, transform.localScale.y / 2, 0))
		{
			isTurnDone = true;
		}

		//-----------------------------------------------------------

		BoardPiece boardForward = null;
		BoardPiece boardForwardTwice = null;
		BoardPiece boardBackWard = null;
		BoardPiece boardBackWardTwice = null;

		if (Mathf.Approximately(Vector3.Dot(_forwardV, Vector3.forward), 1))
		{
			boardForward = currentBoardPiece.FrontBoard;
			boardForwardTwice = currentBoardPiece.FrontFrontBoard;
			boardBackWard = currentBoardPiece.BackBoard;
			boardBackWardTwice = currentBoardPiece.BackBackBoard;
		}
		else if (Mathf.Approximately(Vector3.Dot(_forwardV, Vector3.back), 1))
		{
			boardForward = currentBoardPiece.BackBoard;
			boardForwardTwice = currentBoardPiece.BackBackBoard;
			boardBackWard = currentBoardPiece.FrontBoard;
			boardBackWardTwice = currentBoardPiece.FrontFrontBoard;
		}
		else if (Mathf.Approximately(Vector3.Dot(_forwardV, Vector3.right), 1))
		{
			boardForward = currentBoardPiece.RightBoard;
			boardForwardTwice = currentBoardPiece.RightRightBoard;
			boardBackWard = currentBoardPiece.LeftBoard;
			boardBackWardTwice = currentBoardPiece.LeftLeftBoard;
		}
		else if (Mathf.Approximately(Vector3.Dot(_forwardV, Vector3.left), 1))
		{
			boardForward = currentBoardPiece.LeftBoard;
			boardForwardTwice = currentBoardPiece.LeftLeftBoard;
			boardBackWard = currentBoardPiece.RightBoard;
			boardBackWardTwice = currentBoardPiece.RightRightBoard;
		}

		if (_selectedBoardPiece == boardForward)
		{
			numPlaysForward++;
		}

		else if (_selectedBoardPiece == boardForwardTwice)
		{
			numPlaysForward = numPlaysForward + 2;
		}

		else if (_selectedBoardPiece == boardBackWard)
		{
			numPlaysForward--;
		}
		
		else if (_selectedBoardPiece == boardBackWardTwice)
		{
			numPlaysForward = numPlaysForward - 2;
		}
	}
}
