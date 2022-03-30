using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWallColors : MonoBehaviour
{
    private Color[] wallColors;
    public int roomGeneratorIndex;
    public GameObject[] roomA;
    public GameObject[] roomB;
    public char roomWeAreInside = 'B';
    // Start is called before the first frame update
    void Start()
    {
        wallColors = new Color[7] { Color.red, Color.blue, Color.green, Color.grey, Color.yellow, Color.magenta, Color.white };
        roomGeneratorIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeColor()
    {
        if (roomWeAreInside == 'B')
        {
            for (int i = 0; i < roomA.Length; i++)
            {
                roomA[i].GetComponent<Renderer>().material.color = wallColors[roomGeneratorIndex];
            }
        }
        else if (roomWeAreInside == 'A')
        {
            for (int i = 0; i < roomB.Length; i++)
            {
                roomB[i].GetComponent<Renderer>().material.color = wallColors[roomGeneratorIndex];
            }
        }
    }
}
