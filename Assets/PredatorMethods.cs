using UnityEngine;
class PredatorMethods : ScriptController {

    public bool onScreen; //if predator seeable on mainCamera
    protected Transform predatorSpawnerTransform;
    public Transform home_nest;
    protected Camera mainCamera;
    protected bool predatorInitiated = false;
    protected bool stay = false;
    protected bool isSetLastPlayerPosition = false;
    protected bool faraway = false;
    public bool spotted = false;
    protected bool home = true;
    protected bool rebuild_nest = false;
    protected bool can_attack = true;
    protected Vector3 lastPlayerPosition;
    protected float wait = 10f;
    protected float wait_at_foreign = 30f;
    protected float timepassed = 0f;
    protected float timepassed_nest = 0f;
    protected GameObject Player;
    protected DiggingFunction diggingFunction;
    //Orbit Variables
    public float radius = 12.0f;
    public float radiusSpeed = 4f;
    protected float speedup = 0.5f;
    protected Vector3 Destination;
    protected float MinHeight = 4.1f;
    protected bool Landed = false;

    public void StartScript()
    {
        PredatorAI[] allChildren_nest = GameObject.FindGameObjectWithTag("Nests").transform.GetComponentsInChildren<PredatorAI>();
        foreach (PredatorAI child in allChildren_nest)
        {
            newIndicator(child.gameObject);
        }
        Reset();
    }
    public void Reset()
    {
        /*if(predatorSpawner != null)
            predatorSpawner.Reset();*/
        predatorInitiated = false;
        stay = false;
        isSetLastPlayerPosition = false;
        faraway = false;
        spotted = false;
        home = true;
        rebuild_nest = false;
        can_attack = true;
        Landed = false;
        MinHeight = 4.1f;
        lastPlayerPosition = Vector3.zero;
        timepassed = Time.fixedTime;
        timepassed_nest = Time.fixedTime;    
        transform.localPosition = Vector3.up;
        transform.rotation = Quaternion.identity;
        
    }

    /*protected void PredatorAttack()
    {
        predatorSpawner.newPredator();
        timepassed = Time.fixedTime;
        predatorSpawner.rotateSpawnerRotator(Mathf.RoundToInt(predatorSpawner.predatorSpawnerRotator.rotation.eulerAngles.y), true);
        predatorInitiated = true;
    }*/

    protected void PredatorFlyTowards()
    {
        //toTarget = (Player.transform.position - transform.position).normalized;
        if(Landed && Vector3.Distance(Player.transform.position, this.transform.position) <= 4)
        {
            transform.position = Player.transform.position;
        }else
        {
            float minHeight = 4.1f;
            if (/*Vector3.Dot(toTarget, new Vector3(0.4f, 0, 0.4f)) > 0 &&*/ Vector3.Distance(Player.transform.position, this.transform.position) <= 6)
            {
                //Debug.Log("Target is in front of this game object.");
                speedup = 5;
                minHeight = 1f;
            }
            else
            if (Vector3.Distance(Player.transform.position, this.transform.position) >= 8)
            {
                //Debug.Log("Target is not in front of this game object.");
                speedup = 0.5f;
            }
            Move(Player.transform.position, minHeight + Player.transform.position.y);
        }
        Landed = false;
        isSetLastPlayerPosition = false;
        spotted = true;
        home = false;
        faraway = !onScreen;
        goThroughChildreen(this.transform.GetComponentsInChildren<MeshRenderer>());//NEED TO MAKE MORE EFFICIENT
        if (Vector3.Distance(transform.position, Player.transform.position) <= 2 && !diggingFunction.belowground)
        {
            diggingFunction.eaten = true;
        }
    }

    protected void PredatorStay()
    {
        if (!isSetLastPlayerPosition)
        {
            lastPlayerPosition = Player.transform.position;
            timepassed = Time.fixedTime;
            isSetLastPlayerPosition = true;
            spotted = false;
        }
        //if(Vector3.Distance(transform.position, lastPlayerPosition) >= 2)
            Move(lastPlayerPosition, 0.05f);
        if(speedup == 0.5f)
        {
            Landed = false;
            stay = false;
        }
        else if (Landed)
        {
            transform.rotation = Quaternion.identity;
            if (Time.fixedTime - timepassed >= wait + Random.Range(1, 5))
            {
                Landed = false;
                stay = false;
            }
        }      
        faraway = !onScreen;
    }

    protected void PredatorGiveUp()
    {
        Landed = false;
        if (Vector3.Distance(transform.position, predatorSpawnerTransform.position) <= 5.5f)
        {
            Reset();
            /*if(predatorSpawner != null)
                Destroy(this.gameObject);*/
        }
        else
        {
            Move(predatorSpawnerTransform.position, predatorSpawnerTransform.position.y+1.5f);
            faraway = !onScreen;
        }
    }

    protected void PredatorOrbitAround()
    {
        Landed = false;
        Vector3 tangentVector = Quaternion.Euler(0, 90 * radiusSpeed * Time.deltaTime, 0) * (transform.position - predatorSpawnerTransform.position).normalized;
        Move(predatorSpawnerTransform.position + new Vector3(tangentVector.x, 0, tangentVector.z) * radius, predatorSpawnerTransform.position.y+3.5f);
        if (predatorSpawnerTransform != home_nest && Time.fixedTime - timepassed_nest >= wait_at_foreign + Random.Range(1, 5))
        {
            rebuild_nest = true;
            home = false;
            Debug.Log("Went Home To Rebuil Nest");
        }
    }

    protected void RebuildNest()
    {
        if (Landed)
        {
            transform.rotation = Quaternion.identity;
            can_attack = true;
            if (Time.fixedTime - timepassed_nest >= wait + Random.Range(1, 5))
            {
                predatorSpawnerTransform = home_nest;
                home_nest.GetComponent<NestAI>().enabled = true;
                this.transform.parent = home_nest;
                home_nest.GetComponent<MeshRenderer>().enabled = true;
                Reset();
            }
        }else
        {
            Move(new Vector3(home_nest.position.x, home_nest.position.y + 0.25f, home_nest.position.z), home_nest.position.y + 1.3f);
            can_attack = false;
            timepassed_nest = Time.fixedTime;
        }           
    }

    protected void CheckForPredatorOnScreen()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(this.transform.position);
        onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }

    protected void goThroughChildreen(MeshRenderer[] allChildren) //NEED TO MAKE MORE EFFICIENT
    {
        /*foreach (MeshRenderer child in allChildren)
        {
            if (onScreen)
            {
                child.enabled = true;
            }
            else
            {
                child.enabled = false;
            }
        }*/
    }

    protected bool RaycastingPlayer()
    {
        RaycastHit checkCover;
        Debug.DrawRay(transform.position, Player.transform.position - transform.position, Color.blue);
        if (Physics.Raycast(transform.position, Player.transform.position - transform.position, out checkCover))
        {
            if (checkCover.collider.gameObject.layer != 8)
            {
                //then the cover is not working at all.
                //home = true;
                return false;
            }else
                return true;
        }else
        {
            return false;
        }
    }

    private void Move(Vector3 destination)
    {

        Destination = destination;
        MinHeight = 4.1f;
    }

    private void Move(Vector3 destination, float _minHeight) {
        
        Destination = destination;
        MinHeight = _minHeight;
    }

    private void newIndicator(GameObject madePredator)
    {
        GameObject Indicator;
        Indicator = Instantiate(Resources.Load("VitalObjects/Arrow Rotation Holder")) as GameObject;
        //Indicator.name = "Indicator";
        Transform CanvasObject = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Transform>();
        Indicator.transform.SetParent(CanvasObject.Find("DigUI/DigItems/Indicator").GetComponent<RectTransform>(), false);
        //Indicator.transform.localPosition = Vector3.zero;
        Indicator.GetComponent<IndicatorAI>().myPredator = madePredator;
    }

    
    public bool GetPredatorInitiated()
    {
        return predatorInitiated;
    }
    public void deletePredator()
    {
        Destroy(this.gameObject);
    }
}
