using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandomObject : MonoBehaviour
{
    public GameObject[] shapes;
    public Color[] colors;
    public List<GameObject> createdObjects;
    private int indexTable = 1;
    void Start()
    {
        shapes = Resources.LoadAll<GameObject>("InteractObjects");
        colors = new Color[7] { Color.red, Color.blue, Color.green, Color.grey, Color.yellow, Color.magenta, Color.white };
        if (this.gameObject.name.Equals("Table2"))
        {
            createdObjects.Add(CreateObject());
        }        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    GameObject CreateObject()
    {
        int chooseItem = Random.Range(0, shapes.Length);        
        float tableTop = this.transform.position.y + this.GetComponent<Renderer>().bounds.size.y / 2;
        
        Vector3 blockCentre = new Vector3(this.transform.position.x, tableTop + this.GetComponent<Renderer>().bounds.size.y, this.transform.position.z + this.transform.right.z * this.GetComponent<Renderer>().bounds.size.z/5);

        
        GameObject obj = Instantiate(shapes[3], blockCentre, Quaternion.identity) as GameObject;
        obj.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];

        Debug.Log("Color" + obj.GetComponent<Renderer>().material.color);
        Debug.Log("OBJECT CREATED");
       
        return obj;    
     
    }

	private void OnCollisionEnter(Collision collision)
	{
        int number = createdObjects.Count;
        CreateObject();
        if (collision.collider.gameObject != createdObjects[number - 1])
        {
            CreateObject();
        }
	}
}
