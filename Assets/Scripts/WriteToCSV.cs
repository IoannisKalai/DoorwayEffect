using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class WriteToCSV : MonoBehaviour
{
    private List<string[]> rowData = new List<string[]>();
    // Start is called before the first frame update
    void Start()
    {
        Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Save()
    {
        string[] rowDataTemp = new string[3];
        rowDataTemp[0] = "Participant";
        rowDataTemp[1] = "Technique";
        rowDataTemp[2] = "Trial";
        rowData.Add(rowDataTemp);

        for (int i = 0; i < 10; i++)
        {
            rowDataTemp = new string[3];
            rowDataTemp[0] = "Sushanta" + i; // name
            rowDataTemp[1] = "" + i; // ID
            rowDataTemp[2] = "$" + UnityEngine.Random.Range(5000, 10000); // Income
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
            sb.AppendLine(string.Join(delimiter, output[index]));

        string filePath = "C:/Users/Yiannis/Desktop/DataFileFolder/Data.csv";
        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();

    }
}
