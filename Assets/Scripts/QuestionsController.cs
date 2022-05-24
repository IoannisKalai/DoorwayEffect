using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Diagnostics;

public class QuestionsController : MonoBehaviour
{
    public List<string> associatedPrompts;
    public Text question;
    public Button yesButton;
    public Button noButton;
    public Camera camera;
    public GameObject player;
    public float distanceFromCamera = 1;
    public float secondsForPromptToAppear;    
    public int promptNumber;
    private string associated;
    private string response;

    private string[] rowDataTemp = new string[7];
    public  List<string[]> rowData = new List<string[]>();
    public List<string> negativePrompts;
    private Stopwatch responseTimer = new Stopwatch();
    private float responseTime;
    public GameObject box;     
    int negativePromptLocation1;
    int negativePromptLocation2;
    public List<string[]>  rowDataToSent = new List<string[]>();

    public float collTime;

    public string promptTriggerName;
    public bool promptAppearing;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Canvas>().enabled = false;
        //objectsInsideBox = GameObject.Find("Box(Clone)").gameObject.GetComponentInChildren<ObjectsToBox>().objectsInsideBox;
        promptNumber = 0;
    }

    // Update is called once per frame
    public void Update()
    {              
        if (box != null)
        {
            associatedPrompts = GameObject.Find("Box(Clone)").gameObject.GetComponentInChildren<ObjectsToBox>().getAssociatedPrompts(); 
            negativePrompts = GameObject.Find("Box(Clone)").gameObject.GetComponentInChildren<ObjectsToBox>().getNegativePrompts();
            
            if (box.GetComponentInChildren<ObjectsToBox>().objectsInsideBox.Count == 6)
            {
                box.gameObject.GetComponentInChildren<ObjectsToBox>().objectCollectionTimer.Stop();
                box.gameObject.GetComponentInChildren<ObjectsToBox>().collectionTime = box.gameObject.GetComponentInChildren<ObjectsToBox>().objectCollectionTimer.ElapsedMilliseconds;
                 //UnityEngine.Debug.Log(" Collection TIME " + box.gameObject.GetComponentInChildren<ObjectsToBox>().collectionTime);
                collTime = box.gameObject.GetComponentInChildren<ObjectsToBox>().collectionTime;
                UnityEngine.Debug.Log(" Collection TIME " + collTime + "  object " + this.name);
            }
           
        }
        if (this.gameObject.GetComponent<Canvas>().enabled == true)
        {
            if(OVRInput.Get(OVRInput.RawButton.X))
            {                
                yesButton.GetComponent<Image>().color = Color.cyan;
                response = "TRUE";
            }
            else if(OVRInput.Get(OVRInput.RawButton.A))
            {                              
                noButton.GetComponent<Image>().color = Color.cyan;
                response = "FALSE";
            }

            if (OVRInput.GetUp(OVRInput.RawButton.X))
            {
                yesButton.GetComponent<Image>().color = Color.white;
                responseTimer.Stop();				
                responseTime = responseTimer.ElapsedMilliseconds;               
                writeRowData();
                promptNumber++;
                this.gameObject.GetComponent<Canvas>().enabled = false;
                if (promptNumber < 6)
                {
                    CreateQuestionPrompts();
                }
                else
                {
                    promptNumber = 0;
                    rowDataToSent = rowData;
                    //GameObject.Find("GameObject").gameObject.GetComponent<WriteToCSV>().Save(rowData);
                    rowData = new List<string[]>();
                }
            }
            else if (OVRInput.GetUp(OVRInput.RawButton.A))
            {
                noButton.GetComponent<Image>().color = Color.white;
                responseTimer.Stop();
                responseTime = responseTimer.ElapsedMilliseconds;                
                writeRowData();
                promptNumber++;
                this.gameObject.GetComponent<Canvas>().enabled = false;
                if (promptNumber < 6)
                {
                    CreateQuestionPrompts();
                }
                else
                {
                    promptNumber = 0;
                    rowDataToSent = rowData;
                    //GameObject.Find("GameObject").gameObject.GetComponent<WriteToCSV>().Save(rowData);
                    rowData = new List<string[]>();
                }
            }
            if (this.gameObject.GetComponent<Canvas>().enabled == false)
            {
                yesButton.GetComponent<Image>().color = Color.white;
                noButton.GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void CreateQuestionPrompts()
    {
        float negativePromptChance = Random.Range(0.0f, 1.0f);
        responseTimer.Reset();
        if(associatedPrompts.Count > 1)
        {           
            if(promptNumber == negativePromptLocation1)
            {
                string negativePrompt = negativePrompts[0];
                if(negativePrompt.Split(' ')[0] == "orange")
                {
                    question.text = "Is there an " + negativePrompt + " in the box?";
                }
                else
                {
                    question.text = "Is there a " + negativePrompt + " in the box?";
                }
                associated = "FALSE";
                this.gameObject.GetComponent<Canvas>().enabled = true;
                responseTimer.Start();
            }
            else if(promptNumber == negativePromptLocation2)
            {
                string negativePrompt = negativePrompts[1];
                if (negativePrompt.Split(' ')[0] == "orange")
                {
                    question.text = "Is there an " + negativePrompt + " in the box?";
                }
                else
                {
                    question.text = "Is there a " + negativePrompt + " in the box?";
                }
                associated = "FALSE";
                this.gameObject.GetComponent<Canvas>().enabled = true;
                responseTimer.Start();
            }
            else
            {
                if (associatedPrompts[promptNumber].Split(' ')[0] == "orange")
                {
                    question.text = "Is there an " + associatedPrompts[promptNumber] + " in the box?";
                }
                else
                {
                    question.text = "Is there a " + associatedPrompts[promptNumber] + " in the box?";
                }                
                associated = "TRUE";
                this.gameObject.GetComponent<Canvas>().enabled = true;
                responseTimer.Start();
            }
        }       
    }

    public bool AppearPromptOnScreen(string triggerName)
    {
        promptTriggerName = triggerName;
        GameObject.Find("Box_closed(Clone)").GetComponent<OVRGrabbable>().promptTimer.Stop();       
        if (GameObject.Find("Box_closed(Clone)").gameObject.tag == "Table2" && promptTriggerName == "PromptTrigger1")
        {
            negativePromptLocation1 = Random.Range(0, 5);
            negativePromptLocation2 = Random.Range(0, 5);
            while (negativePromptLocation2 == negativePromptLocation1)
            {
                negativePromptLocation2 = Random.Range(0, 5);
            }
            CreateQuestionPrompts();
            //this.transform.position = new Vector3(camera.transform.position.x - distanceFromCamera, player.transform.position.y + (player.transform.forward.y * distanceFromCamera) + 0.4f, 0f);
            this.transform.position = new Vector3(-2.541f, player.transform.position.y + (player.transform.forward.y * distanceFromCamera) + 0.4f, 0f);
            this.transform.Rotate(0, 180, 0);
            //UnityEngine.Debug.Log(" MPIKAA " + promptTriggerName + " pinakidaa " + this.transform);
            promptAppearing = false;
        }
        else if (GameObject.Find("Box_closed(Clone)").gameObject.tag == "Table1" && promptTriggerName == "PromptTrigger2")
        {
            negativePromptLocation1 = Random.Range(0, 5);
            negativePromptLocation2 = Random.Range(0, 5);
            while (negativePromptLocation2 == negativePromptLocation1)
            {
                negativePromptLocation2 = Random.Range(0, 5);
            }
            CreateQuestionPrompts();
            //this.transform.position = new Vector3(camera.transform.position.x + distanceFromCamera, player.transform.position.y + (player.transform.forward.y * distanceFromCamera) + 0.4f, 0f);
            this.transform.position = new Vector3(2.541f, player.transform.position.y + (player.transform.forward.y * distanceFromCamera) + 0.4f, 0f);
            this.transform.Rotate(0, 180, 0);
            promptAppearing = false;
        }
        else
        {
            promptAppearing = true;
        }
        UnityEngine.Debug.Log("Prompt location" + this.transform.position);
        UnityEngine.Debug.Log("Camera location" + camera.transform.position);
        //UnityEngine.Debug.Log(" PROMPT IN QUESTION CONTROLLER " + promptAppearing);               
        return promptAppearing;
    }

    public void writeRowData()
    {
        rowDataTemp = new string[12];
        string objectsInBox = " ";
        rowDataTemp[0] = GameObject.Find("GameObject").gameObject.GetComponent<MenuController>().participantID;
        rowDataTemp[1] = GameObject.Find("GameObject").gameObject.GetComponent<MenuController>().locomotionTechnique;
        rowDataTemp[2] = (GameObject.Find("GameObject").GetComponent<SLRoomSpawner>().roomIndex).ToString();
        char roomLetter = GameObject.Find("GameObject").GetComponent<SLRoomSpawner>().roomSequence[GameObject.Find("GameObject").GetComponent<SLRoomSpawner>().roomIndex - 1];       
       
        if(roomLetter == 'S')
        {
            rowDataTemp[3] = "D";
        }
        else if(roomLetter == 'L')
        {
            rowDataTemp[3] = "ND";
        }
        rowDataTemp[4] = (promptNumber + 1).ToString();
        rowDataTemp[5] = associated;
        rowDataTemp[6] = response;
        rowDataTemp[7] = responseTime.ToString() + " ms";
        rowDataTemp[8] = GameObject.Find("Box_closed(Clone)").GetComponent<OVRGrabbable>().promptTimer.ElapsedMilliseconds.ToString()+ " ms";
        rowDataTemp[9] = " ";

        for(int i = 0; i < associatedPrompts.Count; i++)
        {
            objectsInBox += associatedPrompts[i] + "+";
        }
        rowDataTemp[10] = collTime.ToString() + " ms";
        rowDataTemp[11] = objectsInBox;
        rowData.Add(rowDataTemp);        
    }

    public List<string[]> getRowData()
    {
        return rowDataToSent;
    }

    public void setRowData(List<string[]> rowDataTemp)
    {

    }

    public GameObject getBoxObject()
    {
        return box;
    }
    public void setBoxObject(GameObject boxObj)
    {
        box = boxObj;
    }
}
