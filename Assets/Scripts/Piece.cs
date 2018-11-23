using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class Piece : MonoBehaviour
{
	[SerializeField] private Color colorOfPiece;

	[SerializeField] private int orderInTurn;

	public void setOrderInTurn(int _orderInTurn)
	{
		orderInTurn = _orderInTurn;
	}


	// Use this for initialization
	void Start()
	{
		setColor();
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
}
