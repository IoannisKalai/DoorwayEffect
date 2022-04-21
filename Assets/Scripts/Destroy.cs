using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public OVRGrabber myGrabber;

    public void destroyThis()

    {

        //this turns off the OVRGrabbable script

        this.GetComponent<OVRGrabbable>().enabled = false;

        //this gets the hand that's grabbing it
        
        myGrabber = this.GetComponent<OVRGrabbable>().m_grabbedBy;
        if(myGrabber != null)
        {
        Debug.Log("grabber " + myGrabber);

        //use ForceRelease method in the OVRGrabber to release object

        myGrabber.ForceRelease(this.gameObject.GetComponent<OVRGrabbable>());
        }
        //destroy object
        Debug.Log("destroy " + this.gameObject.name);
        Destroy(this.gameObject);

    }
}
