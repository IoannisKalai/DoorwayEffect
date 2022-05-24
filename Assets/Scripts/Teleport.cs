using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    public GameObject player;
    public GameObject centerEyeAnchor;
    public float fadeDuration = 0.5f;
    public Color fadeColor;
    public GameObject fader;
    private Renderer rend;

    public GameObject buttonPole1;
    public GameObject buttonPole2;

    public Canvas promptCanvas;

    public GameObject TeleportPoint1;
    public GameObject TeleportPoint2;

    public Text countdown1;
    public Text countdown2;
    public float countTimer = 2;
    public int ButtonPressedGlobal = 0;
    public bool countdownOnce = false;
    public bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        rend = fader.GetComponent<Renderer>();
        countdown1.gameObject.SetActive(false);
        countdown2.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(countdownOnce)
        {
            Countdown();
        }
    }

    public void TeleportToLocation(int ButtonPressed)
    {
        StartCoroutine(FadeTeleport(ButtonPressed));
    }

    public IEnumerator FadeTeleport(int ButtonPressed)
    {
        if (ButtonPressed == 2 && isPressed == false)
        {
            isPressed = true;
            countdown2.gameObject.SetActive(true);
            countTimer = 2;
            ButtonPressedGlobal = 2;
            countdownOnce = true;
            yield return new WaitForSeconds(2);
            FadeOut();
            yield return new WaitForSeconds(fadeDuration);
            this.enabled = false;

            this.transform.position = new Vector3(-0.7f, this.transform.position.y, this.transform.position.z);
            var ceaOffset = new Vector3(centerEyeAnchor.transform.localPosition.x, 0, centerEyeAnchor.transform.localPosition.z);

            this.transform.Translate(-ceaOffset);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            this.enabled = true;
            //cameraRig.transform.position = TeleportPoint1.transform.position;
            //cameraRig.transform.position += new Vector3(-2.957f, 0, 0);
            buttonPole2.SetActive(false);
            FadeIn();
            yield return new WaitForSeconds(fadeDuration);
           
            if (GameObject.Find("PromptTrigger1").gameObject.GetComponent<AppearPrompt>().promptsAppearing == true)
            {
                Debug.Log("Started Coroutine at timestamp : " + Time.time);
                StartCoroutine(WaitForRealSeconds(0.5f));
                Debug.Log("Finished Coroutine at timestamp : " + Time.time);
                promptCanvas.gameObject.GetComponent<QuestionsController>().AppearPromptOnScreen("PromptTrigger1");
                GameObject.Find("PromptTrigger1").gameObject.GetComponent<AppearPrompt>().promptsAppearing = false;
            }
            isPressed = false;
        }
        else if (ButtonPressed == 1 && isPressed == false)
        {
            isPressed = true;
            countdown1.gameObject.SetActive(true);
            countTimer = 2;
            ButtonPressedGlobal = 1;
            countdownOnce = true;
            yield return new WaitForSeconds(2);
            FadeOut();
            yield return new WaitForSeconds(fadeDuration);
            this.enabled = false;
            this.transform.position = new Vector3(0.7f, this.transform.position.y, this.transform.position.z);
            var ceaOffset = new Vector3(centerEyeAnchor.transform.localPosition.x, 0, centerEyeAnchor.transform.localPosition.z);

            this.transform.Translate(-ceaOffset);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            this.enabled = true;
            this.enabled = true;
            // cameraRig.transform.position = new Vector3(0.016f, cameraRig.transform.position.y, -1.7f);

            //cameraRig.transform.position = TeleportPoint2.transform.position;
            //this.transform.position += new Vector3(3.1f, 0, 0);
            buttonPole1.SetActive(false);
            FadeIn();
            yield return new WaitForSeconds(fadeDuration);
            
            if (GameObject.Find("PromptTrigger2").gameObject.GetComponent<AppearPrompt>().promptsAppearing == true)
            {
                Debug.Log("Started Coroutine at timestamp : " + Time.time);
                StartCoroutine(WaitForRealSeconds(0.5f));
                Debug.Log("Finished Coroutine at timestamp : " + Time.time);
                promptCanvas.gameObject.GetComponent<QuestionsController>().AppearPromptOnScreen("PromptTrigger2");
                GameObject.Find("PromptTrigger2").gameObject.GetComponent<AppearPrompt>().promptsAppearing = false;
            }
            isPressed = false;
        }
    }
    public void FadeIn()
    {
        Fade(1, 0);
    }
    public void FadeOut()
    {
        Fade(0, 1);
    }
    public void FadeInOut()
    {
        Fade(1, 0);
        Fade(0, 1);
    }
    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);

            rend.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;
        rend.material.SetColor("_Color", newColor2);
    }

    public void Countdown()
    {
        countTimer -= Time.deltaTime;
        if (ButtonPressedGlobal == 1)
        {
            countdown1.text = countTimer.ToString("0.0");
        }
        else if (ButtonPressedGlobal == 2)
        {
            countdown2.text = countTimer.ToString("0.0");
        }

        if (countTimer < 0)
        {
            countdown2.gameObject.SetActive(false);
            countdown1.gameObject.SetActive(false);
            countdownOnce = false;
        }       
    }

    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }
}
