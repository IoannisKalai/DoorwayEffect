using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{

    private bool isEmpty = true;
    private GameObject ItemInBackback;
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
        
    }

    void OnTriggerStay(Collider other)
    {
        // (other.gameObject.GetComponent<OVRGrabbable>().isGrabbed)
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

           handInBag = true;
        }
        else if (isEmpty == false && other.gameObject.GetComponent<OVRGrabbable>().isGrabbed)
        {
            Debug.Log("Create New Item");
            /*
            //ItemInBackback.SetActive(true);
            itemInstance = Instantiate(Resources.Load("InteractObjects/" + BackpackItemName)) as GameObject;
			//itemInstance.transform.SetParent(this.gameObject.transform);
			//itemInstance.transform.position = this.gameObject.transform.position;
			itemInstance.transform.position = GameObject.Find("CustomHandRight").transform.position;
            itemInstance.transform.SetParent(GameObject.Find("CustomHandRight").transform);
            itemInstance.AddComponent<BoxCollider>();
            itemInstance.GetComponent<OVRGrabbable>().GrabBegin(GameObject.Find("CustomHandRight").GetComponent<OVRGrabber>(), GameObject.Find("GrabVolumeBigR").GetComponent<Collider>());

            itemInstance.GetComponent<Rigidbody>().isKinematic = false;
            itemInstance.GetComponent<Rigidbody>().useGravity = false;
            itemInstance.GetComponent<Renderer>().material.color = BackpackItemColor;
            */
            other.gameObject.transform.SetParent(null);
            other.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            //other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            isEmpty = true;
        }


    }

	private void OnTriggerExit(Collider other)
	{
        handInBag = false;        
    }
	
	private bool BackbackHasItem()
    {
        return true;
    }




    
}
