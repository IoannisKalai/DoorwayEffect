using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MenuController : MonoBehaviour
{

    public char locomotionTechnique;
    public Button walking;
    public Button teleportation;
    public string participantID = "";
    public GameObject inputFieldParticipant;
    public TouchScreenKeyboard overlayKeyboard;
    public InputField inputfield;

    public Text tryText;
    private bool keyboardOpen = true;
	public void Start()
	{
       
    }
	public void Update()
	{       
        if (inputfield.isFocused && keyboardOpen)
        {            
            overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);           
            keyboardOpen = false;               
        }
        if (overlayKeyboard != null)
        {
            inputfield.text = overlayKeyboard.text;
            tryText.text = overlayKeyboard.text;
            participantID = inputfield.text;
        }
        if (inputfield.isFocused == false && overlayKeyboard != null)
        {
            participantID = overlayKeyboard.text;
            overlayKeyboard.active = false;
            keyboardOpen = true;
        }
    }
    public void ChooseTechnique()
    {        
        if(EventSystem.current.currentSelectedGameObject.name == "WalkingButton")
        {
            locomotionTechnique = 'W';
            walking.GetComponent<Image>().color = Color.red;
            teleportation.GetComponent<Image>().color = Color.white;
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "TeleportationButton")
        {
            locomotionTechnique = 'T';
            walking.GetComponent<Image>().color = Color.white;
            teleportation.GetComponent<Image>().color = Color.red;
        }
    }

    public void StoreParticipantID()
    {
        tryText.text = overlayKeyboard.text;
    }

    public void OpenKeyboard()
    {
        overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        Debug.Log(TouchScreenKeyboard.isSupported);
        if (overlayKeyboard != null)
        {
            participantID = overlayKeyboard.text;
        }
    }
}
