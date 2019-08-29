using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
using System.IO;
using UnityEngine.EventSystems;
using System;


public class DialogueEditor : MonoBehaviour {

    public Button button;
    public GameObject content;
    private InputField titleInputField;
    private InputField textInputField;
    private Toggle activatedCheckBox;
    private Toggle completedCheckBox;
    private List<DialogueList> database = new List<DialogueList>();
    private JsonData dialogueData;
    private int id_selected;
    
    // Use this for initialization
    void Start () {
        titleInputField = GameObject.Find("Title InputField").GetComponent<InputField>();
        textInputField = GameObject.Find("Text InputField").GetComponent<InputField>();
        activatedCheckBox = GameObject.Find("Activated CheckBox").GetComponent<Toggle>();
        completedCheckBox = GameObject.Find("Completed CheckBox").GetComponent<Toggle>();
        dialogueData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/DialogueClassic.json"));
        //Construct on Start
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

    public void SelectFromList()
    {
        id_selected = Int32.Parse(EventSystem.current.currentSelectedGameObject.name);
        FetchDialogueByID(id_selected);
        titleInputField.text = database[id_selected].Title;
        textInputField.text = database[id_selected].Description;
        activatedCheckBox.isOn = database[id_selected].Activated;
        completedCheckBox.isOn = database[id_selected].Completed;
        Debug.Log("Selected Successfully");
    }

    public void AddToDatabase()
    {
        database.Add(new DialogueList(database.Count, titleInputField.text, textInputField.text, activatedCheckBox.isOn, completedCheckBox.isOn));
        ClearInputField();
        Debug.Log("Added Successfully");
    }

    public void DeleteFromDatabase()
    {
        database.RemoveAt(id_selected);
        ClearInputField();
        Debug.Log("Deleted Successfully");
    }

    public void ChangeSelectedDatabase()
    {
        database[id_selected].Title = titleInputField.text;
        database[id_selected].Description = textInputField.text;
        database[id_selected].Activated = activatedCheckBox.isOn;
        database[id_selected].Completed = completedCheckBox.isOn;
        ClearInputField();
        Debug.Log("Updated Successfully");
    }

    public void RefreshDatabase()
    {
        WriteToJsonFile();
        database.Clear();
        foreach (Transform child in content.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        dialogueData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/DialogueClassic.json"));
        ConstructDialogueDatabase();
    }

    public void ClearInputField()
    {
        titleInputField.text = "";
        textInputField.text = "";
    }

    public void WriteToJsonFile()
    {
        dialogueData = JsonMapper.ToJson(database);
        File.WriteAllText(Application.dataPath + "/StreamingAssets/DialogueClassic.json", dialogueData.ToString());
        Debug.Log("Saved Successfully");
    }

    void ConstructDialogueDatabase()
    {
        int height = 5;
        for (int i = 0; i < dialogueData.Count; i++)
        {
            database.Add(new DialogueList((int)dialogueData[i]["ID"], dialogueData[i]["Title"].ToString(), dialogueData[i]["Description"].ToString(),
                (bool)dialogueData[i]["Activated"], (bool)dialogueData[i]["Completed"]));
            var button_script = Instantiate(button, Vector3.zero, Quaternion.identity) as Button;
            button_script.name = i.ToString();
            button_script.GetComponentInChildren<Text>().text = dialogueData[i]["Title"].ToString();
            button_script.transform.SetParent(content.transform, false);
            button_script.transform.position = new Vector3(content.transform.position.x, content.transform.position.y - height, 0);
            button_script.onClick.AddListener(SelectFromList);
            height += 30;
        }
    }
}
