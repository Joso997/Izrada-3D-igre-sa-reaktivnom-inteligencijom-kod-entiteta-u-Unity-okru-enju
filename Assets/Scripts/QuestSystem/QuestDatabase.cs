using UnityEngine;
using LitJson;
using System.Collections.Generic;
using System.IO;

public class QuestDatabase : MonoBehaviour
{
    private List<QuestList> database = new List<QuestList>();
    private JsonData itemData;

    void Awake()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Quests.json"));
        ConstructItemDatabase();
        //Debug.Log(FetchItemByID(0).Description);
    }

    public QuestList FetchQuestByID(int id)
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

    public QuestList FetchQuestByCode(string code)
    {
        for (int i = 0; i < database.Count; i++)
        {
            if (database[i].Code == code)
            {
                return database[i];
            }
        }
        return null;

    }

    void ConstructItemDatabase()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            database.Add(new QuestList((int)itemData[i]["id"], itemData[i]["title"].ToString(), itemData[i]["description"].ToString(),
                (bool)itemData[i]["activated"], (bool)itemData[i]["completed"], itemData[i]["type"].ToString(), itemData[i]["code"].ToString()));
        }
    }
}

public class QuestList
{
    //For communicating with the outside World
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool Activated { get; set; }
    public bool Completed { get; set; }
    public string Type { get; set; }
    public string Code { get; set; }

    //Makes a list
    public QuestList(int id, string title, string description, bool activated, bool completed, string type, string code)
    {
        this.ID = id;
        this.Title = title;
        this.Description = description;
        this.Activated = activated;
        this.Completed = completed;
        this.Type = type;
        this.Code = code;
    }

    public QuestList()
    {
        this.ID = -1;
    }
    public bool setting()
    {
        return true;
    }
}