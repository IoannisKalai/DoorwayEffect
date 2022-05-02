using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    public UnityEvent onPressed, onReleased;
    public GameObject button;
    private bool isPressed;
    private GameObject presser;
    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
    }

	private void OnTriggerEnter(Collider other)
	{
		if(!isPressed)
        {
            button.transform.localPosition = new Vector3(-0.003f, -0.0173f, 0);
            presser = other.gameObject;
            onPressed.Invoke();
            Debug.Log(" PRESSED ");
            isPressed = true;
        }
    }

	private void OnTriggerExit(Collider other)
	{
		if(other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, 0, 0);
            onReleased.Invoke();
            Debug.Log(" RELEASED ");
            isPressed = false;
        }
    }
}
