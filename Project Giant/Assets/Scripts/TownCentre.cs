using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCentre : MonoBehaviour
{
    public int colour; //0=red, 1=blue, 2=Green
    public int redM;
    public int redF;
    public int blueM;
    public int blueF;
    public int greenM;
    public int greenF;
    public int happiness;

    // Start is called before the first frame update
    void Start()
    {
        Transform biomeMarkers = GameObject.Find("BiomeMarkers").transform;
        float closest = Vector3.Distance(transform.position, biomeMarkers.GetChild(0).position);
        int tempColour = 0;
        switch (biomeMarkers.GetChild(0).tag)
        {
            case "Red":
                tempColour = 0;
                break;
            case "Blue":
                tempColour = 1;
                break;
            case "Green":
                tempColour = 2;
                break;
            default:
                break;
        }
        for (int i = 1; i < biomeMarkers.childCount; i++)
        {
            if (Vector3.Distance(transform.position, biomeMarkers.GetChild(i).position) < closest)
            {
                closest = Vector3.Distance(transform.position, biomeMarkers.GetChild(i).position);
                switch (biomeMarkers.GetChild(i).tag)
                {
                    case "Red":
                        tempColour = 0;
                        break;
                    case "Blue":
                        tempColour = 1;
                        break;
                    case "Green":
                        tempColour = 2;
                        break;
                    default:
                        break;
                }
            }
        }
        colour = tempColour;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
