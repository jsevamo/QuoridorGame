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

	public int getOrderInTurn()
	{
		return orderInTurn;
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

	public void chooseMovingSpot()
	{
		transform.position += new Vector3(0.01f,0,0);
	}
}
