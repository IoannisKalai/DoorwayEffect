using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class WriteToCSV : MonoBehaviour
{
    public List<string[]> rowData = new List<string[]>();
    private string filePath;
    private StreamWriter outStream;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save(List<string[]> data)
    {        
        if (!File.Exists(filePath))
        {
            string[] rowDataTemp = new string[7];
            filePath = getFilePath();
            rowDataTemp[0] = "Participant";
            rowDataTemp[1] = "Technique";
            rowDataTemp[2] = "Trial";
            rowDataTemp[3] = "Condition";
            rowDataTemp[4] = "Associated";
            rowDataTemp[5] = "Response";
            rowDataTemp[6] = "Response Time";
            rowData.Add(rowDataTemp);
            string[][] output1 = new string[rowData.Count][];

            for (int i = 0; i < output1.Length; i++)
            {
                output1[i] = rowData[i];
            }

            int length1 = output1.GetLength(0);
            string delimiter1 = ",";

            StringBuilder sb1 = new StringBuilder();

            for (int index = 0; index < length1; index++)
            {
                if (index == length1 - 1)
                {
                    sb1.Append(string.Join(delimiter1, output1[index]));
                }
                else
                {
                    sb1.AppendLine(string.Join(delimiter1, output1[index]));
                }
            }


            if (!File.Exists(filePath))
            {
                outStream = System.IO.File.CreateText(filePath);
            }            
            outStream.WriteLine(sb1);
            outStream.Close();
        }
        rowData = data;
        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
        {
            if(index == length - 1)
            {
                sb.Append(string.Join(delimiter, output[index]));
            }
			else
			{
                sb.AppendLine(string.Join(delimiter, output[index]));
            }
        }       
        outStream = System.IO.File.AppendText(filePath);        
        Debug.Log("data saved");
        outStream.WriteLine(sb);
        outStream.Close();
    }

    public string getFilePath()
    {
        string dateTime = System.DateTime.Now.ToString("yyyy-MM-dd");
        string participantID = GameObject.Find("GameObject").gameObject.GetComponent<MenuController>().participantID;
        string fileName = dateTime + " " + participantID + ".csv";
        return (Application.persistentDataPath + fileName);
    }

}
