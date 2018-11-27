using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board {
	
	private List<BoardPiece> boardPieces = new List<BoardPiece>();


	public List<BoardPiece> BoardPieces
	{
		get { return boardPieces; }
		set { boardPieces = value; }
	}


	public void AddPiece(BoardPiece _boardPiece)
	{
		boardPieces.Add(_boardPiece);
	}

	public int GetNumberOfPieces()
	{
		return boardPieces.Count();
	}

}
