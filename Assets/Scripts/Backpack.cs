using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{

    private bool isEmpty = true;
    private GameObject ItemInBackback = null;
    private bool handInBag = false;
    private string BackpackItemName;
    private Color BackpackItemColor;
    private GameObject Hand;
    // Start is called before the first frame update
    void Start()
    {
        Hand = GameObject.Find("CustomHandRight");
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemInBackback != null)
        {
            if (ItemInBackback.GetComponent<OVRGrabbable>().isGrabbed)
            {
                ItemInBackback.GetComponent<Rigidbody>().useGravity = false;
            }
            else
            {
                ItemInBackback.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Inside Collider");  
        if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger) && isEmpty == true && other.gameObject.tag != "Hand" && handInBag == false)
        {
            Debug.Log("ITEM IN BAG" + other.gameObject.name);
            isEmpty = false;

            /*
             BackpackItemName = other.gameObject.name;
             BackpackItemColor = other.gameObject.GetComponent<Renderer>().material.color;
             Destroy(other.gameObject);

             ItemInBackback = other.gameObject;
             ItemInBackback.transform.SetParent(this.transform);
             ItemInBackback.GetComponent<MeshRenderer>()
             //ItemInBackback.SetActive(false);
             */
            other.gameObject.transform.SetParent(this.gameObject.transform, true);
            other.gameObject.transform.position = this.gameObject.transform.position;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            ItemInBackback = other.gameObject;
            //other.gameObject.GetComponent<OVRGrabbable>().grabPoints

           handInBag = true;
        }
        else if (isEmpty == false && OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            Debug.Log("Create New Item");

            other.gameObject.transform.position = Hand.transform.position;
            other.gameObject.transform.SetParent(null);
            //other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
           
            isEmpty = true;
        }

    }

	private void OnTriggerExit(Collider other)
	{
        handInBag = false;
        if (other.gameObject == ItemInBackback)
        {
            ItemInBackback = null;
        }
    }

	private void OnTriggerEnter(Collider other)
	{
       
          

	}
}
