using System.Collections.Generic;
using UnityEngine;

public class CreateRandomObject : MonoBehaviour
{
    public GameObject[] shapes;
    public Color[] colors;
    public List<GameObject> createdObjects;
    private bool hasEntered;
    public Canvas endText;
    public GameObject boxObject;
    private GameObject box;
    private List<GameObject> objectInBox;
    private bool doorRotation = false;
 
    public GameObject doorWing;
    private Vector3 startingRotation;
    void Start()
    {        
        shapes = Resources.LoadAll<GameObject>("InteractObjects");
        boxObject = Resources.Load<GameObject>("Box/Box");
        colors = new Color[7] { Color.red, Color.blue, Color.green, Color.grey, Color.yellow, Color.magenta, Color.white };
        endText.enabled = false;       
        if (this.gameObject.name.Equals("Table2"))
        {
            createdObjects.Add(CreateObjects());
        } 
   
    }

    void Update()
	{
        
        if (GameObject.Find("Box(Clone)").GetComponentInChildren<ObjectsToBox>().objectsInsideBox.Count == 6)
        {
            if(doorWing != null && doorRotation == true)
            {
                Debug.Log("Door Open");
                doorWing.transform.eulerAngles = new Vector3(0, -180, 0);
                doorRotation = false;
            }
        }
        
    }

	GameObject CreateObjects()
    {       
        int chooseItem = Random.Range(0, shapes.Length);
        List<int> ItemsOnTable = new List<int>();
        GameObject obj = null; 
        float tableTop = this.transform.position.y + this.GetComponent<Renderer>().bounds.size.y / 2;
        float posx = -0.25f;
        float posz = 0;
        Vector3 blockCentre = new Vector3(this.transform.position.x, tableTop, this.transform.position.z);
        
        // Box Object creation
        Vector3 boxPosition = new Vector3(0, this.GetComponent<Renderer>().bounds.size.y / 2, this.transform.right.z * this.GetComponent<Renderer>().bounds.size.z / 6 + 0.5f);
        box = Instantiate(boxObject, blockCentre + boxPosition, boxObject.transform.rotation);


        for (int i = 0; i < 6; i++)
        {
            chooseItem = Random.Range(0, shapes.Length);
            while (ItemsOnTable.Contains(chooseItem))
            {
                chooseItem = Random.Range(0, shapes.Length);
            }
          
            Vector3 position = new Vector3(posx , this.GetComponent<Renderer>().bounds.size.y / 2, this.transform.right.z * this.GetComponent<Renderer>().bounds.size.z / 6 + posz);
            obj = Instantiate(shapes[chooseItem], blockCentre + position, shapes[chooseItem].transform.rotation) as GameObject;
            obj.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];
            posx += 0.25f;
            if (i == 2)
            {
                if (this.gameObject.name == "Table2")
                {
                    posz = -0.25f;
                }
                else
                {
                    posz = 0.25f;
                }
                posx = -0.25f;
            }
            ItemsOnTable.Add(chooseItem);
            Debug.Log("ITEMCSDC");
            if (this.gameObject.name == "Table1")
            {
                // Debug.Log("Tag set to table1");
                obj.gameObject.tag = "Table1";
                GameObject.Find("GameObject").GetComponent<ChangeWallColors>().roomWeAreInside = 'A';
                box.gameObject.tag = "Table1";
                foreach(Transform t in box.transform)
                {
                    t.gameObject.tag = "Table1";
                }
            }
            else if (this.gameObject.name == "Table2")
            {
                // Debug.Log("Tag set to table2");
                obj.gameObject.tag = "Table2";
                GameObject.Find("GameObject").GetComponent<ChangeWallColors>().roomWeAreInside = 'B';
                box.gameObject.tag = "Table2";
                foreach (Transform t in box.transform)
                {
                    t.gameObject.tag = "Table2";
                }
            }
        }

        if(box.gameObject.tag == "Table1")
        {
            box.transform.position += new Vector3(0, 0, -1.0f);
        }
        //Spawn Small/Large Room
        int roomInd = GameObject.Find("GameObject").GetComponent<SLRoomSpawner>().roomIndex;       
        char roomToCreate = GameObject.Find("GameObject").GetComponent<SLRoomSpawner>().roomSequence[roomInd];
        Debug.Log(roomToCreate);
        GameObject.Find("GameObject").GetComponent<SLRoomSpawner>().SpawnRoom('S');
        if(roomToCreate == 'S')
        {
            Debug.Log("Door Close");
            doorWing = GameObject.Find("SmallRoom(Clone)").transform.GetChild(15).GetChild(0).gameObject;
            doorWing.transform.eulerAngles = new Vector3(0, -90, 0);
            doorWing.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            doorWing.GetComponent<Rigidbody>().velocity = Vector3.zero;
            doorRotation = true;
        }


        GameObject.Find("GameObject").GetComponent<ChangeWallColors>().ChangeColor(roomToCreate);
        GameObject.Find("GameObject").GetComponent<SpawnRoomObjects>().SpawnRoomVariation(roomToCreate);

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
        if (!hasEntered && (collision.gameObject.tag != this.gameObject.name) && (collision.gameObject.tag =="Table1" || collision.gameObject.tag == "Table2"))
        {
           // Debug.Log(collision.gameObject.tag + " COLLIDES WITH " + this.gameObject.name);
            hasEntered = true;
            /*
            if (collision.gameObject.GetComponentInChildren<ObjectsToBox>().objectsInsideBox.Count == 6)
            {
                
                if(doorWing != null)
                {
                    doorWing.transform.eulerAngles = new Vector3(0, 0, 0);
                    Debug.Log("IELA");
                }
            }*/
            collision.gameObject.GetComponentInChildren<ObjectsToBox>().DestroyObjects();

            Destroy(collision.gameObject);
            CreateObjects();
            
        }           
	}
}
