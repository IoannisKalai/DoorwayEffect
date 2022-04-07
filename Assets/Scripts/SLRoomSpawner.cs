using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SLRoomSpawner : MonoBehaviour
{
    public List<char> roomSequence;
    private char[] largeSmallChars = { 'S', 'L' };
    private int numberOfSmallRooms = 0;
    private int numberOfLargeRooms = 0;
    public int roomIndex;
    private GameObject[] roomPrefabs;
    private GameObject prevRoom;
    // Start is called before the first frame update
    void Start()
    {
        roomPrefabs = Resources.LoadAll<GameObject>("SmallLargeRooms");
        roomIndex = 0;
        CreateRoomSequence();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateRoomSequence()
    {
        for (int i = 0; i < 50; i++)
        {
            if (roomSequence.Count >= 4)
            {              
                int sameChar = 1;
                char lastChar = roomSequence[roomSequence.Count - 1];               
                for (int j = i - 1; j > i - 4; j--)
                {
                    if (roomSequence[j] == lastChar)
                    {
                        sameChar++;                       
                    }                   
                }
                if (sameChar > 3)
                {
                    if (lastChar == 'S')
                    {
                        roomSequence.Add('L');
                    }
                    else
                    {
                        roomSequence.Add('S');
                    }
                }
                else
                {
                    int randomCharIndex = Random.Range(0, largeSmallChars.Length);
                    char randomChar = largeSmallChars[randomCharIndex];
                    roomSequence.Add(randomChar);
                }
            }
            else
            {
                int randomCharIndex = Random.Range(0,largeSmallChars.Length);
                char randomChar = largeSmallChars[randomCharIndex];
                roomSequence.Add(randomChar);
            }
            if (roomSequence[i] == 'S')
            {
                numberOfSmallRooms++;
            }
            else
            {
                numberOfLargeRooms++;
            }
		}
        Debug.Log("Number of small rooms: " + numberOfSmallRooms);
        Debug.Log("Number of large rooms: " + numberOfLargeRooms);
    }

    public void SpawnRoom(char roomIndicator)
    {        
        if (roomIndex > 0)
        {
            Debug.Log("Destroy Prev Room");
            Destroy(prevRoom);
        }
        if (roomIndicator == 'L')
        {
            prevRoom = Instantiate(roomPrefabs[0]);
            GameObject.Find("GameObject").GetComponent<ChangeWallColors>().LargeRoom = new List<GameObject>();
            for (int i = 0; i < 5; i++)
            {
                GameObject.Find("GameObject").GetComponent<ChangeWallColors>().LargeRoom.Add(prevRoom.transform.GetChild(i).gameObject);
            }
        }
        else if (roomIndicator == 'S')
        {
            prevRoom = Instantiate(roomPrefabs[1]);
            GameObject.Find("GameObject").GetComponent<ChangeWallColors>().roomA = new List<GameObject>();
            GameObject.Find("GameObject").GetComponent<ChangeWallColors>().roomB = new List<GameObject>();
            for (int i = 0; i < 7; i++)
            {
                GameObject.Find("GameObject").GetComponent<ChangeWallColors>().roomA.Add(prevRoom.transform.GetChild(i).gameObject);
            }
            for (int i = 7; i < 14; i++)
            {
                GameObject.Find("GameObject").GetComponent<ChangeWallColors>().roomB.Add(prevRoom.transform.GetChild(i).gameObject);
            }           
        }
        roomIndex++;
    }
}
