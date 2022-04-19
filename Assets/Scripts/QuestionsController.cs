using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionsController : MonoBehaviour
{
    public List<string> associatedPrompts;
    public Text question;
    public Button yesButton;
    public Button noButton;
    public Camera camera;
    public float distanceFromCamera = 1;
    public float secondsForPromptToAppear;    
    public int promptNumber = 0;
    public string associated;
    public string response;

    private string[] rowDataTemp;
    public  List<string[]> rowData;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Canvas>().enabled = false;
        //objectsInsideBox = GameObject.Find("Box(Clone)").gameObject.GetComponentInChildren<ObjectsToBox>().objectsInsideBox;
    }

    // Update is called once per frame
    public void Update()
    {
        associatedPrompts = GameObject.Find("Box(Clone)").gameObject.GetComponentInChildren<ObjectsToBox>().getAssociatedPrompts();
        if(this.gameObject.GetComponent<Canvas>().enabled == true)
        {
            if(OVRInput.Get(OVRInput.RawButton.X))
            {
                Debug.Log("Answered Yes");
                yesButton.GetComponent<Image>().color = Color.red;               
            }
            else if(OVRInput.Get(OVRInput.RawButton.A))
            {
                Debug.Log("Answered No");
                this.gameObject.GetComponent<Canvas>().enabled = false;
                noButton.GetComponent<Image>().color = Color.red;
            }

            if (OVRInput.GetUp(OVRInput.RawButton.X))
            {
                yesButton.GetComponent<Image>().color = Color.white;
                this.gameObject.GetComponent<Canvas>().enabled = false;
                response = "TRUE";
                writeRowData();
                promptNumber++;
                if (promptNumber < 6)
                {
                    CreateQuestionPrompts();
                }
                else
                {
                    promptNumber = 0;
                    GameObject.Find("GameObject").gameObject.GetComponent<WriteToCSV>().setRowData(rowData);
                    GameObject.Find("GameObject").gameObject.GetComponent<WriteToCSV>().Save();
                }
            }
            else if (OVRInput.GetUp(OVRInput.RawButton.A))
            {
                noButton.GetComponent<Image>().color = Color.white;
                this.gameObject.GetComponent<Canvas>().enabled = false;
                response = "FALSE";
                writeRowData();
                promptNumber++;
                if (promptNumber < 6)
                {
                    CreateQuestionPrompts();
                }
                else
                {
                    promptNumber = 0;
                    GameObject.Find("GameObject").gameObject.GetComponent<WriteToCSV>().setRowData(rowData);
                    GameObject.Find("GameObject").gameObject.GetComponent<WriteToCSV>().Save();
                }
            }
            
            
        }
    }

    public void CreateQuestionPrompts()
    {
        float negativePromptChance = Random.Range(0, 1);
        if(associatedPrompts.Count > 1)
        {
            if(negativePromptChance >= 0.4)
            {
                question.text = "Is there a " + GameObject.Find("GameObject").gameObject.GetComponent<CreateRandomObject>().negativePromptCreate() + " in the box?";
                this.gameObject.GetComponent<Canvas>().enabled = true;
                Debug.Log("Negatiiive PROBE");
                associated = "FALSE";
            }
            else
            {
                question.text = "Is there a " + associatedPrompts[promptNumber] + " in the box?";
                this.gameObject.GetComponent<Canvas>().enabled = true;
                associated = "TRUE";
            }
        }        
    }

    public IEnumerator WaitSomeSeconds(float seconds)
    {
        Debug.Log("WAIT");
        yield return new WaitForSeconds(seconds);        
        CreateQuestionPrompts();
        this.transform.position = camera.transform.position + (camera.transform.forward * distanceFromCamera);
        this.transform.rotation = camera.transform.rotation;
        seconds = 0;
    }

    public void AppearPromptOnScreen()
    {
        StartCoroutine(WaitSomeSeconds(secondsForPromptToAppear));       
    }

    void writeRowData()
    {
        rowDataTemp[0] = GameObject.Find("GameObject").gameObject.GetComponent<ParticipandTransfer>().participantID;
        rowDataTemp[1] = GameObject.Find("GameObject").gameObject.GetComponent<ParticipandTransfer>().technique;
        rowDataTemp[2] = GameObject.Find("GameObject").gameObject.GetComponent<CreateRandomObject>().trialNumber.ToString();
        rowDataTemp[3] = GameObject.Find("GameObject").gameObject.GetComponent<CreateRandomObject>().doorNodoor;
        rowDataTemp[4] = associated;
        rowDataTemp[5] = response;
        rowData.Add(rowDataTemp);
    }
}
