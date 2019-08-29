using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnim : MonoBehaviour
{

    public bool isDown = false;
    static Animator anim;
    public float idleTimer = 0f;

    // Use this for initialization
    void Start()
    {
 
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w") || (Input.GetKey("a") || (Input.GetKey("s") || (Input.GetKey("s")))))
        {
            isDown = true;
            idleTimer = 0;
        }
        else
        {
            isDown = false;
            idleTimer += Time.deltaTime;
        }


        if (isDown == true)
        {
            anim.SetBool("IsMoving", true);
        }
        
        if(isDown == false && idleTimer > 10) 
        {
            anim.SetBool("IsMoving", false);
        }
    }
}