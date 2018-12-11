using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
	[SerializeField] private bool isBeingDragged;
	private bool shouldBeDeleted;
	
	
	[SerializeField]
	public enum OrientationEmun
	{
		Horizontal,
		Vertical
		
	}
	
	[SerializeField] private OrientationEmun orientation;

	public OrientationEmun Orientation
	{
		get { return orientation; }
		set { orientation = value; }
	}

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
		orientation = OrientationEmun.Horizontal;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(isBeingDragged)
		{
			AttachBlockerToCursor();
			setOrientation();
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

	public void setOrientation()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (orientation == OrientationEmun.Horizontal)
			{
				orientation = OrientationEmun.Vertical;
			}
			else
			{
				orientation = OrientationEmun.Horizontal;
			}
		}
		
		
		if (orientation == OrientationEmun.Horizontal)
		{
			transform.localRotation = Quaternion.Euler(0,0,0);
		}
		else if (orientation == OrientationEmun.Vertical)
		{
			transform.localRotation = Quaternion.Euler(0,90,0);
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
