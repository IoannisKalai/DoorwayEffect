using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticipandTransfer : MonoBehaviour
{

    public string participantID;
    public GameObject inputField;
    public Canvas startingCanvas;

    public string technique;

    public void StoreID()
    {
        participantID = inputField.GetComponent<Text>().text;
        technique = "W";
        startingCanvas.gameObject.SetActive(false);
    }
}
