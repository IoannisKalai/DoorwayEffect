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
        if (OVRInput.GetUp(OVRInput.RawButton.RHandTrigger) && isEmpty == true && other.gameObject.tag != "Hand" && (other.gameObject.tag == "Table1" || other.gameObject.tag == "Table2"))
        {
            Debug.Log("ITEM IN BAG" + other.gameObject.name);
            isEmpty = false;

            other.gameObject.transform.SetParent(this.gameObject.transform, true);
            other.gameObject.transform.position = this.gameObject.transform.position;
            other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            ItemInBackback = other.gameObject;               
        }
        if (isEmpty == false && other.gameObject.tag == "Hand")
        {            
            ItemInBackback.transform.position = other.gameObject.transform.position;
            ItemInBackback.GetComponent<MeshRenderer>().enabled = false;
        }

    }

	private void OnTriggerExit(Collider other)
	{       
        if (other.gameObject == ItemInBackback)
        {
            isEmpty = true;
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        if (isEmpty == false && other.gameObject.tag == "Hand")
        {
            Debug.Log("Hand In Collider With item");
            ItemInBackback.transform.position = other.gameObject.transform.position;           
        }

    }
}
