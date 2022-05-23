using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startTimer : MonoBehaviour
{

    public GameObject startCanvas;
    public GameObject table2;

	public void Update()
	{
		
	}
	public void startTimerFirstTime()
    {
		startCanvas = GameObject.Find("StartScreen");
		table2 = GameObject.Find("Table2");
		table2.gameObject.GetComponent<CreateRandomObject>().resetAndStartCollectionTimer();
        startCanvas.SetActive(false);
    }
}
