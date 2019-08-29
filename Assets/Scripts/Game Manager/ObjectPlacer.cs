using Controllers;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour, ControllersTemplate
{

  //------------------- Variables ----------------//
    private GameObject TargetedObject;  //Object that player needs to find
    private Transform digTransform; //Transform information from terrain on which Hot&Cold takes place
    private Vector3 randVector; //Random vector for changing position of new Instantiate object
    private float[] scale = { 0, 0 };   //Saves scale information of digTransform
    //------------------- Start Void ----------------//
    public void StartScript()
    {
        digTransform = GameObject.FindGameObjectWithTag("DigTerrain").GetComponent<Transform>();
        scale[0] = digTransform.localScale.x / 2;
        scale[1] = digTransform.localScale.z / 2;
        newTargetedObject();
    }
    //------------------- Reset Void ----------------//
    public void Reset()
    {
        StartScript();
    }
    //------------------- Update Void ----------------//
    public void newTargetedObject () {       
            randVector = new Vector3(Random.Range(-scale[0], scale[0]), 1, Random.Range(-scale[1], scale[1]));
            TargetedObject = Instantiate(Resources.Load("VitalObjects/TargetedObject")) as GameObject;
            TargetedObject.transform.position = digTransform.position + randVector;
            TargetedObject.name = "TargetedObject";
	}
    public GameObject GetObject()
    {
        return TargetedObject;
    }
    public void deleteTargetObject()
    {
        Destroy(GetObject());
    }
}
//------------------- End Of Script----------------//
//----------------------------------------------//
