using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class Piece : MonoBehaviour
{
	[SerializeField] private Color colorOfPiece;

	[SerializeField] private int orderInTurn;

	private bool isTurnDone;

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
	}

	// Update is called once per frame
	void Update()
	{
	}

	void setColor()
	{
		gameObject.GetComponent<Renderer>().material.color =
			new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		colorOfPiece = GetComponent<Renderer>().material.color;
	}

	public void MakeAMove(BoardPiece _boardPiece)
	{
		transform.position = _boardPiece.getPos() + new Vector3(0,transform.localScale.y / 2, 0);

		if (transform.position == _boardPiece.getPos() + new Vector3(0, transform.localScale.y / 2, 0))
		{
			isTurnDone = true;
		}
			
	}

	
}
