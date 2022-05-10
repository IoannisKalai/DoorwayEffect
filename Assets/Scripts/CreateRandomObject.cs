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
    public GameObject boxObjectClosed;
    public GameObject box;
    private GameObject closedBox;
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

    public GameObject buttonPole1;
    public GameObject buttonPole2;


    public List<string[]> finalData = new List<string[]>();

    public Canvas promptCanvas;
    void Start()
    {        
        shapes = Resources.LoadAll<GameObject>("InteractObjects");
        shapeNames = new List<string> { "cone", "cross", "cube", "disk", "sphere", "pole", "pyramid", "block","star", "wedge" };
        boxObject = Resources.Load<GameObject>("Box/Box");
        boxObjectClosed = Resources.Load<GameObject>("Box/Box_closed");
        colors = new Color[10] { new Color(226f / 255f, 53f / 255f, 53f / 255f), new Color(46f / 255f, 64f / 255f, 219f / 255f), new Color(102f / 255f, 204f / 255f, 0f / 255f), Color.grey, new Color(238f / 255f, 238f / 255f, 73f / 255f), new Color(230f / 255f, 41f / 255f, 230f / 255f), Color.white, Color.black, new Color( 110f / 255f, 38f / 255f, 14f / 255f), new Color(220f / 255f, 135f / 255f, 49f / 255f) };
        colorNames = new List<string> { "red", "blue", "green", "grey", "yellow", "magenta", "white", "black", "brown", "orange" };
        endText.enabled = false;
        buttonPole2.SetActive(false);
        buttonPole1.SetActive(false);
        if (this.gameObject.name.Equals("Table2"))
        {
            createdObjects.Add(CreateObjects());
        } 
   
    }

    void Update()
	{
       if (box != null)
       {
            if (box.GetComponentInChildren<ObjectsToBox>().objectsInsideBox.Count == 6)
            {
                if(doorWing != null && doorRotation == true)
                {
                    Debug.Log("Door Open");
                    doorWing.transform.eulerAngles = new Vector3(0, -180, 0);
                    Debug.Log(doorWing.transform.eulerAngles);
                    doorRotation = false;
                }
            
                closedBox = Instantiate(boxObjectClosed, box.gameObject.transform.position, box.gameObject.transform.rotation);
                closedBox.gameObject.tag = box.gameObject.tag;
                box.gameObject.GetComponentInChildren<ObjectsToBox>().DestroyObjects(); 
                Destroy(box.gameObject);

                if(GameObject.Find("GameObject").GetComponent<MenuController>().locomotionTechnique == "T")
                {
                    if (this.gameObject.name.Equals("Table2"))
                    {
                        buttonPole2.SetActive(true);
                    }
                    else if(this.gameObject.name.Equals("Table1"))
                    {
                        buttonPole1.SetActive(true);
                    }
                }
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

            if(obj.gameObject.name == "StarObject(Clone)")
            {
                obj.transform.Rotate(0f, 90f, 0f);
            }
            else if(obj.gameObject.name == "CrossObject(Clone)")
            {
                obj.transform.Rotate(45f, 0f, 0f);
            }
            else if(obj.gameObject.name == "RectangularBoxObject(Clone)")
            {
                obj.transform.Rotate(90f, 0f, 0f);
            }

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

        GameObject.Find("PromptsCanvas").GetComponent<QuestionsController>().setBoxObject(box);
        return obj;         
    }

    //Create new object when touching grabbed object to the opposite table. 
	public void OnCollisionEnter(Collision collision)
	{        
        if (!hasEntered && (collision.gameObject.tag != this.gameObject.name) && (collision.gameObject.name == "Box_closed(Clone)"))
        {
            Debug.Log(collision.gameObject.tag + " " + this.gameObject.name + " --- " + collision.gameObject.name);
            hasEntered = true;              
            //collision.gameObject.GetComponentInChildren<ObjectsToBox>().DestroyObjects();                    
            GameObject.Find("PromptTrigger").gameObject.GetComponent<AppearPrompt>().promptsAppearing = true;
            collision.gameObject.GetComponent<OVRGrabbable>().timerStart = true;
            collision.gameObject.GetComponent<OVRGrabbable>().travelTimer.Stop();
           
            string travelTime =  collision.gameObject.GetComponent<OVRGrabbable>().travelTimer.ElapsedMilliseconds.ToString() ;
                        
            finalData = promptCanvas.gameObject.GetComponent<QuestionsController>().rowDataToSent; 
            for(int i = 0; i < finalData.Count; i ++)
            {
                finalData[i][8] = travelTime +" ms";
            }           
            promptCanvas.gameObject.GetComponent<QuestionsController>().rowDataToSent = new List<string[]>();
            
            GameObject.Find("GameObject").gameObject.GetComponent<WriteToCSV>().Save(finalData);
            finalData = new List<string[]>();
            Destroy(collision.gameObject);
            CreateObjects();            
        }           
	}   

    List<string> getAssociatedObjectList()
    {
        return associatedPrompts;      
    }

    public GameObject getBoxObject()
    {
        return box;
    }
    public void setBoxObject(GameObject boxObj)
    {
        boxObj = box;
    }

}
