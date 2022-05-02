using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private GameObject player;

    public float fadeDuration = 1;
    public Color fadeColor;
    public GameObject fader;
    private Renderer rend;

    public GameObject buttonPole1;
    public GameObject buttonPole2;

    public Canvas promptCanvas;
 
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        rend = fader.GetComponent<Renderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TeleportToLocation(int ButtonPressed)
    {
        StartCoroutine(FadeTeleport(ButtonPressed));
    }

    public IEnumerator FadeTeleport(int ButtonPressed)
    {
        if (ButtonPressed == 2)
        {
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
}
