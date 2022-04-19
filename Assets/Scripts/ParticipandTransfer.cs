using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticipandTransfer : MonoBehaviour
{

    public string participantID;
    public GameObject inputField;
    public Canvas startingCanvas;
    

    public void StoreID()
    {
        participantID = inputField.GetComponent<Text>().text;
        startingCanvas.gameObject.SetActive(false);
    }
}
