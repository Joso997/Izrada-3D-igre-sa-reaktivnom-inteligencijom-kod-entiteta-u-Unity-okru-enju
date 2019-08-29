using UnityEngine;

public class Movement_OLD : MonoBehaviour {

  public float xSpeed = 150.0f;
  public float zSpeed = 4.0f;

  //------------------- Update Void ----------------//
  void Update() {

    var x = Input.GetAxis("Horizontal") * Time.deltaTime * xSpeed;
    var z = Input.GetAxis("Vertical") * Time.deltaTime * zSpeed;

    transform.Rotate(0, x, 0);
    transform.Translate(0, 0, z);
  }
}
//------------------- End Of Script----------------//
//----------------------------------------------//
