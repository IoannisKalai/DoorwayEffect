using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomObjects : MonoBehaviour
{
    public GameObject[] smallRoomVariations;
    public GameObject[] largeRoomVariations;
    public List<GameObject> roomsCreated = new List<GameObject>();
    public int roomIndex;
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
            Debug.Log("Spawn Room " + roomIndex);
            roomsCreated.Add(Instantiate(smallRoomVariations[roomIndex], smallRoomVariations[roomIndex].transform.position, Quaternion.identity) as GameObject);
            roomIndex++;
            Debug.Log(roomsCreated.Count);
            if (roomsCreated.Count > 2)
            {
                Debug.Log("Destoyed " + removeIndex);
                Destroy(roomsCreated[removeIndex]);
                removeIndex++;
            }
        }
        else if (roomType == 'L')
        {

        }
    }
}



