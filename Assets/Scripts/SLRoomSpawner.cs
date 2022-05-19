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
        for (int i = 0; i < 60; i++)
        {
            if (roomSequence.Count >= 3)
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
         
        //Create same number of large and small rooms
        //Check also so that no more than 3 of the same rooms appear in the same order
        while (!(numberOfLargeRooms == numberOfSmallRooms))
        {
            int randomRoom = Random.Range(3, roomSequence.Count - 4);
            if(numberOfLargeRooms > numberOfSmallRooms)
            {               
                if (roomSequence[randomRoom] == 'L')
                {
                    Debug.Log("EDWWWW Na kanw to L mikro stin thesi " + randomRoom);
                    int counter1 = 0;
                    int counter2 = 0;
                    roomSequence[randomRoom] = 'S';
                    for (int i = randomRoom - 3; i <= randomRoom; i++)
                    {
                        Debug.Log("Pisw 3 " + roomSequence[i]);
                        if(roomSequence[i] == roomSequence[randomRoom])
                        {
                            counter1++;
                        }
                        else if (roomSequence[i] != roomSequence[randomRoom])
                        {
                            counter1 = 0;
                        }
                    }

                    for (int i = randomRoom + 3; i >= randomRoom; i--)
                    {
                        Debug.Log("mprosta 3 " + roomSequence[i]);
                        if (roomSequence[i] == roomSequence[randomRoom])
                        {
                            counter2++;
                        }
                        else if (roomSequence[i] != roomSequence[randomRoom])
                        {
                            counter2 = 0;
                        }
                    }

                    int finalcounter = counter1 + counter2;
                    if(finalcounter >= 3)
                    {
                        roomSequence[randomRoom] = 'L';
                    }
                    else
                    {
                        Debug.Log("ALLAKSEE L -> S");
                        numberOfSmallRooms++;
                        numberOfLargeRooms--;
                    }
                }
            }
            else if(numberOfLargeRooms < numberOfSmallRooms)
            {                
                if (roomSequence[randomRoom] == 'S')
                {
                    Debug.Log("EDWWWW Na kanw to S megalo stin thesi " + randomRoom);
                    int counter1 = 0;
                    int counter2 = 0;
                    roomSequence[randomRoom] = 'L';
                    for (int i = randomRoom - 3; i <= randomRoom; i++)
                    {
                        Debug.Log("Pisw 3 " + roomSequence[i]);
                        if (roomSequence[i] == roomSequence[randomRoom])
                        {
                            counter1++;
                        }
                        else if (roomSequence[i] != roomSequence[randomRoom])
                        {
                            counter1 = 0;
                        }
                    }

                    for (int i = randomRoom + 3; i >= randomRoom; i--)
                    {
                        Debug.Log("mprosta 3 " + roomSequence[i]);
                        if (roomSequence[i] == roomSequence[randomRoom])
                        {
                            counter2++;
                        }
                        else if (roomSequence[i] != roomSequence[randomRoom])
                        {
                            counter2 = 0;
                        }
                    }

                    int finalcounter = counter1 + counter2;
                    if (finalcounter >= 3)
                    {
                        roomSequence[randomRoom] = 'S';
                    }
                    else
                    {
                        Debug.Log("ALLAKSEE S -> L");
                        numberOfSmallRooms--;
                        numberOfLargeRooms++;
                    }
                }
            }           
           
        }
        
        Debug.Log("Number of small rooms: " + numberOfSmallRooms);
        Debug.Log("Number of large rooms: " + numberOfLargeRooms);
    }

    public GameObject SpawnRoom(char roomIndicator)
    {        
        if (roomIndex > 0)
        {
            Debug.Log("Destroy Prev Room");
            Destroy(prevRoom);
        }
        Debug.Log(roomIndicator);
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
        if (roomIndex < roomSequence.Count)
            roomIndex++;
        

        return prevRoom;
    }
}
