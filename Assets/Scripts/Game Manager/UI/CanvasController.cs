using Controllers;
using UnityEngine;
using UnityEngine.UI;

 class CanvasController : ScriptController, ControllersTemplate
{
    //------------------- Variables ----------------//
    public Transform Canvas;
    private GameObject digUI;
    private GameObject mobileUI;
    private GameObject endScreen;
    private GameObject digItems;
    private Text scoreText;
    private Text nestText;
    private Text nestUI;
    private Text radiusTextComponent;
    private Text foundObjectText;
    private Text scoreEndValueTxt;
    private Text timerText;
    private float popUp = 0;
    private PredatorSpawner predatorSpawner;

    //------------------- Awake Void ----------------//
    void Awake () {//Need to edit, only while testing
        if (GameObject.FindGameObjectWithTag("Canvas") != null)
        {
            Canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Transform>();
            digUI = Canvas.Find("DigUI").gameObject;
            mobileUI = Canvas.Find("MobileUI").gameObject;
            digItems = Canvas.Find("DigUI/DigItems").gameObject;
            endScreen = Canvas.Find("DigUI/EndScreen/End-Screen").gameObject;
            scoreEndValueTxt = Canvas.Find("DigUI/EndScreen/EndScoreValue").GetComponent<Text>();
            radiusTextComponent = Canvas.Find("DigUI/DigItems/RadiusTxt").GetComponent<Text>();
            foundObjectText = Canvas.Find("DigUI/DigItems/FoundObjectTxt").GetComponent<Text>();
            timerText = Canvas.Find("DigUI/DigItems/CountdownValue").GetComponent<Text>();
            scoreText = Canvas.Find("DigUI/DigItems/ScoreTxtValue").GetComponent<Text>();
            nestText = Canvas.Find("DigUI/DigItems/NestTxtValue").GetComponent<Text>();
            nestUI = Canvas.Find("DigUI/DigItems/NestTxt").GetComponent<Text>();
            predatorSpawner = GetComponent<PredatorSpawner>();
            
#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
            mobileUI.SetActive(true);
            mobileUI.transform.Find("Dig").gameObject.GetComponent<Button>().onClick.AddListener(() => HotAndColdController.button());
            mobileUI.transform.Find("Jump").gameObject.GetComponent<Button>().onClick.AddListener(() => HotAndColdController.movement.Jump());
            mobileUI.transform.Find("Hide").gameObject.GetComponent<Button>().onClick.AddListener(() => HotAndColdController.diggingFunction.Hide());
#else
            mobileUI.SetActive(false);
#endif
        }
    }
    void SetUI()
    {
        digItems.SetActive(true);
        scoreEndValueTxt.enabled = false;
        endScreen.SetActive(false);
        digUI.SetActive(false);
        radiusTextComponent.enabled = false;
        foundObjectText.enabled = false;
        scoreText.text = 0.ToString();
        scoreEndValueTxt.text = 0.ToString();
        nestText.text = HotAndColdController.nestCount.ToString();
    }
    //------------------- Start Void ----------------//
    public void StartScript()
    {
        Awake();
        Reset();
    }
    public void Reset()
    {
        SetUI();
        ClassicModeActivate();
    }
    //------------------- Update Void ----------------//
    void FixedUpdate () {
        if (LevelController.IfEndGame())
        {
            EndScreen();
        }else
        {
            timerText.text = Timer.timerLength.ToString("f0");
        }
        if (HotAndColdController.objectFound == true)
        {
            ShowFoundObjText();
            popUpadd();
        }
        if (HotAndColdController.radiusSet == true)
        {
            ShowRadiusText();
            popUpadd();
        }
        if (HotAndColdController.CalculateStopTime(popUp))
        {           
            HideRadiusText();
            HideFoundObjText();
            popUp = 0;
        }
            UpdateNestCountText();//NEED TO BE CHANGED
    }
    //------------------- EndScreen Void ----------------//
    public void ExitScreen()
    {
        endScreen.SetActive(false);       
        digItems.SetActive(true);
        scoreEndValueTxt.enabled = false;
    }
    public void EndScreen()
    {
        endScreen.SetActive(true);
        digItems.SetActive(false);
        scoreEndValueTxt.text = HotAndColdController.foundValue.ToString();
        scoreEndValueTxt.enabled = true;
        Text txt = Canvas.Find("DigUI/EndScreen/End-Screen/TimeIsUpTxt").GetComponent<Text>();
        if (HotAndColdController.SeeIfConditionMetWin())
            txt.text = "LEVEL COMPLETED";
        else if (HotAndColdController.diggingFunction.eaten)
            txt.text = "PLAYER EATEN";
        else if((LevelController.IfEndGame() && Timer.stopTimer == false))
            txt.text = "TIME IS UP";
        else if(!(LevelController.IfEndGame()))
            txt.text = "GAME IS PAUSED";
    }
    //------------------- ClassicModeActivate Void ----------------//
    void ClassicModeActivate()
    {
        digUI.SetActive(true);
    }
    //------------------- ShowRadiusText Void ----------------//
    void ShowRadiusText()
    {
        radiusTextComponent.enabled = true;
        radiusTextComponent.text = HotAndColdController.radiusText.ToString();
    }
    //------------------- HideRadiusText Void ----------------//
    void HideRadiusText()
    {
        HotAndColdController.radiusSet = false;
        radiusTextComponent.enabled = false;
    }
    //------------------- ShowFoundObjText Void ----------------//
    void ShowFoundObjText()
    {
        scoreText.text = HotAndColdController.foundValue.ToString();
        foundObjectText.enabled = true;        
    }
    //------------------- HideFoundObjText Void ----------------//
    void HideFoundObjText()
    {
        foundObjectText.enabled = false;
        HotAndColdController.objectFound = false;
    }
    void UpdateNestCountText()
    {
        nestText.text = HotAndColdController.brokenNestCounter.ToString();
    }
    void HideNestCountText()
    {

    }
    //------------------- popUpadd Void ----------------//
    void popUpadd()
    {
        if(popUp == 0)
        {
            popUp += Time.fixedTime;
        }      
    }
    
}
