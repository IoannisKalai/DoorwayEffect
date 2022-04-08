using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsToBox : MonoBehaviour
{
    public List<GameObject> objectsInsideBox;
    public List<string> objectType;
    public List<Color> objectColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter(Collider other)
	{
        Debug.Log(other.gameObject);
        if (other.gameObject.tag == "GrabbableObject")
        {
            if (!objectsInsideBox.Contains(other.gameObject))
            {
                objectsInsideBox.Add(other.gameObject);
               
                objectType.Add(other.gameObject.name);
                objectColor.Add(other.gameObject.GetComponent<Renderer>().material.color);
                Destroy(other.gameObject);
                Debug.Log("Object Type: " + objectType[objectType.Count - 1] + " Object Color: " + objectColor[objectColor.Count - 1]);
               /*
                other.gameObject.GetComponent<Renderer>().enabled = true;
                other.gameObject.transform.localScale = new Vector3(0.008f, 0.008f, 0.008f);
               */
            }
        }
	}
}
