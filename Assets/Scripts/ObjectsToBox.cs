using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class ObjectsToBox : MonoBehaviour
{
    public List<GameObject> objectsInsideBox = new List<GameObject>();
    public List<string> associatedPrompts;
    private bool playedEffect = false;

    public List<string> negativePrompts;

    public Stopwatch objectCollectionTimer = new Stopwatch();
    public float collectionTime;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    public void Update()
    {
        if (objectsInsideBox.Count == 6 && playedEffect == false)
        {
            PlayParticleEffect();
            playedEffect = true;            
        }
        //UnityEngine.Debug.Log("time " + objectCollectionTimer.ElapsedMilliseconds);
    }

	void OnTriggerEnter(Collider other)
	{       
        if ((other.gameObject.tag == "Table1" || other.gameObject.tag == "Table2") && other.gameObject.name != "Box(Clone)")
        {
            if (!objectsInsideBox.Contains(other.gameObject))
            {
                objectsInsideBox.Add(other.gameObject);

                // Destroy(other.gameObject);                
                //Debug.Log("Object : " + objectsInsideBox[objectsInsideBox.Count - 1].GetComponent<Renderer>().material.color);
                other.gameObject.SetActive(false);
               /*
                other.gameObject.GetComponent<Renderer>().enabled = true;
                other.gameObject.transform.localScale = new Vector3(0.008f, 0.008f, 0.008f);
               */
            }
        }
	}

    void PlayParticleEffect()
    {
        
        this.GetComponent<ParticleSystem>().Emit(100);
      
    }

    public void DestroyObjects()
    {
        foreach(GameObject i in objectsInsideBox)
        {
            Destroy(i);
        }

    }

    public void SetAssociatedPrompts(List<string> associatedList)
    {
        associatedPrompts = associatedList;
    }

    public List<string> getAssociatedPrompts()
    {
        return associatedPrompts;
    }

    public void SetNegativePrompts(List<string> negativeList)
    {
        negativePrompts = negativeList;
    }

    public List<string> getNegativePrompts()
    {
        return negativePrompts;
    }

    
}
