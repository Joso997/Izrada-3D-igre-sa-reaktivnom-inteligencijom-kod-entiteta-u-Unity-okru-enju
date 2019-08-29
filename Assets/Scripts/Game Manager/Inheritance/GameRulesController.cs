using UnityEngine;
using NamespaceGameRules;
using System.Collections.Generic;

 class GameRulesController : GameRulesDatabase {
    public List<GameRulesList> localdatabase = new List<GameRulesList>(); 
    
    // Use this for initialization
    public void StartController (int ruleID) {
        localdatabase.RemoveRange(0, localdatabase.Count);//Removes previous rules
        localdatabase.Add(FetchRulesByID(ruleID));//Change ID to change rules
    } 
}
