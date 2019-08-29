using UnityEngine;
using LitJson;
using System.Collections.Generic;
using System.IO;


    public class DialogueDatabase : MonoBehaviour
    {

        private List<DialogueList> database = new List<DialogueList>();
        private JsonData dialogueData;


        void Awake()
        {
            
            dialogueData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/DialogueClassic.json"));
            ConstructDialogueDatabase();
        }

        public DialogueList FetchDialogueByID(int id)
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

        void ConstructDialogueDatabase()
        {
            for (int i = 0; i < dialogueData.Count; i++)
            {
                database.Add(new DialogueList((int)dialogueData[i]["ID"], dialogueData[i]["Title"].ToString(), dialogueData[i]["Description"].ToString(),
                    (bool)dialogueData[i]["Activated"], (bool)dialogueData[i]["Completed"]));
            }
        }

    }

public class DialogueList
    {
        //For communicating with the outside World
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Activated { get; set; }
        public bool Completed { get; set; }

        //Makes a list
        public DialogueList(int id, string title, string description, bool activated, bool completed)
        {
            this.ID = id;
            this.Title = title;
            this.Description = description;
            this.Activated = activated;
            this.Completed = completed;
        }

        public DialogueList()
        {
            this.ID = -1;
        }
    }
