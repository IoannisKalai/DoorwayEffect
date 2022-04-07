using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SpawnRoomObjects : MonoBehaviour
{
    public GameObject[] smallRoomVariations;
    public GameObject[] largeRoomVariations;
    public List<GameObject> roomsCreated = new List<GameObject>();
    public int roomIndex;
    private int largeRoomIndex = 0;
    private int removeIndex = 0;

	public void Start()
	{
        smallRoomVariations = Resources.LoadAll<GameObject>("RoomVars");
        largeRoomVariations = Resources.LoadAll<GameObject>("LargeRoomVars");
	}
	public void SpawnRoomVariation(char roomType)
    {
        if (roomType == 'S')
        {
            roomsCreated.Add(Instantiate(smallRoomVariations[roomIndex], smallRoomVariations[roomIndex].transform.position, Quaternion.identity) as GameObject);
            roomIndex++;
            if (roomIndex > smallRoomVariations.Length - 1)
            {
                roomIndex = 0;
            }
            roomsCreated.Add(Instantiate(smallRoomVariations[roomIndex], smallRoomVariations[roomIndex].transform.position, Quaternion.identity) as GameObject);
            roomIndex++;
            if (roomIndex > smallRoomVariations.Length - 1)
            {
                roomIndex = 0;
            }
        }
        else if (roomType == 'L')
        {
            roomsCreated.Add(Instantiate(largeRoomVariations[largeRoomIndex], largeRoomVariations[largeRoomIndex].transform.position, Quaternion.identity) as GameObject);
            largeRoomIndex++;
            if (largeRoomIndex > largeRoomVariations.Length - 1)
            {
                largeRoomIndex = 0;
            }
        }
        Debug.Log(roomsCreated.Count);
        if (roomType == 'S')
        {
            int remove = Math.Max(0, roomsCreated.Count - 2);
            for (int i = 0; i < remove; i++)
            {
                Destroy(roomsCreated[i]);
            }
        }
        else if (roomType == 'L')
        {
            int last = roomsCreated.Count - 1;
            for (int i = 0; i < last; i++)
            {
                Destroy(roomsCreated[i]);
            }
        }
        Debug.Log(roomsCreated);
    }
}



