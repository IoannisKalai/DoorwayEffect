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
        this.gameObject.SetActive(false);
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
        }        
    }

    IEnumerator WaitSomeSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    public void AppearPromptOnScreen()
    {
        WaitSomeSeconds(2f);
        CreateQuestionPrompt();
    }
}
