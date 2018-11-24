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

	public List<BoardPiece> GetCurrentBoardPiece()
	{
		List<BoardPiece> BPList = new List<BoardPiece>();

		foreach (var _boardPiece in boardPieces)
		{
			if (_boardPiece.HasActivePlayerOnTop)
			{
				BPList.Add((_boardPiece));
			}
		}

		return BPList;
	}
}
