using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticipandTransfer : MonoBehaviour
{

    public string participantID;
    public GameObject inputFieldParticipant;
    public GameObject inputFieldTechnique;
    public Canvas startingCanvas;

    public string technique;

    public void StoreIDandTechnique()
    {
        participantID = inputFieldParticipant.GetComponent<Text>().text;
        technique = inputFieldTechnique.GetComponent<Text>().text;
        startingCanvas.gameObject.SetActive(false);
    }
}
