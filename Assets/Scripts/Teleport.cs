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
        if(ButtonPressed == 2)
        {            
            FadeOut();
            player.transform.position = new Vector3(-0.7f, player.transform.position.y, player.transform.position.z);
            FadeIn();
        }
        else if(ButtonPressed == 1)
        {
            FadeOut();
            player.transform.position = new Vector3(0.7f, player.transform.position.y, player.transform.position.z);
            FadeIn();
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
