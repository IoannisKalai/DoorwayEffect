using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWallColors : MonoBehaviour
{
    public Color[] wallColors;
    public int roomGeneratorIndex;
    public List<GameObject> roomA;
    public List<GameObject> roomB;
    private GameObject[] roomPrefabs;
    public char roomWeAreInside = 'A';

    public void Start()
    {

    }
    public void ChangeColor(char roomType)
    {
        if (roomType == 'S')
        {
            int randomColor1 = Random.Range(0, wallColors.Length);
            int randomColor2 = Random.Range(0, wallColors.Length);
            while (randomColor1 == randomColor2)
            {
                randomColor1 = Random.Range(0, wallColors.Length);
            }
            for (int i = 0; i < roomA.Count; i++)
            {
                roomA[i].GetComponent<Renderer>().material.color = wallColors[randomColor1];
            }
            for (int i = 0; i < roomB.Count; i++)
            {
                roomB[i].GetComponent<Renderer>().material.color = wallColors[randomColor2];
            }
            /*
            if (roomWeAreInside == 'B')
            {
                Debug.Log(wallColors);
                for (int i = 0; i < roomA.Count; i++)
                {
                    roomA[i].GetComponent<Renderer>().material.color = wallColors[roomGeneratorIndex];
                }
            }
            else if (roomWeAreInside == 'A')
            {
                for (int i = 0; i < roomB.Count; i++)
                {
                    roomB[i].GetComponent<Renderer>().material.color = wallColors[roomGeneratorIndex];
                }
            }
            */
        }
        else if (roomType == 'L')
        {

        }
        roomGeneratorIndex += 1;
    }
}
