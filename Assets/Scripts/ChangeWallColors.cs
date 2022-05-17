using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWallColors : MonoBehaviour
{
    public Color[] wallColors;
    public int roomGeneratorIndex;
    public List<GameObject> roomA;
    public List<GameObject> roomB;
    public List<GameObject> LargeRoom;
    private GameObject[] roomPrefabs;
    public char roomWeAreInside = 'A';
    public List<Color> colorsCreated;

    public void Start()
    {

    }
    public void ChangeColor(char roomType)
    {
        if (roomType == 'S')
        {
            Color randomColor1 = chooseColor();
            Color randomColor2 = chooseColor();

            for (int i = 0; i < roomA.Count; i++)
            {
                roomA[i].GetComponent<Renderer>().material.color = randomColor1;
            }
            for (int i = 0; i < roomB.Count; i++)
            {
                roomB[i].GetComponent<Renderer>().material.color = randomColor2;
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
            Color randomColor = chooseColor();
            for (int i = 0; i < LargeRoom.Count; i++)
            {
                LargeRoom[i].GetComponent<Renderer>().material.color = randomColor;
            }
        }
        roomGeneratorIndex += 1;
    }

    private Color chooseColor()
    {
        
        if (colorsCreated.Count >= 1)
        {
            Color lastColor = colorsCreated[colorsCreated.Count - 1];
            Color newColor = wallColors[Random.Range(0, wallColors.Length - 1)];

            while (newColor == lastColor)
            {
                newColor = wallColors[Random.Range(0, wallColors.Length - 1)];
            }
            colorsCreated.Add(newColor);
        }
        else
        {
            colorsCreated.Add(wallColors[Random.Range(0, wallColors.Length - 1)]);
        }
        
        //colorsCreated.Add(wallColors[Random.Range(0, wallColors.Length)]);
        return colorsCreated[colorsCreated.Count - 1];
    }
}
