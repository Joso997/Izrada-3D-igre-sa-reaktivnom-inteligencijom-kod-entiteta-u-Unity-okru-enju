using UnityEngine;
using Controllers;

/**/ class ScriptController : GetControllers
{
    protected void FinalizingLoadLevel()
    {
        LevelController.enabled = true;
    }
    // Use this for initialization
    protected void EnableHotAndColdGameScripts()
    {
        GetComponent<ObjectPlacer>().enabled = true;
        //GetComponent<PredatorSpawner>().enabled = true;
        HotAndColdController.enabled = true;      
        Timer.enabled = true;
        CanvasController.enabled = true;
        ButtonController.enabled = true;
        EventController.enabled = false;
    }
    protected void DisableHotAndColdGameScripts()
    {
        CanvasController.enabled = false;
        HotAndColdController.enabled = false;
        GetComponent<ObjectPlacer>().enabled = false;
        //GetComponent<PredatorSpawner>().enabled = false;
        Timer.enabled = false;
        Timer.stopTimer = false;
        LevelController.enabled = false;
        ButtonController.enabled = false;
        EventController.enabled = true;
    }
    protected void DisablePlayer()
    {
        HotAndColdController.movement.jumpEnabled = true;
        HotAndColdController.player.SetActive(false);
    }
    protected void EnablePlayer()
    {
        HotAndColdController.player.SetActive(true);
        HotAndColdController.movement.enabled = true;
        HotAndColdController.diggingFunction.eaten = false;
        HotAndColdController.movement.Reset();
    }
    protected void InitializePositionAndStats()
    {
        Transform Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Transform Respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>();
        Player.transform.position = Respawn.transform.position;
        Player.transform.rotation = Respawn.transform.rotation;
    }
    protected void SetRule(int id_rule)
    {
        HotAndColdController.ruleID = id_rule;
    }
    protected void StopAll_AI()
    {
        PredatorAI[] allChildren_nest = GameObject.FindGameObjectWithTag("Nests").transform.GetComponentsInChildren<PredatorAI>();
        foreach (PredatorAI child in allChildren_nest)
        {
            child.ShoutDownMovement();
            child.enabled = false;
        }
    }
    protected void ResumeAll_AI()
    {
        PredatorAI[] allChildren_nest = GameObject.FindGameObjectWithTag("Nests").transform.GetComponentsInChildren<PredatorAI>();
        foreach (PredatorAI child in allChildren_nest)
        { 
            child.enabled = true;
            child.StartUpMovement();
        }
    }
    protected void DeleteAll_AI()
    {
        PredatorAI[] allChildren_nest = GameObject.FindGameObjectWithTag("Nests").transform.GetComponentsInChildren<PredatorAI>();
        NestAI[] nestAI = GameObject.FindGameObjectWithTag("Nests").transform.GetComponentsInChildren<NestAI>();
        foreach (PredatorAI child in allChildren_nest)
        {
            child.FullReset();
            child.enabled = true;
            child.StartUpMovement();
        }
        foreach (NestAI child in nestAI)
        {
            child.Reset();
        }
        Debug.Log("----------------------GAME ENDED----------------------");
    }
    protected int CheckNestCount()
    {
        int count = 0;
        NestAI[] nestAI = GameObject.FindGameObjectWithTag("Nests").transform.GetComponentsInChildren<NestAI>();
        foreach (NestAI child in nestAI)
        {
            if (child.gameObject.GetComponent<MeshRenderer>().enabled)
                count++;
        }
        return count;
    }
}

