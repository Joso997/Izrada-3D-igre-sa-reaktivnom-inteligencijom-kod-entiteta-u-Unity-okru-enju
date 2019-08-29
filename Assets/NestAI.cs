using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestAI : MonoBehaviour {
    // Use this for initialization
    public void Reset () {
        this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<NestAI>().enabled = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.GetComponent<MeshRenderer>().enabled = false;
            NestAI[] nestAI = GameObject.FindGameObjectWithTag("Nests").transform.GetComponentsInChildren<NestAI>();
            Transform closest_nest = null;
            float dist = 0f;
                foreach (NestAI child in nestAI)
                {
                    if (child.transform != this.transform && child.enabled == true)
                    {
                        dist = Vector3.Distance(child.transform.position, this.transform.position);
                        if (closest_nest == null)
                            closest_nest = child.transform;
                        else
                        if (Vector3.Distance(closest_nest.position, this.transform.position) > dist)
                            closest_nest = child.transform;
                    }
                }              
            if(closest_nest != null)
            {
                PredatorAI[] allChildren_nest = this.transform.GetComponentsInChildren<PredatorAI>();
                foreach (PredatorAI child in allChildren_nest)
                {
                    child.transform.parent = closest_nest;
                    child.setNewNest(closest_nest);
                }
            }
            else{
                PredatorAI[] allChildren_nest = this.transform.GetComponentsInChildren<PredatorAI>();
                foreach (PredatorAI child in allChildren_nest)
                {
                    closest_nest = GameObject.FindGameObjectWithTag("Nests").transform.Find("default_nest").GetComponent<Transform>();
                    child.transform.parent = closest_nest;
                    child.setNewNest(closest_nest);
                }
            }
            other.GetComponent<DiggingFunction>().nestBroken();
            this.GetComponent<NestAI>().enabled = false;
            
        }
        
    }
}
