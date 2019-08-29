using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class somecode : MonoBehaviour {

    Transform sphere;
    Vector3 result;

	// Use this for initialization
	void Start () {
        sphere = GameObject.Find("Sphere").transform;
	}
	
	// Update is called once per frame
	void Update () {
        result = (transform.position - sphere.position).normalized + sphere.position - transform.position;
        Debug.Log(result);
        Debug.DrawRay(transform.position, transform.InverseTransformPoint(sphere.position), Color.black);
        Debug.DrawRay(transform.position, -(transform.position - sphere.position), Color.green);
        Debug.DrawRay(transform.position, (transform.position - sphere.position).normalized + sphere.position, Color.blue);
        Debug.DrawRay(transform.position, result, Color.red);
    }
}
