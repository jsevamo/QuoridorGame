using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceBlocker : MonoBehaviour
{
	[SerializeField] private bool hasBlocker;

    [SerializeField] private PlaceBlocker frontBlocker;
    [SerializeField] private PlaceBlocker rightBlocker;
    [SerializeField] private PlaceBlocker leftBlocker;
    [SerializeField] private PlaceBlocker backBlocker;

	public bool HasBlocker
	{
		get { return hasBlocker; }
		set { hasBlocker = value; }
	}

	// Use this for initialization
	void Start ()
	{
		hasBlocker = false;
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
		
	}
}
