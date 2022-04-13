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
            }
            else if (OVRInput.GetUp(OVRInput.RawButton.A))
            {
                noButton.GetComponent<Image>().color = Color.white;
                this.gameObject.GetComponent<Canvas>().enabled = false;               
            }

        }
    }

    public void CreateQuestionPrompt()
    {
        if(associatedPrompts.Count > 1)
        {
            question.text = associatedPrompts[Random.Range(0, associatedPrompts.Count)];
            this.gameObject.GetComponent<Canvas>().enabled = true;
        }        
    }

    public IEnumerator WaitSomeSeconds(float seconds)
    {
        Debug.Log("WAIT");
        yield return new WaitForSeconds(seconds);
        CreateQuestionPrompt();
        this.transform.position = camera.transform.position + (camera.transform.forward * distanceFromCamera);
        this.transform.rotation = camera.transform.rotation;
    }

    public void AppearPromptOnScreen()
    {
        StartCoroutine(WaitSomeSeconds(2.0f));       
    }
}
