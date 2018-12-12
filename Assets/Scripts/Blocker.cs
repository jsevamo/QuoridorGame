using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour
{
	[SerializeField] private bool isBeingDragged;
	[SerializeField] private bool hasBeenPlaced;
	
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



	public bool IsBeingDragged
	{
		get { return isBeingDragged; }
		set { isBeingDragged = value; }
	}

	// Use this for initialization
	void Awake ()
	{
		isBeingDragged = false;
		hasBeenPlaced = false;
		orientation = OrientationEmun.Horizontal;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(isBeingDragged)
		{
			AttachBlockerToCursor();
			setOrientation();
		}
			
		if (Input.GetKeyDown(KeyCode.P))
		{
			if (!hasBeenPlaced)
			{
				Destroy(this);
				Destroy(this.gameObject);
			}
			

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


	public void PlaceBlockerOnBoard(GameObject placeHere)
	{
		isBeingDragged = false;

		transform.position = placeHere.transform.position + new Vector3(0,transform.localScale.y/2, 0);

		GetComponent<BoxCollider>().enabled = true;

		hasBeenPlaced = true;
	}
}