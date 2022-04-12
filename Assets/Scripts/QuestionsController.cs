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
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Canvas>().enabled = false;
        //objectsInsideBox = GameObject.Find("Box(Clone)").gameObject.GetComponentInChildren<ObjectsToBox>().objectsInsideBox;
    }

    // Update is called once per frame
    void Update()
    {
        associatedPrompts = GameObject.Find("Box(Clone)").gameObject.GetComponentInChildren<ObjectsToBox>().getAssociatedPrompts();
        
    }

    public void CreateQuestionPrompt()
    {
        if(associatedPrompts.Count == 6)
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
    }

    public void AppearPromptOnScreen()
    {
        StartCoroutine(WaitSomeSeconds(2.0f));       
    }
}
