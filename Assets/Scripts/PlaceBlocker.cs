using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	[SerializeField] private BoardPiece BP1;
	[SerializeField] private BoardPiece BP2;
	[SerializeField] private BoardPiece BP3;
	[SerializeField] private BoardPiece BP4;

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
        
        
    }
	
	// Update is called once per frame
	void Update () {
		
		if (hasBlocker)
		{
			isActive(false);

			
			if (QC.ActualBlocker)
			{
				if (QC.ActualBlocker.Orientation == Blocker.OrientationEmun.Horizontal &&
				    blocker.Orientation == Blocker.OrientationEmun.Horizontal)
				{
					if(rightBlocker && !rightBlocker.hasBlocker)
						rightBlocker.isActive(false);
					
					if(leftBlocker && !leftBlocker.hasBlocker)
						leftBlocker.isActive(false);
					
					if(frontBlocker && !frontBlocker.hasBlocker)
						frontBlocker.isActive(true);

					if (backBlocker && !backBlocker.hasBlocker)
						backBlocker.isActive(true);
				}
				else if (QC.ActualBlocker.Orientation == Blocker.OrientationEmun.Vertical &&
				         blocker.Orientation == Blocker.OrientationEmun.Horizontal)
				{
					if(rightBlocker && !rightBlocker.hasBlocker)
						rightBlocker.isActive(true);
					
					if(leftBlocker && !leftBlocker.hasBlocker)
						leftBlocker.isActive(true);
					
					if(frontBlocker && !frontBlocker.hasBlocker)
						frontBlocker.isActive(true);

					if (backBlocker && !backBlocker.hasBlocker)
						backBlocker.isActive(true);
	
				}
				else if (QC.ActualBlocker.Orientation == Blocker.OrientationEmun.Horizontal &&
				         blocker.Orientation == Blocker.OrientationEmun.Vertical)
				{
					if(rightBlocker && !rightBlocker.hasBlocker)
						rightBlocker.isActive(true);
					
					if(leftBlocker && !leftBlocker.hasBlocker)
						leftBlocker.isActive(true);
					
					if(frontBlocker && !frontBlocker.hasBlocker)
						frontBlocker.isActive(true);

					if (backBlocker && !backBlocker.hasBlocker)
						backBlocker.isActive(true);
					
				}
				else if (QC.ActualBlocker.Orientation == Blocker.OrientationEmun.Vertical &&
				         blocker.Orientation == Blocker.OrientationEmun.Vertical)
				{
					if(rightBlocker && !rightBlocker.hasBlocker)
						rightBlocker.isActive(true);
					
					if(leftBlocker && !leftBlocker.hasBlocker)
						leftBlocker.isActive(true);
					
					if(frontBlocker && !frontBlocker.hasBlocker)
						frontBlocker.isActive(false);

					if (backBlocker && !backBlocker.hasBlocker)
						backBlocker.isActive(false);
					
				}

				
			}
	    }
	
	}
	
	void isActive(bool a)
	{
		GetComponent<Renderer>().enabled = a;
		GetComponent<SphereCollider>().enabled = a;
	}
	
	void OnTriggerEnter(Collider col)
	{
		
	}
	
	
}