using System;
using UnityEngine;

 class PredatorAI : PredatorMethods
{
    public PredatorSpawner predatorSpawnerTemp = null;
    private PredatorMovement predatorMovement;
    

    // Use this for initialization
    void Start () {
		Player = GameObject.FindGameObjectWithTag("Player");
        diggingFunction = Player.GetComponent<DiggingFunction>();       
        /*if (predatorSpawnerTemp != null)
        {
            predatorSpawner = predatorSpawnerTemp;
            predatorSpawnerTransform = predatorSpawner.predatorSpawner.transform;
        }
        else
        {*/
            predatorSpawnerTransform = this.gameObject.transform.parent.GetComponent<Transform>();
            home_nest = predatorSpawnerTransform;
            predatorSpawnerTemp = null;
            predatorMovement = this.gameObject.GetComponent<PredatorMovement>();
        //}           
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        StartScript();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Landed = predatorMovement.landed;
        predatorMovement.landed = Landed;
        if (spotted)
        {
            stay = true;
            //RaycastingPlayer();
        }
        if (!diggingFunction.belowground && !diggingFunction.eaten && Player.gameObject != null && spotted && can_attack)
        {
            CheckSystem(1);
            PredatorFlyTowards();
        }
        else
        if (home)
        {
            CheckSystem(0);
            PredatorOrbitAround();
        }
        else
        if (rebuild_nest)
        {
            CheckSystem(2);
            RebuildNest();
        }
        else
        if ((diggingFunction.belowground || faraway || !spotted) && !stay && !home)
        {
            CheckSystem(3);
            PredatorGiveUp();   
        }
        else
        if ((diggingFunction.belowground || faraway || !spotted) && stay && !home)
        {
            CheckSystem(4);
            PredatorStay();  
        }
        if (!diggingFunction.eaten && Player.gameObject != null)
        {
            CheckForPredatorOnScreen();
            goThroughChildreen(this.gameObject.transform.GetComponentsInChildren<MeshRenderer>());
        }
        predatorMovement.localDestination = Destination;
        predatorMovement.thisMinHeight = MinHeight;
        if (predatorMovement.landed && !Landed)
            predatorMovement.landed = Landed;
        
    }

    public void setNewNest(Transform newNest)
    {
        predatorSpawnerTransform = newNest;
        timepassed_nest = Time.fixedTime;
    }

    void CheckSystem(int i)
    {
        if(i != predatorMovement.changedMode)
        {
            switch (i)
            {
                case 0:
                    Debug.Log("Now in: " + "PredatorOrbitAround" + "mode by: " + this.gameObject.name);
                    break;
                case 1:
                    Debug.Log("Now in: " + "PredatorFlyTowards" + "mode by: " + this.gameObject.name);
                    break;
                case 2:
                    Debug.Log("Now in: " + "RebuildNest" + "mode by: " + this.gameObject.name);
                    break;
                case 3:
                    Debug.Log("Now in: " + "PredatorGiveUp" + "mode by: " + this.gameObject.name);
                    break;
                case 4:
                    Debug.Log("Now in: " + "PredatorStay" + "mode by: " + this.gameObject.name);
                    break;

            }
            predatorMovement.changedMode = i;
        }        
    }

    public bool GetOnScreen()
    {
        if (predatorMovement.changedMode == 1)
            return !onScreen;
        else
            return false;
    }

    //Added
    void OnTriggerEnter(Collider other)
    {
        if (!diggingFunction.belowground)
        {
            if (other.gameObject.tag == "Player")
            {
                spotted = true;
            }
                
        }

    }
    void OnTriggerStay(Collider other)
    {
        if (!diggingFunction.belowground)
        {
            if (other.gameObject.tag == "Player" && RaycastingPlayer())
            {
                spotted = true;
            }

        }

    }
    void OnTriggerExit(Collider other)
    {
        if (!diggingFunction.belowground)
        {
            if (other.gameObject.tag == "Player")
            {
                spotted = true;
            }

        }

    }

    public void FullReset()
    {
        spotted = false;
        transform.parent = home_nest;
        predatorSpawnerTransform = home_nest;
        Reset();
        predatorMovement.Reset();
    }

    public void ShoutDownMovement()
    {
        predatorMovement.enabled = false;
    }

    public void StartUpMovement()
    {
        predatorMovement.enabled = true;
    }

}