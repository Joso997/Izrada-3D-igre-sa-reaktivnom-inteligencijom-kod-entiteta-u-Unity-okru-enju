using Controllers;
using UnityEngine;

class HotAndColdController : GameRulesController, ControllersTemplate
{
    //------------------- Scripts ----------------//
    private ObjectPlacer objectplacer;
    public Movement movement;
    public DiggingFunction diggingFunction;
    public GameObject player;
    //------------------- Variables ----------------//
    private int radius = 0; //Represents distance from the object
    private int diggingspeed = 1;
    public float foundValue = 0;  //Used to set score value for script CanvasController
    public float scoreValue = 0;  //Used to set score value for script CanvasController
    private float stopWhileDigging = 0;  //Stops player movement for setStopTime seconds, represents digging
    private float setStopTime = 0.38f; // Defines how long in seconds will player movement be stopped
    private float dighardness = 0f;
    public int brokenNestCounter = 0;
    public int nestCount = -1;
    public string radiusText; //Used to set radius text for script CanvasController
    public bool objectFound = false; //Used for script CanvasController
    private bool buttonpressed = false;
    public bool radiusSet = false; //Used for script RadiusText
    private bool upgrade = false;  //BonusTime and Speed given on found x_amount of found objects
    private bool diggingObject = false;
    //------------------- Properties ----------------//
    public int ruleID { get; set; }

    //------------------- Start Void ----------------//
    public void StartScript()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = player.GetComponent<Movement>();
        diggingFunction = player.GetComponent<DiggingFunction>();
        objectplacer = GetComponent<ObjectPlacer>();
        diggingFunction.nestBroken = calculateNests;
        nestCount = GameObject.FindGameObjectWithTag("Nests").transform.GetComponentsInChildren<NestAI>().Length;
        Reset();
    }
    public void Reset()
    {
        StartController(ruleID);
        Timer.timerLength = localdatabase[0].Time[0].starttime;
        Timer.direction = localdatabase[0].Time[0].direction;
        movement.jumpEnabled = true;
        diggingFunction.eaten = false;
        objectFound = false;
        radiusSet = false;
        upgrade = false;
        diggingObject = false;
        scoreValue = 0;
        foundValue = 0;
        dighardness = 0f;
        radius = 0;
        diggingspeed = 1;
        stopWhileDigging = 0;
        brokenNestCounter = nestCount;
        SetTargetObject();
        objectplacer.deleteTargetObject();
        objectplacer.newTargetedObject();
    }
    //------------------- Update Void ----------------//
    void Update()
    {
        if ((Input.GetButtonDown("Scan") || buttonpressed) && (movement.enabled || stopWhileDigging == 0) && !diggingFunction.belowground && movement.OnGround())
        {
            DetermineRadius();
            if (StartDigging())
            {
                dighardness -= diggingspeed;
                diggingObject = true;
                if (dighardness <= -1)
                    StopDigging();
            }
            stopWhileDigging = Time.fixedTime;
        }
        if (CalculateStopTime(stopWhileDigging))
        {
            stopWhileDigging = 0;
            if (!diggingObject)
            {
                movement.enabled = true;

            }

        }
        if (upgrade == true)//Bonus settings
        {
            Timer.timerLength += localdatabase[0].Bonus[0].bonustime;
            movement.values["speed"] *= localdatabase[0].Bonus[0].movementspeed;
            diggingspeed *= localdatabase[0].Bonus[0].diggingspeed;
            upgrade = false;
        }
        calculateNests();
    }
    //------------------- DetermineRadius Void ----------------//
    private void DetermineRadius()
    {
        /*Debug.Log(objectplacer.GetObject().transform.position);
        Debug.Log(player.transform.position);*/
        var vectorToTarget = objectplacer.GetObject().transform.position - player.transform.position;
        vectorToTarget.y = 0;
        float x = vectorToTarget.magnitude;
        //Debug.Log("Distance: " + x);
        if (x <= 2)
        {
            radius = 5;//Object
        }
        else if (x > 2 && x <= 5)
        {
            radius = 4;//Very Hot
        }
        else if (x > 5 && x <= 10)
        {
            radius = 3;//Hot
        }
        else if (x > 10 && x <= 15)
        {
            radius = 2;//Cold
        }
        else if (x > 15)
        {
            radius = 1;//Very Cold
        }
    }
    //------------------- CalculateStopTime bool ----------------// Simulira kopanje
    public bool CalculateStopTime(float stop)
    {
        if ((Time.fixedTime - stop) >= setStopTime && stop != 0)
        {
            return true;
        }
        return false;
    }
    //------------------- SeeIfConditionMet bool ----------------//NEED TO ADD FOREACH LOOPS ONCE MORE RULES AT THE SAME TIME ARE IMPLEMENTED
    public bool SeeIfConditionMetWin()
    {
        return CheckCondition(localdatabase[0].Win[0].condition, localdatabase[0].Win[0].objective);
    }
    public bool SeeIfConditionMetEndGame()
    {
        return CheckCondition(localdatabase[0].EndGame[0].condition, localdatabase[0].EndGame[0].objective);
    }
    public bool SeeIfConditionMetBonusCondition()
    {
        return CheckCondition(localdatabase[0].BonusCondition[0].condition, localdatabase[0].BonusCondition[0].objective);
    }
    //------------------- CheckCondition bool ----------------//
    private bool CheckCondition(string x, string y)
    {
        switch (x)
        {
            case "time":
                if (Timer.timerLength <= int.Parse(y))
                    return true;
                break;
            case "found"://Use only for BonusCondition
                if ((foundValue % int.Parse(y)) == 0)
                    return true;
                break;
            case "foundonce":
                if (foundValue >= int.Parse(y))
                    return true;
                break;
            case "breaknest":
                if ((float)brokenNestCounter / nestCount * 100 <= int.Parse(y))
                    return true;
                break;
            case "empty":
            default:
                break;
        }
        return false;
    }
    public void button()//For mobile gaming!
    {
        buttonpressed = true;
    }
    private void SetTargetObject()
    {
        dighardness = Random.Range(1, 5);
    }
    private bool StartDigging()
    {
        bool OnDigArea = false;
        switch (radius)
        {
            case 0:
            case 1:
                radiusText = "Very Cold";
                break;
            case 2:
                radiusText = "Cold";
                break;
            case 3:
                radiusText = "Hot";
                break;
            case 4:
                radiusText = "Very Hot";
                break;
            case 5:
                radiusText = dighardness.ToString();
                OnDigArea = true;
                break;
        }
        movement.enabled = false;
        buttonpressed = false;
        radiusSet = true;
        return OnDigArea;
    }
    private void StopDigging()
    {
        objectplacer.deleteTargetObject();
        objectplacer.newTargetedObject();
        SetTargetObject();
        foundValue += localdatabase[0].Score[0].xitem;
        objectFound = true;
        diggingObject = false;
        if (SeeIfConditionMetBonusCondition())
            upgrade = true;
    }
    private void calculateNests()
    {
        brokenNestCounter = CheckNestCount();
    }
}
    
//------------------- End Of Script----------------//
//----------------------------------------------//
