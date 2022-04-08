using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsToBox : MonoBehaviour
{
    public List<GameObject> objectsInsideBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if
    }

	void OnTriggerEnter(Collider other)
	{
        Debug.Log(other.gameObject);
        if (other.gameObject.tag == "GrabbableObject")
        {
            if (!objectsInsideBox.Contains(other.gameObject))
            {
                objectsInsideBox.Add(other.gameObject);              
                Destroy(other.gameObject);                
                Debug.Log("Object : " + objectsInsideBox[objectsInsideBox.Count - 1].GetComponent<Renderer>().material.color);

               /*
                other.gameObject.GetComponent<Renderer>().enabled = true;
                other.gameObject.transform.localScale = new Vector3(0.008f, 0.008f, 0.008f);
               */
            }
        }
	}
}
