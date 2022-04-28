using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MenuController : MonoBehaviour
{

    public string locomotionTechnique;
    public Button walking;
    public Button teleportation;
    public string participantID = "";
    public GameObject inputFieldParticipant;
    public TouchScreenKeyboard overlayKeyboard;
    public InputField inputfield;
    public Canvas mainCanvas;
    private bool keyboardOpen = true;
	public void Start()
	{
        mainCanvas.gameObject.SetActive(false);
    }
	public void Update()
	{
        if(OVRInput.GetDown(OVRInput.RawButton.RThumbstick))
        {
            if (mainCanvas.gameObject.active == true)
            {
               mainCanvas.gameObject.SetActive(false);
            }
            else if(mainCanvas.gameObject.active == false)
            {
                mainCanvas.gameObject.SetActive(true);
            }
        }
        if (mainCanvas.gameObject.active == true)
        {
            if (inputfield.isFocused && keyboardOpen)
            {            
                overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);           
                keyboardOpen = false;               
            }
            if (overlayKeyboard != null)
            {
                inputfield.text = overlayKeyboard.text;           
                participantID = inputfield.text;
            }
            if (inputfield.isFocused == false && overlayKeyboard != null)
            {
                participantID = overlayKeyboard.text;
                overlayKeyboard.active = false;
                keyboardOpen = true;
            }
        }
    }
    public void ChooseTechnique()
    {        
        if(EventSystem.current.currentSelectedGameObject.name == "WalkingButton")
        {
            locomotionTechnique = "W";
            walking.GetComponent<Image>().color = Color.red;
            teleportation.GetComponent<Image>().color = Color.white;
        }
        else if (EventSystem.current.currentSelectedGameObject.name == "TeleportationButton")
        {
            locomotionTechnique = "T";
            walking.GetComponent<Image>().color = Color.white;
            teleportation.GetComponent<Image>().color = Color.red;
        }
    }

    public void StoreParticipantID()
    {
       
    }

    public void OpenKeyboard()
    {
        overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);        
        if (overlayKeyboard != null)
        {
            participantID = overlayKeyboard.text;
        }
    }
}
