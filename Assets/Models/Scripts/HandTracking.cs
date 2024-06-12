using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    public UDPReceive udpReceive;
    public GameObject[] handPoints;

    //insert the experimental distances for the hardware setup
    public float[] experimentalDistances;

    //dimension the motion correct, the camera is not really good on depth 
    public float dimensional_factor;

    //Array to track coordinates of fingers 
    Vector3[] trackpoints = new Vector3[21];
    //Distance array 
    float[] point_dist = new float[5];
    //bool array if gesture is active
    bool[] gesture_dection = new bool[5];


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;

        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        //print(data);
        string[] points = data.Split(',');
        //print(handPoints[0]);


        for ( int i = 0; i < 21; i++)
        {
            float x = float.Parse(points[i * 3]) / dimensional_factor; //decide on a suitable factor 
            float y = float.Parse(points[i * 3 + 1]) / dimensional_factor;
            float z = float.Parse(points[i * 3 + 2]) / dimensional_factor;

            handPoints[i].transform.localPosition = new Vector3(x, y, z);
            trackpoints[i] = new Vector3(x, y, z);
        }

        //calculate the distances 
        point_dist[0] = Vector3.Distance(trackpoints[4], trackpoints[1]); //thumb 
        point_dist[1] = Vector3.Distance(trackpoints[8], trackpoints[5]); //index finger 
        point_dist[2] = Vector3.Distance(trackpoints[12], trackpoints[9]); //middle finger 
        point_dist[3] = Vector3.Distance(trackpoints[16], trackpoints[13]); //ring finger 
        point_dist[4] = Vector3.Distance(trackpoints[20], trackpoints[17]); //pinky  

        Debug.Log($"Distance 1: {point_dist[0]}");
        Debug.Log($"Distance 2: {point_dist[1]}");
        Debug.Log($"Distance 3: {point_dist[2]}");
        Debug.Log($"Distance 4: {point_dist[3]}");
        Debug.Log($"Distance 5: {point_dist[4]}");


        //the following problem occurs here, thus we only use one camera there is no depth info (that is reliable), thus the distance from tip to root of the hand 
        // changes depending on the distance of the hand to the camera (m´smaller when further away) The following data is done at 23 cm distance to cam
        //Therefore we have to specif certian experimental values 

        bool[] gesture_dection = new bool[5];

        gesture_dection[0] = (point_dist[0] < experimentalDistances[0]);
        gesture_dection[1] = (point_dist[1] < experimentalDistances[1]);
        gesture_dection[2] = (point_dist[2] < experimentalDistances[2]);
        gesture_dection[3] = (point_dist[3] < experimentalDistances[3]);
        gesture_dection[4] = (point_dist[4] < experimentalDistances[4]);


    }
}
