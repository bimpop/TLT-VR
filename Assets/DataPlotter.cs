using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlotter : MonoBehaviour {

    // Name of the input file, no extension
    public string inputfile;
    
    // List for holding data from CSV reader
    private List<Dictionary<string, object>> pointList;
    
    // Indices for columns to be assigned
    public int columnX = 1;
    public int columnY = 2;
    public int columnZ = 3;
    public int columnT = 4;
    
    // Full column names
    public string xName;
    public string yName;
    public string zName;
    public string tName;

    // The prefab for the data points to be instantiated
    public GameObject PointPrefab;

    // The prefab for the data points that will be instantiated
    public GameObject PointHolder;

    // Use this for initialization
    void Start () {

        // Set pointlist to results of function Reader with argument inputfile
        pointList = CSVReader.Read(inputfile);
        
        //Log to console
        Debug.Log(pointList);

        // Declare list of strings, fill with keys (column names)
        List<string> columnList = new List<string>(pointList[1].Keys);
 
        // Print number of keys (using .count)
        Debug.Log("There are " + columnList.Count + " columns in CSV");
        
        foreach (string key in columnList)
        Debug.Log("Column name is " + key);

        // Assign column name from columnList to Name variables
        xName = columnList[columnX];
        yName = columnList[columnY];
        zName = columnList[columnZ];
        tName = columnList[columnT];

        //Loop through Pointlist
        for (var i = 0; i < pointList.Count; i++) {
            // Get value in poinList at ith "row", in "column" Name
            float x = System.Convert.ToSingle(pointList[i][xName]);
            float y = System.Convert.ToSingle(pointList[i][yName]);
            float z = System.Convert.ToSingle(pointList[i][zName]);
            float t = System.Convert.ToSingle(pointList[i][tName]);
            
            // Instantiate as gameobject variable so that it can be manipulated within loop
            GameObject dataPoint = Instantiate(
            PointPrefab, 
            new Vector3(x, y, z), 
            Quaternion.identity);

            // Make dataPoint child of PointHolder object 
            dataPoint.transform.parent = PointHolder.transform;

            // Rotate the data to the appropriate orientation
            PointHolder.transform.Rotate(-90.0f, 180.0f, 0.0f, Space.Self);

            // Assigns original values to dataPointName
            string dataPointName = 
            pointList[i][xName] + " "
            + pointList[i][yName] + " "
            + pointList[i][zName];

            // Assigns name to the prefab
            dataPoint.transform.name = dataPointName;

            // RGB color
            int r = 255;
            int g = 0;
            int b = 0;

            switch (t) {
                case float n when n <= 294.0:
                    r = 0;
                    g = 255;
                    b = 0;
                    break;
                case float n when n > 294.0 && n <= 296.0:
                    r = 204;
                    g = 204;
                    b = 0;
                    break;
                case float n when n > 296.0:
                    r = 255;
                    g = 0;
                    b = 0;
                    break;
                default:
                    r = 255;
                    g = 0;
                    b = 0;
                    break;
            }

            // Gets material color and sets it to a new RGBA color we define
            dataPoint.GetComponent<Renderer>().material.color = 
            new Color(r,g,b, 1.0f);
        }
    }
}