using UnityEngine;
using System.Collections.Generic;
using NamespaceLevels;

 class LevelDatabase : ScriptController
{

    public List<LevelsList> database = new List<LevelsList>();//Will be changed
    private Levels jsonlist;
    void Awake()//To Be Edited
    {
        string itemData = JsonFileReader.LoadJsonAsResource("/StreamingAssets/Levels.json");
        jsonlist = JsonUtility.FromJson<Levels>(itemData);
        for (int i = 0; i < jsonlist.Level.Count; i++)
        {
            database.Add(jsonlist.Level[i]);
        }
    }
    public void ResetLevels()
    {
        database.RemoveRange(0, database.Count);
        string itemData = JsonFileReader.LoadJsonAsResource("/StreamingAssets/LevelsSave.json");
        jsonlist = JsonUtility.FromJson<Levels>(itemData);
        for (int i = 0; i < jsonlist.Level.Count; i++)
        {
            database.Add(jsonlist.Level[i]);
        }
    }
    /*public LevelsList FetchRulesByID(int id)//WILL BE EDITED
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].ID == id)
            {
                return database[i];
            }
        }
        return null;
    }*/
}

namespace NamespaceLevels
{
    [System.Serializable]
    public class Levels
    {
        public List<LevelsList> Level;
        public Levels(List<LevelsList> level)
        {
            this.Level = level;
        }
    }
    [System.Serializable]
    public class LevelsList
    {
        public int ID;
        public int id_Rule;
        public int id_Map;
        public bool Pass;
        public int num_Stars;
        public float Score;
        public LevelsList(int id, int id_rule, int id_map, int num_stars)
        {
            this.ID = id;
            this.id_Rule = id_rule;
            this.id_Map = id_map;
            this.num_Stars = num_stars;
            this.Pass = false;
        }
    }
}
