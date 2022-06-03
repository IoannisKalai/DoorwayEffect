using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startTimer : MonoBehaviour
{

    public GameObject startCanvas;
    public GameObject table2;
	public GameObject startButton;

	public void Start()
	{
		startCanvas = GameObject.Find("StartScreen");
		table2 = GameObject.Find("Table2");
		startButton = GameObject.Find("StartScreen/StartButton");
	}
	public void Update()
	{
		if (startCanvas.GetComponent<Canvas>().enabled == true)
		{
			if (OVRInput.Get(OVRInput.RawButton.X))
			{
				startButton.GetComponent<Image>().color = Color.cyan;				
			}
			if (OVRInput.GetUp(OVRInput.RawButton.X))
			{
				startButton.GetComponent<Image>().color = Color.white;
				table2.gameObject.GetComponent<CreateRandomObject>().resetAndStartCollectionTimer();
				startCanvas.GetComponent<Canvas>().enabled = false;
			}
		}
	}
	
}
