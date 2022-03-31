using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoomObjects : MonoBehaviour
{
    public GameObject[] objectToSpawn;
    public Vector3[] objectPositions;
    public Vector3[] objectScale;
    public Vector3[] objectRotation;
    //VARIABLES FOR SCALES ALSO
    // Start is called before the first frame update
    void Start()
    {
        SpawnObjects();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObjects()
    {
        for (int i = 0; i < objectToSpawn.Length; i++)
        {
           GameObject item = Instantiate(objectToSpawn[i], objectPositions[i], Quaternion.Euler(objectRotation[i]));
           item.transform.localScale = objectScale[i];
        }
    }
}
