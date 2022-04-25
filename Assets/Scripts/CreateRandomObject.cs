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

    public List<string> colorNames;
    public List<string> shapeNames;

    public List<string> associatedPrompts;
    public List<string> negativePrompts;
    public int trialNumber;
    public string doorNodoor;

    void Start()
    {        
        shapes = Resources.LoadAll<GameObject>("InteractObjects");
        shapeNames = new List<string> { "cone", "cross", "cube", "disk", "sphere", "pole", "pyramid", "beam","star", "wedge" };
        boxObject = Resources.Load<GameObject>("Box/Box");
        colors = new Color[10] { Color.red, Color.blue, Color.green, Color.grey, Color.yellow, Color.magenta, Color.white, Color.black, new Color( 110f / 255f, 38f / 255f, 14f / 255f), Color.cyan };
        colorNames = new List<string> { "red", "blue", "green", "grey", "yellow", "purple", "white", "black", "brown", "cyan" };
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
                Debug.Log(doorWing.transform.eulerAngles);
                doorRotation = false;
            }
        }       
    }

	GameObject CreateObjects()
    {       
        int chooseItem = Random.Range(0, shapes.Length);
        List<int> ItemsOnTable = new List<int>();
        List<int> ColorsOnTable = new List<int>();
        GameObject obj = null; 
        float tableTop = this.transform.position.y + this.GetComponent<Renderer>().bounds.size.y / 2;
        float posx = -0.25f;
        float posz = 0;
        Vector3 blockCentre = new Vector3(this.transform.position.x, tableTop, this.transform.position.z);
        int randomColor;
        // Box Object creation
        Vector3 boxPosition = new Vector3(0, this.GetComponent<Renderer>().bounds.size.y / 2, this.transform.right.z * this.GetComponent<Renderer>().bounds.size.z / 6 + 0.5f);
        box = Instantiate(boxObject, blockCentre + boxPosition, boxObject.transform.rotation);
        associatedPrompts = new List<string>();
        negativePrompts =  new List<string>();
        for (int i = 0; i < 6; i++)
        {

            chooseItem = Random.Range(0, shapes.Length);
            randomColor = Random.Range(0, colors.Length);
            while (ItemsOnTable.Contains(chooseItem))
            {
                chooseItem = Random.Range(0, shapes.Length);
            }
            while (ColorsOnTable.Contains(randomColor))
            {
                randomColor = Random.Range(0, colors.Length);
            }

            Vector3 position = new Vector3(posx , this.GetComponent<Renderer>().bounds.size.y / 2, this.transform.right.z * this.GetComponent<Renderer>().bounds.size.z / 6 + posz);
            obj = Instantiate(shapes[chooseItem], blockCentre + position, Quaternion.identity) as GameObject;
            
            obj.GetComponent<Renderer>().material.color = colors[randomColor];
            associatedPrompts.Add(colorNames[randomColor] + " " + shapeNames[chooseItem]);
           
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
            ColorsOnTable.Add(randomColor);
           
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
        ItemsOnTable = new List<int>();
        ColorsOnTable = new List<int>();
       
        for(int i = 0; i < 6; i++)
        {
            int randomColorNegative = Random.Range(0, colors.Length);
            int randomShapeNegative = Random.Range(0, shapes.Length);            
            while (associatedPrompts.Contains(colorNames[randomColorNegative] + " " + shapeNames[randomShapeNegative]))
            {
                randomColorNegative = Random.Range(0, colors.Length);
                randomShapeNegative = Random.Range(0, shapes.Length);                
            }
            negativePrompts.Add(colorNames[randomColorNegative] + " " + shapeNames[randomShapeNegative]);

        }
        

        if (box.gameObject.tag == "Table1")
        {
            box.transform.position += new Vector3(0, 0, -1.0f);
        }
        
        //Spawn Small/Large Room
        int roomInd = GameObject.Find("GameObject").GetComponent<SLRoomSpawner>().roomIndex;       
        char roomToCreate = GameObject.Find("GameObject").GetComponent<SLRoomSpawner>().roomSequence[roomInd];
       
        GameObject newRoom;
        newRoom = GameObject.Find("GameObject").GetComponent<SLRoomSpawner>().SpawnRoom(roomToCreate);        
        GameObject.Find("GameObject").GetComponent<ChangeWallColors>().ChangeColor(roomToCreate);       
        GameObject.Find("GameObject").GetComponent<SpawnRoomObjects>().SpawnRoomVariation(roomToCreate);         
        box.gameObject.GetComponentInChildren<ObjectsToBox>().SetAssociatedPrompts(associatedPrompts);
        box.gameObject.GetComponentInChildren<ObjectsToBox>().SetNegativePrompts(negativePrompts);
        
        
        if (roomToCreate == 'S')
        {
            Debug.Log("Door Close");            
           doorWing = newRoom.transform.GetChild(15).GetChild(0).gameObject;
            doorWing.transform.eulerAngles = new Vector3(0, -90, 0);
            Debug.Log(doorWing);
            doorWing.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            doorWing.GetComponent<Rigidbody>().velocity = Vector3.zero;
            doorRotation = true;           
        }
        hasEntered = false;

        trialNumber += 1;        
        return obj;         
    }

    //Create new object when touching grabbed object to the opposite table. 
	public void OnCollisionEnter(Collision collision)
	{        
        if (!hasEntered && (collision.gameObject.tag != this.gameObject.name) && (collision.gameObject.name == "Box(Clone)"))
        {  
            hasEntered = true;              
            collision.gameObject.GetComponentInChildren<ObjectsToBox>().DestroyObjects();                    
            GameObject.Find("PromptTrigger").gameObject.GetComponent<AppearPrompt>().promptsAppearing = true;
            Destroy(collision.gameObject);
            CreateObjects();            
        }           
	}   

    List<string> getAssociatedObjectList()
    {
        return associatedPrompts;      
    }
}
