using UnityEngine;
using System.Collections.Generic;
using NamespaceGameRules;

 class GameRulesDatabase : ScriptController
{
    private List<GameRulesList> database = new List<GameRulesList>();
    private GameRules jsonlist;
    void Awake()//To Be Edited Once GameRulesEditor Introduced
    {
        string itemData = JsonFileReader.LoadJsonAsResource("/StreamingAssets/GameRules.json");
        jsonlist = JsonUtility.FromJson<GameRules>(itemData);
        for (int i = 0; i < jsonlist.GameRule.Count; i++)
        {
            database.Add(jsonlist.GameRule[i]);
        }
    }
    public GameRulesList FetchRulesByID(int id)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].ID == id)
            {
                return database[i];
            }
        }
        return null;
    }
}
namespace NamespaceGameRules
{
    [System.Serializable]
    public class GameRules
    {
        public List<GameRulesList> GameRule;
        public GameRules(List<GameRulesList> gamerule)
        {
            this.GameRule = gamerule;
        }
    }
    [System.Serializable]
    public class GameRulesList
    {
        public int ID;
        public List<TimeClass> Time;
        public List<ScoreClass> Score;
        public List<ConditionClass> Win;
        public List<ConditionClass> EndGame;
        public List<ConditionClass> BonusCondition;
        public List<BonusClass> Bonus;
        public GameRulesList(int id, List<TimeClass> time, List<ScoreClass> score, List<ConditionClass> win, List<ConditionClass> endgame, List<ConditionClass> bonuscondition, List<BonusClass> bonus)
        {
            this.ID = id;
            this.Time = time;
            this.Score = score;
            this.Win = win;
            this.EndGame = endgame;
            this.BonusCondition = bonuscondition;
            this.Bonus = bonus;
        }
    }
    [System.Serializable]
    public class TimeClass
    {
        public int starttime;
        public int direction;
        public TimeClass(int _starttime, int _direction)
        {
            this.starttime = _starttime;
            this.direction = _direction;
        }
    }
    [System.Serializable]
    public class ScoreClass
    {
        public int xitem;
        public ScoreClass(int _xitem)
        {
            this.xitem = _xitem;
        }
    }
    [System.Serializable]
    public class ConditionClass
    {
        public string condition;
        public string objective;
        public ConditionClass(string _condition, string _objective)
        {
            this.condition = _condition;
            this.objective = _objective;
        }
    }
    [System.Serializable]
    public class BonusClass
    {
        public int bonustime;
        public float movementspeed;
        public int diggingspeed;
        public BonusClass(int _bonustime, int _movementspeed, int _diggingspeed)
        {
            this.bonustime = _bonustime;
            this.movementspeed = _movementspeed;
            this.diggingspeed = _diggingspeed;
        }
    }
}