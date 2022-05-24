using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearPrompt : MonoBehaviour
{
    public Canvas promptCanvas;
    public bool promptsAppearing; 
    // Start is called before the first frame update
    void Start()
    {
        promptsAppearing = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
        if(promptsAppearing == true)
        {
		    if(other.gameObject.name == "Box_closed(Clone)")
            {
                //Debug.Log("PROMPT IN APPEAR PROMT " + promptsAppearing);
                promptsAppearing = promptCanvas.gameObject.GetComponent<QuestionsController>().AppearPromptOnScreen(this.gameObject.name);               
            }
        }
    }
}
