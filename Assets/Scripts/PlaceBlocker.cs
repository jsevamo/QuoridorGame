using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class PlaceBlocker : MonoBehaviour
{
	[SerializeField] private bool hasBlocker;
	[SerializeField] private Blocker blocker;
	private QuoridorController QC;

	public void setBlocker(Blocker B)
	{
		blocker = B;
	}

	[SerializeField] private PlaceBlocker frontBlocker;
    [SerializeField] private PlaceBlocker rightBlocker;
    [SerializeField] private PlaceBlocker leftBlocker;
    [SerializeField] private PlaceBlocker backBlocker;
	[SerializeField] private PlaceBlocker frontRightBlocker;
	[SerializeField] private PlaceBlocker frontLeftBlocker;
	[SerializeField] private PlaceBlocker backRightBlocker;
	[SerializeField] private PlaceBlocker backLefttBlocker;

	[SerializeField] private BoardPiece BP1;

	public BoardPiece Bp1
	{
		get { return BP1; }
		set { BP1 = value; }
	}

	public BoardPiece Bp2
	{
		get { return BP2; }
		set { BP2 = value; }
	}

	public BoardPiece Bp3
	{
		get { return BP3; }
		set { BP3 = value; }
	}

	public BoardPiece Bp4
	{
		get { return BP4; }
		set { BP4 = value; }
	}

	[SerializeField] private BoardPiece BP2;
	[SerializeField] private BoardPiece BP3;
	[SerializeField] private BoardPiece BP4;

	private bool hasCheckedBoardPieces;

	public bool HasBlocker
	{
		get { return hasBlocker; }
		set { hasBlocker = value; }
	}

	// Use this for initialization
	void Start ()
	{
		hasBlocker = false;
		blocker = null;
		QC = GameObject.FindWithTag("GameController").GetComponent<QuoridorController>();
		FindNeighbors();
		BP1 = null;
		BP2 = null;
		BP3 = null;
		BP4 = null;
		hasCheckedBoardPieces = false;
	}
	
	void FindNeighbors()
    {
        RaycastHit hit;
  
        

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            frontBlocker = hit.transform.gameObject.GetComponent<PlaceBlocker>();
           
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit))
        {
            rightBlocker = hit.transform.gameObject.GetComponent<PlaceBlocker>();
            
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit))
        {
            leftBlocker = hit.transform.gameObject.GetComponent<PlaceBlocker>();
          
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out hit))
        {
            backBlocker = hit.transform.gameObject.GetComponent<PlaceBlocker>();

        }


	    if (rightBlocker)
	    {
		    if (Physics.Raycast(rightBlocker.transform.position, transform.TransformDirection(Vector3.forward), out hit))
		    {
			    frontRightBlocker = hit.transform.gameObject.GetComponent<PlaceBlocker>();

		    }
	    }
	    

	    


    }
	
	// Update is called once per frame
	void Update () {





		if (hasBlocker)
		{
			isActive(false);
		}


		if (rightBlocker)
		{
			if (rightBlocker.hasBlocker)
			{
				if (rightBlocker.blocker.Orientation == Blocker.OrientationEmun.Horizontal)
				{
					if (QC.ActualBlocker)
					{
						if (QC.ActualBlocker.Orientation == Blocker.OrientationEmun.Horizontal)
						{
							isActive(false);
						}
						else
						{
							isActive(true);
						}
					}
					
				}
			}
		}
		
		if (leftBlocker)
		{
			if (leftBlocker.hasBlocker)
			{
				if (leftBlocker.blocker.Orientation == Blocker.OrientationEmun.Horizontal)
				{
					if (QC.ActualBlocker)
					{
						if (QC.ActualBlocker.Orientation == Blocker.OrientationEmun.Horizontal)
						{
							isActive(false);
						}
						else
						{
							isActive(true);
						}
					}
					
				}
			}
		}
		
		if (frontBlocker)
		{
			if (frontBlocker.hasBlocker)
			{
				if (frontBlocker.blocker.Orientation == Blocker.OrientationEmun.Vertical)
				{
					if (QC.ActualBlocker)
					{
						if (QC.ActualBlocker.Orientation == Blocker.OrientationEmun.Vertical)
						{
							isActive(false);
						}
						else
						{
							isActive(true);
						}
					}
					
				}
			}
		}
		
		if (backBlocker)
		{
			if (backBlocker.hasBlocker)
			{
				if (backBlocker.blocker.Orientation == Blocker.OrientationEmun.Vertical)
				{
					if (QC.ActualBlocker)
					{
						if (QC.ActualBlocker.Orientation == Blocker.OrientationEmun.Vertical)
						{
							isActive(false);
						}
						else
						{
							isActive(true);
						}
					}
					
				}
			}
		}

		if (leftBlocker && backBlocker)
		{
			if (leftBlocker.hasBlocker && backBlocker.hasBlocker)
			{
				if (leftBlocker.blocker.Orientation == Blocker.OrientationEmun.Horizontal &&
				    backBlocker.blocker.Orientation == Blocker.OrientationEmun.Vertical)
				{
					if (QC.ActualBlocker)
					{
						isActive(false);
					}
				}
			}
		}
		
		if (rightBlocker && backBlocker)
		{
			if (rightBlocker.hasBlocker && backBlocker.hasBlocker)
			{
				if (rightBlocker.blocker.Orientation == Blocker.OrientationEmun.Horizontal &&
				    backBlocker.blocker.Orientation == Blocker.OrientationEmun.Vertical)
				{
					if (QC.ActualBlocker)
					{
						isActive(false);
					}
				}
			}
		}
		
		if (rightBlocker && frontBlocker)
		{
			if (rightBlocker.hasBlocker && frontBlocker.hasBlocker)
			{
				if (rightBlocker.blocker.Orientation == Blocker.OrientationEmun.Horizontal &&
				    frontBlocker.blocker.Orientation == Blocker.OrientationEmun.Vertical)
				{
					if (QC.ActualBlocker)
					{
						isActive(false);
					}
				}
			}
		}
		
		if (leftBlocker && frontBlocker)
		{
			if (leftBlocker.hasBlocker && frontBlocker.hasBlocker)
			{
				if (leftBlocker.blocker.Orientation == Blocker.OrientationEmun.Horizontal &&
				    frontBlocker.blocker.Orientation == Blocker.OrientationEmun.Vertical)
				{
					if (QC.ActualBlocker)
					{
						isActive(false);
					}
				}
			}
		}
		

		if (QC.ActualBlocker)
			{/*
				if (QC.ActualBlocker.Orientation == Blocker.OrientationEmun.Horizontal &&
				    blocker.Orientation == Blocker.OrientationEmun.Horizontal)
				{
					if (leftBlocker)
					{
						leftBlocker.isActive(false);
					}

					if (rightBlocker)
					{
						rightBlocker.isActive(false);
						Debug.Log("jeje");
					}
				}
				*/
				
				
			}

		if (hasBlocker)
		{
			isActive(false);
		}
	    

		/*if (hasCheckedBoardPieces)
		{
			BP2 = BP1.RightBoard;
		}*/
		
	}
	
	void isActive(bool a)
	{
		GetComponent<Renderer>().enabled = a;
		GetComponent<SphereCollider>().enabled = a;
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Board_Segment")
		{
			BP1 = col.gameObject.GetComponent<BoardPiece>();
			BP2 = col.gameObject.GetComponent<BoardPiece>().RightBoard;
			BP3 = col.gameObject.GetComponent<BoardPiece>().DiagonalBoard;
			BP4 = col.gameObject.GetComponent<BoardPiece>().BackBoard;
			hasCheckedBoardPieces = true;
		}
		

	}
	
	
}