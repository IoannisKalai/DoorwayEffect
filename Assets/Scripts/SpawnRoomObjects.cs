using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomObjects : MonoBehaviour
{
    public GameObject[] roomVariations;
    public List<GameObject> roomsCreated = new List<GameObject>();
    public int roomIndex;
    private int removeIndex = 0;
    //VARIABLES FOR SCALES ALSO
    // Start is called before the first frame update
    public void Start()
    {
        SpawnRoomVariation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRoomVariation()
    {
        Debug.Log("Spawn Room " + roomIndex);
        roomsCreated.Add(Instantiate(roomVariations[roomIndex], roomVariations[roomIndex].transform.position, Quaternion.identity) as GameObject);
        roomIndex++;
        Debug.Log(roomsCreated.Count);
        if (roomsCreated.Count > 2)
        {
            Debug.Log("Destoyed " + removeIndex);
            Destroy(roomsCreated[removeIndex]);
            removeIndex++;
        }
    }
}



