using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWallColors : MonoBehaviour
{
    public Color[] wallColors;
    public int roomGeneratorIndex;
    public GameObject[] roomA = null;
    public GameObject[] roomB = null;
    public char roomWeAreInside = 'A';
    public void ChangeColor()
    {
        if (roomWeAreInside == 'B')
        {
            Debug.Log(wallColors);
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
        roomGeneratorIndex += 1;
    }
}
