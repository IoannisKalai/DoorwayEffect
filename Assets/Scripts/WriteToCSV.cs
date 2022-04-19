using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class WriteToCSV : MonoBehaviour
{
    public List<string[]> rowData = new List<string[]>();
    private string filePath = "C:/Users/Yiannis/Desktop/DataFileFolder/Data.csv";
    private StreamWriter outStream;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        string[] rowDataTemp = new string[3];
        if (!File.Exists(filePath))
        {            
            rowDataTemp[0] = "Participant";
            rowDataTemp[1] = "Technique";
            rowDataTemp[2] = "Trial";
            rowDataTemp[3] = "Condition";
            rowDataTemp[4] = "Associated";
            rowDataTemp[5] = "Response";
            rowData.Add(rowDataTemp);
        }


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


        if (!File.Exists(filePath))
        {
            outStream = System.IO.File.CreateText(filePath);
        }
        else
        {
            outStream = System.IO.File.AppendText(filePath);
        }
        Debug.Log("data saved");
        outStream.WriteLine(sb);
        outStream.Close();

    }

    public void setRowData(List<string[]> data)
    {
        rowData = data;
    }
}
