using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandomObject : MonoBehaviour
{
    public GameObject[] shapes;
    public Color[] colors;
    public List<GameObject> createdObjects;
    private bool hasEntered;
    public Canvas endText;
    public GameObject doorWing;
    private Vector3 startingRotation;
    void Start()
    {
        startingRotation = doorWing.transform.eulerAngles;
        Debug.Log(startingRotation);
        shapes = Resources.LoadAll<GameObject>("InteractObjects");
        colors = new Color[7] { Color.red, Color.blue, Color.green, Color.grey, Color.yellow, Color.magenta, Color.white };
        endText.enabled = false;       
        if (this.gameObject.name.Equals("Table2"))
        {
            createdObjects.Add(CreateObject());
        }        
    }

    GameObject CreateObject()
    {       
        int chooseItem = Random.Range(0, shapes.Length);        
        float tableTop = this.transform.position.y + this.GetComponent<Renderer>().bounds.size.y / 2;
        
        Vector3 blockCentre = new Vector3(this.transform.position.x, tableTop + this.GetComponent<Renderer>().bounds.size.y / 2, this.transform.position.z + this.transform.right.z * this.GetComponent<Renderer>().bounds.size.z/5);

        
        GameObject obj = Instantiate(shapes[chooseItem], blockCentre, Quaternion.identity) as GameObject;
        obj.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];

        if (this.gameObject.name == "Table1")
        {
           // Debug.Log("Tag set to table1");
            obj.gameObject.tag = "Table1";
            GameObject.Find("GameObject").GetComponent<ChangeWallColors>().roomWeAreInside = 'A';           
        }
        else if(this.gameObject.name == "Table2")
        {
           // Debug.Log("Tag set to table2");
            obj.gameObject.tag = "Table2";
            GameObject.Find("GameObject").GetComponent<ChangeWallColors>().roomWeAreInside = 'B';
        }
        /*
        if (GameObject.Find("GameObject").GetComponent<ChangeWallColors>().roomGeneratorIndex < 7)
        {
            GameObject.Find("GameObject").GetComponent<ChangeWallColors>().ChangeColor();
            GameObject.Find("GameObject").GetComponent<SpawnRoomObjects>().SpawnRoomVariation();
        }
        else
        {
            endText.enabled = true;
        }
        */
        Debug.Log("---------------");
        int roomInd = GameObject.Find("GameObject").GetComponent<SLRoomSpawner>().roomIndex;
        Debug.Log(GameObject.Find("GameObject").GetComponent<SLRoomSpawner>().roomSequence.Count + "---------------");
        char roomToCreate = GameObject.Find("GameObject").GetComponent<SLRoomSpawner>().roomSequence[roomInd];
        
        GameObject.Find("GameObject").GetComponent<SLRoomSpawner>().SpawnRoom(roomToCreate);
        
        /*
        doorWing.transform.eulerAngles = startingRotation;
        doorWing.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        doorWing.GetComponent<Rigidbody>().velocity = Vector3.zero;
        doorWing.GetComponent<Rigidbody>().freezeRotation = true;
        doorWing.GetComponent<Rigidbody>().freezeRotation = false;
        */

        hasEntered = false;       
       
        return obj;         
    }

    //Create new object when touching grabbed object to the opposite table. 
	private void OnCollisionEnter(Collision collision)
	{
        if (!hasEntered && (collision.gameObject.tag != this.gameObject.name) && (collision.gameObject.tag == "Table1" || collision.gameObject.tag == "Table2"))
        {
           // Debug.Log(collision.gameObject.tag + " COLLIDES WITH " + this.gameObject.name);
            hasEntered = true;
            CreateObject();
            // FOR NOW THE OBJECT IS DESTROYED. THIS WILL CHANGE
            Destroy(collision.gameObject);
        }           
	}
}
