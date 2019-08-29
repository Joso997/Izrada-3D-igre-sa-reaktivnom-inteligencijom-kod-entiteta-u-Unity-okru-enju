using UnityEngine;

 class EventController : ScriptController
{
    //private GameObject Player;   
    void Awake()
    {
        //Player = GameObject.FindGameObjectWithTag("Player");
        DisableHotAndColdGameScripts();
    }
    /*void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            LevelController.RunStoryMode(Player.GetComponentInChildren<TagReader>().sender, Player);
        }    
    }*/
    
}
