﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
	[SerializeField] private bool isBeingDragged;
	private bool shouldBeDeleted;

	public bool ShouldBeDeleted
	{
		get { return shouldBeDeleted; }
		set { shouldBeDeleted = value; }
	}

	public bool IsBeingDragged
	{
		get { return isBeingDragged; }
		set { isBeingDragged = value; }
	}

	// Use this for initialization
	void Awake ()
	{
		isBeingDragged = false;
		shouldBeDeleted = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(isBeingDragged)
		{
			AttachBlockerToCursor();
		}
		else
		{
			if (shouldBeDeleted)
			{
				DestroyBlocker();
			}

		}
			
		//TODO: CHANGE THIS KEY TO ESCAPE TO DELETE BLOCKER
		if (Input.GetKeyDown(KeyCode.P))
		{
			isBeingDragged = false;
			shouldBeDeleted = true;
		}
		
	}

	public void AttachBlockerToCursor()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 rayPoint = ray.GetPoint(10);

		transform.position = rayPoint;
	}

	public void DestroyBlocker()
	{
		Destroy(this);
	}

	public void PlaceBlockerOnBoard(GameObject placeHere)
	{
		isBeingDragged = false;

		transform.position = placeHere.transform.position + new Vector3(0,transform.localScale.y/2, 0);
	}
}
