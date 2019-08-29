using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueController : MonoBehaviour {

    DialogueDatabase database;
    public List<DialogueList> quests = new List<DialogueList>();
    // Use this for initialization
    void Start () {

        database = GetComponent<DialogueDatabase>();
        //CreatePlayerQuestDatabase();
    }

    public void CreatePlayerDialogueDatabase() {

        //Debug.Log(QuestToAdd[0].ID);

    }

    //NPC provjerava Status Questa
    /*public bool CheckQuestStatus(string code) {

        bool endquest = false;
        //DialogueList QuestToAdd = FetchQuestByCode(code, endquest);

        if (QuestToAdd == null) {

            return true;
        }
        else {

            return false;
        }
    }*/
    //Starta Quest i učitava ga u osobnu bazu
	   /*public void StartQuest(string code) {

        DialogueList QuestToAdd = database.FetchQuestByCode(code);
        quests.Add(new DialogueList(QuestToAdd.ID, QuestToAdd.Title, QuestToAdd.Description, QuestToAdd.Activated = true, QuestToAdd.Completed, QuestToAdd.Type, QuestToAdd.Code));
    }*/

    //Ažuriranje Quest
    public void UpdateQuest(string code) {

    }

    //Završava Quest
    public void EndQuest(string code) {

        bool endquest = true;
        //DialogueList QuestToAdd = FetchQuestByCode(code, endquest);
    }

    //Provjera je li Quest završen (ako jest ažurira se baza)
    /*public DialogueList FetchQuestByCode(string code, bool endquest) {

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
    }*/
}
