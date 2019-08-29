using Controllers;
using UnityEngine;
using System.Collections.Generic;

public class PredatorSpawner : MonoBehaviour, ControllersTemplate
{

    //------------------- Variables ----------------//
    private GameObject Predator;  //Object that player needs to find
    public Transform predatorSpawnerRotator; //Transform information from terrain on which Hot&Cold takes place
    public Transform predatorSpawner; //Transform information from terrain on which Hot&Cold takes place
    private float randAngle; //Random Angle for changing position of new Instantiate object
    private float[] scale = { 0, 0 };   //Saves scale information of digTransform
    private RectTransform Indicators;
    //private List<GameObject> predatorList = new List<GameObject>();
    //------------------- Start Void ----------------//
    public void StartScript()
    {
        predatorSpawnerRotator = GameObject.FindGameObjectWithTag("PredatorSpawner").GetComponent<Transform>();
        predatorSpawner = GameObject.FindGameObjectWithTag("PredatorSpawner").GetComponent<Transform>().Find("PredatorSpawner").GetComponent<Transform>();
        Indicators = GetComponent<CanvasController>().Canvas.Find("DigUI/DigItems/Indicator").GetComponent<RectTransform>();
        /*scale[0] = digTransform.localScale.x / 2;
        scale[1] = digTransform.localScale.z / 2;*/
        //newPredator();
    }
    public void Reset()
    {
        /*if(Predator != null)
        {
            deletePredator();
        }*/
        Predator = null;
    }
    //------------------- Update Void ----------------//
    public void newPredator()
    {
        rotateSpawnerRotator(0, false);
        Predator = Instantiate(Resources.Load("VitalObjects/Predator")) as GameObject;
        Predator.transform.position = predatorSpawner.position;
        Predator.name = "Predator";
        Predator.GetComponent<PredatorAI>().predatorSpawnerTemp = this;
        Predator.transform.SetParent(transform.GetChild(0).transform, false);
        //newIndicator();
        //predatorList.Add(Predator);
    }
    public void newIndicator(GameObject madePredator)
    {
        GameObject Indicator;
        Indicator = Instantiate(Resources.Load("VitalObjects/Arrow Rotation Holder")) as GameObject;
        Debug.Log("Problem");
        //Indicator.name = "Indicator";
        Indicator.transform.SetParent(Indicators, false);
        //Indicator.transform.localPosition = Vector3.zero;
        Indicator.GetComponent<IndicatorAI>().myPredator = madePredator;
    }
    /*public GameObject GetPredator()
    {
        return Predator;
    }*/
    /*public GameObject GetPredatorbyID()
    {
        for (int i = 0; i < predatorList.Count; i++)
        {
            if (predatorList[i].ID == id)
            {
                return predatorList[i];
            }
        }
        return null;
    }*/
    /*public void deletePredator()
    {
        Destroy(GetPredator());
    }*/
    public void rotateSpawnerRotator(int x, bool t)
    {
        if(t)
            randAngle = Random.Range(x + 60, (x - 60) + 360);
        else
            randAngle = Random.Range(0, 360);
        predatorSpawnerRotator.rotation = Quaternion.Euler(0, randAngle, 0);
    }
}
//------------------- End Of Script----------------//
//----------------------------------------------//
