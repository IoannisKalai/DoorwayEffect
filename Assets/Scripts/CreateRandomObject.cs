using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRandomObject : MonoBehaviour
{
    public GameObject[] shapes;
    public Color[] colors;
    public List<GameObject> createdObjects;
    private int indexTable = 1;
    private bool firstCollisionEnter = false;
    private bool hasEntered;
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
        
        Vector3 blockCentre = new Vector3(this.transform.position.x, tableTop + this.GetComponent<Renderer>().bounds.size.y / 2, this.transform.position.z + this.transform.right.z * this.GetComponent<Renderer>().bounds.size.z/5);

        
        GameObject obj = Instantiate(shapes[chooseItem], blockCentre, Quaternion.identity) as GameObject;
        obj.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];

        Debug.Log("Color" + obj.GetComponent<Renderer>().material.color);
        Debug.Log("OBJECT CREATED");

        if (this.gameObject.name == "Table1")
        {
            Debug.Log("Tag set to table1");
            obj.gameObject.tag = "Table1";
        }
        else
        {
            Debug.Log("Tag set to table2");
            obj.gameObject.tag = "Table2";
        }
       
        return obj;    
     
    }

	private void OnCollisionEnter(Collision collision)
	{
        if (!hasEntered && (collision.gameObject.tag != this.gameObject.name))
        {
            hasEntered = true;
            CreateObject();
            Destroy(collision.gameObject);
        }           
	}
}
