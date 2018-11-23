using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class Piece : MonoBehaviour
{
	[SerializeField]
	private Color colorOfPiece;
	
	// Use this for initialization
	void Start()
	{
		gameObject.GetComponent<Renderer>().material.color = 
			new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
		colorOfPiece = GetComponent<Renderer>().material.color;
	}

	// Update is called once per frame
	void Update()
	{
	}
}
