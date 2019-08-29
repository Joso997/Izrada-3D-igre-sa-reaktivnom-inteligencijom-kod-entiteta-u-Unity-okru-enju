using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tets : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<MeshRenderer>().material.color = new Color(0.1f, 1.0f, 1.0f, 0.6f);
    }
}
