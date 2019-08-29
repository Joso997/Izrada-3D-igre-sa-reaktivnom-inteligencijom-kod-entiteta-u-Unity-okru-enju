using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

 class LevelController : LevelDatabase
{
    private Vector3 test;
    private int id_Rule = 0;
    private int id_Level = 0;
    private int id_Map = 0;
    public void Run(int id_rule, int id_level, int id_map)
    {
        id_Rule = id_rule;
        id_Level = id_level;
        id_Map = id_map;
        SceneController.Instance.Unload("LevelMenu");
        SceneController.Instance.Load("Level"+ id_Map);
        OnEnable();
    }
    public void RunStoryMode(GameObject objectRead, GameObject Player)
    {
        test = new Vector3(-10, 0, 5);
        Player.transform.position = objectRead.transform.position + test;
        EnableHotAndColdGameScripts();
        HotAndColdController.ruleID = 0; //to be edited
        FinalizingLoadLevel();
    }
    void Update()//need to edit(clean up)
    {
        if ((IfEndGame() && Timer.stopTimer == false) || HotAndColdController.diggingFunction.eaten)
        {
            if (HotAndColdController.SeeIfConditionMetWin() && HotAndColdController.diggingFunction.eaten == false)
            {
                if (HotAndColdController.localdatabase[0].Win[0].condition == "time")
                {
                    HotAndColdController.scoreValue = Timer.timerLength;
                    if (HotAndColdController.scoreValue < database[id_Level].Score || database[id_Level].Score == 0)
                        database[id_Level].Score = HotAndColdController.scoreValue;
                }
                else
                {
                    HotAndColdController.scoreValue = HotAndColdController.foundValue;
                    database[id_Level].Score = HotAndColdController.scoreValue > database[id_Level].Score ? HotAndColdController.scoreValue : database[id_Level].Score;
                }
                database[id_Level].Pass = true;
            }
            OpenMenu();
            SaveController.SaveLevelChanges(database);
        }
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Load();
        StartLevel();
        SetButtons();
    }
    private void Load()
    {
        EnableHotAndColdGameScripts();
        SetRule(id_Rule);
        FinalizingLoadLevel();
        InitializePositionAndStats();
    }
    private void SetButtons()
    {
        Button buttonRestart = CanvasController.Canvas.Find("DigUI/EndScreen/End-Screen/EndTryBtn").GetComponent<Button>();
        buttonRestart.onClick.AddListener(RestartLevel);
        Button buttonExit = CanvasController.Canvas.Find("DigUI/EndScreen/End-Screen/EndExitBtn").GetComponent<Button>();
        buttonExit.onClick.AddListener(ExitLevel);
    }
    private void StartLevel()
    {
        GetComponent<ObjectPlacer>().StartScript();
        HotAndColdController.StartScript();
        CanvasController.StartScript();       
        ButtonController.StartScript();
        DevToolController.StartScript();
        //GetComponent<PredatorSpawner>().StartScript();     
    }
    public void RestartLevel()
    {
        DisableHotAndColdGameScripts();
        HotAndColdController.Reset();
        CanvasController.Reset();
        ButtonController.Reset();
        DevToolController.Reset();
        //GetComponent<PredatorSpawner>().Reset();
        HotAndColdController.diggingFunction.Reset();
        EnablePlayer();
        DeleteAll_AI();
        Load();
    }
    public void ExitLevel()
    {
        SceneController.Instance.Unload("Level"+ id_Map);
        SceneController.Instance.Load("LevelMenu");
        OnDisable();
        DisableHotAndColdGameScripts();
        GetComponent<ObjectPlacer>().deleteTargetObject();
        DeleteAll_AI();
    }
    public void OpenMenu()
    {
        CanvasController.EndScreen();
        Timer.stopTimer = true;
        DisablePlayer();
        StopAll_AI();
    }
    public void CloseMenu()
    {
        CanvasController.ExitScreen();
        Timer.stopTimer = false;
        EnablePlayer();
        ResumeAll_AI();
    }
    public bool IfEndGame()
    {
        return HotAndColdController.SeeIfConditionMetEndGame(); 
    }
    public SaveController getSaveController()
    {
        return SaveController;
    }
}
