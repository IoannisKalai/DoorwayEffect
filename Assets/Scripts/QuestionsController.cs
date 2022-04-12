using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuestionsController : MonoBehaviour
{
    public List<GameObject> objectsInsideBox;
    public Text question;
    // Start is called before the first frame update
    void Start()
    {
        //objectsInsideBox = GameObject.Find("Box(Clone)").gameObject.GetComponentInChildren<ObjectsToBox>().objectsInsideBox;
    }

    // Update is called once per frame
    void Update()
    {
        objectsInsideBox = GameObject.Find("Box(Clone)").gameObject.GetComponentInChildren<ObjectsToBox>().objectsInsideBox;
        CreateQuestionPrompt();
    }

    void CreateQuestionPrompt()
    {
        if(objectsInsideBox.Count > 0)
        {
            question.text  = objectsInsideBox[0].gameObject.name + " " + objectsInsideBox[0].gameObject.GetComponent<Renderer>().material.color.ToString();
        }
    }
}
