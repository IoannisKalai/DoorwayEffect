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

    public List<string[]>  rowDataToSent = new List<string[]>();
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
            if(negativePromptChance <= 0.4)
            {
                string negativePrompt = negativePrompts[promptNumber];
                question.text = "Is there a " + negativePrompt + " in the box?";
                associated = "FALSE";
                this.gameObject.GetComponent<Canvas>().enabled = true;
                responseTimer.Start();
            }
            else
            {
                question.text = "Is there a " + associatedPrompts[promptNumber] + " in the box?";
                associated = "TRUE";
                this.gameObject.GetComponent<Canvas>().enabled = true;
                responseTimer.Start();
            }
        }       
    }

    public IEnumerator WaitSomeSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);        
        CreateQuestionPrompts();
        this.transform.position = new Vector3(camera.transform.position.x + (camera.transform.forward.x + distanceFromCamera), player.transform.position.y + (player.transform.forward.y * distanceFromCamera) + 0.4f, 0f);
		UnityEngine.Debug.Log("Prompt location" + this.transform.position);
		UnityEngine.Debug.Log("Camera location" + camera.transform.position);
        this.transform.Rotate(0, 180, 0);
        seconds = 0;
    }

    public void AppearPromptOnScreen()
    {
        GameObject.Find("Box_closed(Clone)").GetComponent<OVRGrabbable>().promptTimer.Stop();
        StartCoroutine(WaitSomeSeconds(secondsForPromptToAppear));       
    }

    public void writeRowData()
    {
        rowDataTemp = new string[10];
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
        rowDataTemp[4] = associated;
        rowDataTemp[5] = response;
        rowDataTemp[6] = responseTime.ToString() + " ms";
        rowDataTemp[7] = GameObject.Find("Box_closed(Clone)").GetComponent<OVRGrabbable>().promptTimer.ElapsedMilliseconds.ToString()+ " ms";
        rowDataTemp[8] = " ";

        for(int i = 0; i < associatedPrompts.Count; i++)
        {
            objectsInBox += associatedPrompts[i] + "+";
        }
        rowDataTemp[9] = objectsInBox;
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
