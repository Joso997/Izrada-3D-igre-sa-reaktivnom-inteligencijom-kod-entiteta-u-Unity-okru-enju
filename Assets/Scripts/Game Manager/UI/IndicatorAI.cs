using UnityEngine.UI;
using UnityEngine;

public class IndicatorAI : MonoBehaviour {

    private GameObject Player;
    public GameObject myPredator;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        if(myPredator != null)
        {
            GetPredator(myPredator);
            if (myPredator.GetComponent<PredatorAI>().GetOnScreen())
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else if (transform.GetChild(0).gameObject.activeSelf != false)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }else
        {
            Destroy(gameObject);
        }
        
    }

    void GetPredator(GameObject predator)
    {
        if (predator != null)
        {
            Vector3 relativePos = Player.transform.position - predator.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, -rotation.eulerAngles.y), Time.deltaTime * 4);
        }
    }
}
