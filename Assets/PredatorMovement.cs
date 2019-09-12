using UnityEngine;

public class PredatorMovement : MonoBehaviour {

    CharacterController controller;
    int raySize = 5; //size of Ray
    float speed = 4;
    public float Speed{
        get {return speed; }
        set { speed = value;rotationSpeed = speed / 1.5f; }
    }
    float rotationSpeed = 1;
    Vector3 forward;
    Vector3 desiredRay;
    Vector3 direction;
    Vector3 diff;
    public int avoid = 0;
    bool pathFound = false;
    int angleRight = 0;
    int angleLeft = 0;
    bool destinationSet = false;
    public float offsetdistance = 2;
    float lastdistance = 0;
    public int lastChangedMode = -1;
    public float thisMinHeight;
    public int changedMode = -2;
    public bool landed = false;
    public Vector3 localDestination;
    int counterOfFrames = 0;
    private Transform digTransform;
    // Use this for initialization
    void Start()
    {
        digTransform = GameObject.FindGameObjectWithTag("DigTerrain").GetComponent<Transform>();
        controller = GetComponent<CharacterController>();
        forward = new Vector3(0, 0, raySize);
        direction = transform.position;
        diff = transform.position;
        Reset();
    }

    public void Reset()
    {
        forward = new Vector3(0, 0, raySize);
        direction = transform.position;
        diff = transform.position;
        angleRight = 0;
        angleLeft = 0;
        avoid = 0;
        offsetdistance = 2;
        lastChangedMode = -1;
        lastdistance = 0;
        pathFound = false;
        destinationSet = false;
        changedMode = -2;
        landed = false;
        localDestination = Vector3.zero;
        speed = 4;
        rotationSpeed = speed / 2f;
        counterOfFrames = 0;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
             
        if (!landed)
        {
            Debug.Log(Vector3.Distance(direction, transform.position) <= offsetdistance);
            Debug.Log(Vector3.Distance(direction, transform.position));
            //AVOIDANCE
            if (AvoidanceRaycast() && !pathFound)
            {
                Debug.Log("Avoidance");
                avoid = 2;
                direction = transform.position;
                //Avoidance
            }
            else if (Vector3.Distance(direction, transform.position) <= offsetdistance || Vector3.Distance(direction, transform.position) >= (digTransform.localScale.x) || changedMode != lastChangedMode)
            {
                counterOfFrames = 0;
                avoid = 0;
            }
            //AVOIDANCE END

            //CASE SETTINGS
            if (Vector3.Distance(direction, transform.position) <= offsetdistance || (localDestination != diff && avoid == 0) || changedMode != lastChangedMode)
            {
                switch (avoid)
                {
                    case 0:
                        direction = localDestination;
                        pathFound = false;
                        destinationSet = false;
                        angleRight = 0;
                        angleLeft = 0;
                        offsetdistance = 2;
                        break;
                    case 2:
                        //Debug.Log("2");
                        ScanToFindPath();
                        counterOfFrames++;
                        break;
                }
            }
            lastChangedMode = changedMode;
            //CASE SETTINGS END

            if (!landed && (Vector3.Distance(transform.position, localDestination) <= 2 + offsetdistance || (Vector3.Distance(transform.position, localDestination) > lastdistance && lastdistance != 0)) && (transform.position.y <= localDestination.y + 1.4f /*&& transform.position.y >= localDestination.y - 1.4f*/) && (changedMode == 4 || changedMode == 2))
            {
                landed = true;
                lastdistance = 0;
            }
            else
            {
                lastdistance = Vector3.Distance(transform.position, localDestination);
            }
            Move(direction);
            
            Debug.DrawRay(transform.position, desiredRay, Color.black);
            Debug.DrawRay(transform.position + desiredRay, transform.TransformDirection(forward), Color.black);
            Debug.DrawRay(transform.position, transform.TransformDirection(forward) + transform.up * raySize, Color.cyan);
        }
    }

    protected void Move(Vector3 destination)
    {
        destination = new Vector3(destination.x, thisMinHeight, destination.z);
        Debug.DrawRay(transform.position, destination - transform.position, Color.magenta);
        if (!destinationSet || (destinationSet && avoid == 2 && pathFound))
        {
            diff = destination - transform.position;
            destinationSet = true;
        }
        controller.Move(transform.forward * Time.deltaTime * speed);
        Vector3 newDir = Vector3.RotateTowards(transform.forward, diff.normalized, Time.deltaTime * rotationSpeed, 0f);
        transform.rotation = Quaternion.LookRotation(newDir);
        if (!transform.rotation.Equals(Quaternion.LookRotation(newDir)) || Stuck())
        {
            offsetdistance += 0.3f;
        }
        
        Debug.DrawRay(transform.position, diff, Color.red);

    }

    protected bool AvoidanceRaycast()
    {
        RaycastHit obstacle;
        Debug.DrawRay(transform.position + transform.right * 1, transform.TransformDirection(forward), Color.blue);
        Debug.DrawRay(transform.position - transform.right * 1, transform.TransformDirection(forward), Color.blue);
        if (Physics.Raycast(transform.position, transform.TransformDirection(forward), out obstacle, raySize) || 
            Physics.Raycast(transform.position + transform.right * 1, transform.TransformDirection(forward), out obstacle, raySize) || 
            Physics.Raycast(transform.position - transform.right * 1, transform.TransformDirection(forward), out obstacle, raySize))
        {
            return true;
        }
        return false;
    }

    protected bool RayForSpiral()
    {
        return false;
    }

    private bool Stuck()
    {
        if (counterOfFrames > 20)
            return true;
        else
            return false;
    }

    protected void ScanToFindPath()
    {
        Vector3 testAroundRight = transform.forward * (raySize);
        Vector3 testAroundLeft = transform.forward * (raySize);
        if (pathFound)
        {
            counterOfFrames = 0;
            pathFound = false;
        }
        RaycastHit pathRight;
        RaycastHit pathLeft;
        RaycastHit path;
        testAroundRight = Quaternion.Euler(0, angleRight, 0) * testAroundRight;//calculation must go in this order ! ! !
        testAroundLeft = Quaternion.Euler(0, angleLeft, 0) * testAroundLeft;//calculation must go in this order ! ! !
        if (angleRight > 90 || angleLeft < -90)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(forward) + (transform.up * (raySize/ 1.6f)), Color.white);
            Debug.DrawRay(transform.position + transform.TransformDirection(forward) + (transform.up * (raySize/1.6f)), transform.TransformDirection(forward), Color.white);
            if (Physics.Raycast(transform.position, transform.TransformDirection(forward) + (transform.up * (raySize/ 1.6f)), out path, raySize) || Physics.Raycast(transform.position + transform.TransformDirection(forward) + (transform.up * (raySize/ 1.6f)), transform.TransformDirection(forward), out path, raySize))
            {
                direction = transform.TransformPoint(-forward) + transform.up;
                pathFound = true;
                destinationSet = false;
                offsetdistance = 1.6f;
                //Debug.Log("BackFlip");
                //BackFlip
            }
            else
            {
                direction = transform.position + transform.forward * (raySize/1.4f) + transform.up * raySize;
                pathFound = true;
                destinationSet = false;
                offsetdistance = 1f;
                //Debug.Log("Go Above");
                //Go Above
            }
        }
        else
        {
            if (Physics.Raycast(testAroundRight, transform.TransformDirection(forward), out pathRight, raySize) || Physics.Raycast(transform.position + testAroundRight, transform.TransformDirection(forward), out pathRight, raySize))
            {
                Debug.DrawLine(transform.position, transform.position + testAroundRight, Color.green);
                angleRight += 5;
                //Debug.Log(angleRight);
            }
            else if (pathFound == false)
            {
                Finalize(testAroundRight, 1);
                //Debug.Log("Right working");
            }

            if (Physics.Raycast(testAroundLeft, transform.TransformDirection(forward), out pathRight, raySize) || Physics.Raycast(transform.position + testAroundLeft, transform.TransformDirection(forward), out pathLeft, raySize))
            {
                Debug.DrawLine(transform.position, transform.position + testAroundLeft, Color.yellow);
                angleLeft -= 5;
                //Debug.Log(angleLeft);
            }
            else if(pathFound == false)
            {
                Finalize(testAroundLeft, -1);
                //Debug.Log("Left working");
            }
        }
    }

    void Finalize(Vector3 testAround, int smjer)
    {
        testAround = Quaternion.Euler(0, smjer * 30, 0) * testAround;
        direction = transform.position + testAround;
        pathFound = true;
        destinationSet = false;
        desiredRay = testAround;
        angleRight = 0;
        angleLeft = 0;
        offsetdistance = 2f;
        Debug.Log("Finalize");
    }
}
