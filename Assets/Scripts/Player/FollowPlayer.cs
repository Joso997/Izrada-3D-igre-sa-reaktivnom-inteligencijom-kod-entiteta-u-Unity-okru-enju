using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	//------------------- Variables ----------------//
	public Transform player;
	public Vector3 offsetPostion;
	public Quaternion offsetRotation;
    public int speed = 4;

	//------------------- Start Void ----------------//
	void Start () {

		player = GameObject.Find("Player").GetComponent<Transform>();
        offsetPostion = new Vector3(0, 8, -3);
        offsetRotation = Quaternion.Euler(new Vector3(70, 0, 0));
	}

	//------------------- Update Void ----------------//
	void Update () {

		transform.position = player.position + offsetPostion;
		transform.rotation = offsetRotation;
    }
}
//------------------- End Of Script----------------//
//----------------------------------------------//
