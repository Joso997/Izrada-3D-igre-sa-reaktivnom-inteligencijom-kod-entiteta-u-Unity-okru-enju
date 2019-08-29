using UnityEngine;

public class TagReader : MonoBehaviour
{
    //------------------- Variables ----------------//
    public GameObject sender;

    void OnTriggerEnter(Collider Director)
    {
        if (Director.transform.Find("Marker") != null)
        {
            Director.transform.Find("Marker").GetChild(0).gameObject.SetActive(true);
            Director.transform.Find("Marker").GetChild(1).gameObject.SetActive(true);
        }
    }
    //------------------- OnTriggerStay Void ----------------//
    void OnTriggerStay(Collider Director )
    {
        if (Director.tag == "Event")
        {
            sender = Director.gameObject;
        }
    }

    //------------------- OnTriggerExit Void ----------------//
    void OnTriggerExit(Collider Director)
    {
        if (Director.transform.Find("Marker") != null)
        {
            Director.transform.Find("Marker").GetChild(0).gameObject.SetActive(false);
            Director.transform.Find("Marker").GetChild(1).gameObject.SetActive(false);
        }          
    }
}
