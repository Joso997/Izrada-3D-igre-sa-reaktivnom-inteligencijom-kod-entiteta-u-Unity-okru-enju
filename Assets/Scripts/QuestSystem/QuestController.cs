using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuestController : MonoBehaviour {
    
    QuestDatabase database;
    public List<QuestList> quests = new List<QuestList>();
    // Use this for initialization
    void Start () {

        database = GetComponent<QuestDatabase>();
        //CreatePlayerQuestDatabase();
    }

    public void CreatePlayerQuestDatabase() {

        //Debug.Log(QuestToAdd[0].ID);

    }

    //NPC provjerava Status Questa
    public bool CheckQuestStatus(string code) {

        bool endquest = false;
        QuestList QuestToAdd = FetchQuestByCode(code, endquest);

        if (QuestToAdd == null) {

            return true;
        }
        else {

            return false;
        }
    }
    //Starta Quest i učitava ga u osobnu bazu
	   public void StartQuest(string code) {

        QuestList QuestToAdd = database.FetchQuestByCode(code);
        quests.Add(new QuestList(QuestToAdd.ID, QuestToAdd.Title, QuestToAdd.Description, QuestToAdd.Activated = true, QuestToAdd.Completed, QuestToAdd.Type, QuestToAdd.Code));
    }

    //Ažuriranje Quest
    public void UpdateQuest(string code) {

    }

    //Završava Quest
    public void EndQuest(string code) {

        bool endquest = true;
        QuestList QuestToAdd = FetchQuestByCode(code, endquest);
        QuestToAdd.setting();
    }

    //Provjera je li Quest završen (ako jest ažurira se baza)
    public QuestList FetchQuestByCode(string code, bool endquest) {

        for (int i = 0; i < quests.Count; i++) {

            if (quests[i].Code == code) {

                if (endquest) {

                    quests[i].Completed = true;
                    quests[i].Activated = false;
                }
                else {
                    return quests[i];
                }
            }
        }
        return null;
    }
}
