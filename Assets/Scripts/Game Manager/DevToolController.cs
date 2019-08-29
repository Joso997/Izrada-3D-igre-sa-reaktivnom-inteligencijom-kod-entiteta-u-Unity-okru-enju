using Controllers;
using UnityEngine.UI;
using UnityEngine;

class DevToolController : ScriptController, ControllersTemplate
{
    InputField InputField1;
    InputField InputField2;
    Button Button1;
    Text TextType;
    public bool showDevTools = false;

    void LockInput1(InputField input)
    {
        HotAndColdController.movement.values["speed"] = float.Parse(input.text);
    }
    void LockInput2(InputField input)
    {
        PredatorMovement[] allChildren_nest = GameObject.FindGameObjectWithTag("Nests").transform.GetComponentsInChildren<PredatorMovement>();
        foreach (PredatorMovement predatorMovement in allChildren_nest)
            predatorMovement.Speed = float.Parse(input.text);
    }
    public void SetPlayerDevTools()
    {
        InputField1.text = HotAndColdController.movement.values["speed"].ToString();
    }
    public void SetPredatorDevTools()
    {
        float speed = GameObject.FindGameObjectWithTag("Nests").transform.GetComponentInChildren<PredatorMovement>().Speed;
        InputField2.text = speed.ToString();
    }
    private void ChangeMovement()
    {
        TextType.text = "Movement Type: " + (HotAndColdController.movement.ChangeMovement() ? "B" : "A");
    }
    public void StartScript()
    {
        CanvasController.Canvas.transform.Find("DevToolsButton/Button").GetComponent<Button>().onClick.AddListener(() => ActOnDevTools());
        InputField1 = CanvasController.Canvas.transform.Find("DevToolsUI/Item1/InputField").GetComponent<InputField>();
        InputField2 = CanvasController.Canvas.transform.Find("DevToolsUI/Item2/InputField").GetComponent<InputField>();
        Button1 = CanvasController.Canvas.transform.Find("DevToolsUI/Item3/Button").GetComponent<Button>();
        TextType = CanvasController.Canvas.transform.Find("DevToolsUI/Item3/Text").GetComponent<Text>();
        InputField1.onEndEdit.AddListener(delegate { LockInput1(InputField1); });
        InputField2.onEndEdit.AddListener(delegate { LockInput2(InputField2); });
        Button1.onClick.AddListener(delegate { ChangeMovement(); });
        HotAndColdController.movement.movementLoaded = this.SetPlayerDevTools;
        SetPredatorDevTools();
        Reset();
    }
    public void Reset()
    {
        showDevTools = false;
        CanvasController.Canvas.transform.Find("DevToolsUI").gameObject.SetActive(false);
        TextType.text = "Movement Type: A";
    }

    public void ActOnDevTools()
    {
        if (!LevelController.IfEndGame())
        {
            if (showDevTools)
                showDevTools = false;
            else
                showDevTools = true;
            //
            if (showDevTools)
                LevelController.OpenMenu();
            else
                LevelController.CloseMenu();
            CanvasController.Canvas.transform.Find("DevToolsUI").gameObject.SetActive(showDevTools);
        }
        else
        {
            LevelController.ExitLevel();
        }

    }
}
