using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{
    private GameObject player;

    public float fadeDuration = 0.5f;
    public Color fadeColor;
    public GameObject fader;
    private Renderer rend;

    public GameObject buttonPole1;
    public GameObject buttonPole2;

    public Canvas promptCanvas;


    public Text countdown1;
    public Text countdown2;
    public float countTimer = 4;
    public int ButtonPressedGlobal = 0;
    public bool countdownOnce = false;
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
        if (ButtonPressed == 2)
        {
            countdown2.gameObject.SetActive(true);
            countTimer = 4;
            ButtonPressedGlobal = 2;
            countdownOnce = true;
            yield return new WaitForSeconds(4);
            FadeOut();
            yield return new WaitForSeconds(fadeDuration);
            player.transform.position = new Vector3(-0.7f, player.transform.position.y, player.transform.position.z);
            buttonPole2.SetActive(false);
            FadeIn();
            yield return new WaitForSeconds(fadeDuration);
            if (GameObject.Find("PromptTrigger").gameObject.GetComponent<AppearPrompt>().promptsAppearing == true)
            {                
                promptCanvas.gameObject.GetComponent<QuestionsController>().AppearPromptOnScreen();
                GameObject.Find("PromptTrigger").gameObject.GetComponent<AppearPrompt>().promptsAppearing = false;                
            }
            
        }
        else if (ButtonPressed == 1)
        {
            countdown1.gameObject.SetActive(true);
            countTimer = 4;
            ButtonPressedGlobal = 1;
            countdownOnce = true;
            yield return new WaitForSeconds(4);
            FadeOut();
            yield return new WaitForSeconds(fadeDuration);
            player.transform.position = new Vector3(0.7f, player.transform.position.y, player.transform.position.z);
            buttonPole1.SetActive(false);
            FadeIn();
            yield return new WaitForSeconds(fadeDuration);
            if (GameObject.Find("PromptTrigger").gameObject.GetComponent<AppearPrompt>().promptsAppearing == true)
            {
                promptCanvas.gameObject.GetComponent<QuestionsController>().AppearPromptOnScreen();
                GameObject.Find("PromptTrigger").gameObject.GetComponent<AppearPrompt>().promptsAppearing = false;
            }
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
}
