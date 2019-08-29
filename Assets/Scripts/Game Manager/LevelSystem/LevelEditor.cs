using UnityEngine;
using UnityEngine.UI;

public class LevelEditor : MonoBehaviour {
    private Button button;
    private GameObject Panel;
    private Transform Canvas;
    private LevelController LevelController;
    private string[] naziv = { "practice", "collect at least 2 points", "collect 4 points inside 60 sec", "break ALL bird's nest" };
    private string[] ime = {"vježbaj", "skupi najmanje 2 boda", "skupi 4 boda unutar 60 sec", "razbi SVA ptičja gnijezda" };
    void Start()
    {
        LevelController = GameObject.FindGameObjectWithTag("GameController").GetComponent<LevelController>();
        Canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Transform>();
        Panel = Canvas.Find("Panel").gameObject;
        ConstructDialogueDatabase();
        Canvas.Find("Panel/Reset").gameObject.GetComponent<Button>().onClick.AddListener(() => LevelController.ResetLevels());
        Canvas.Find("Panel/Reset").gameObject.GetComponent<Button>().onClick.AddListener(() => ConstructDialogueDatabase());
        Canvas.Find("Panel/Reset").gameObject.GetComponent<Button>().onClick.AddListener(() => LevelController.getSaveController().SaveLevelChanges(LevelController.database));
        Canvas.Find("Panel/Back").gameObject.GetComponent<Button>().onClick.AddListener(() => Back());
    }

    public void Back()
    {
        SceneController.Instance.Unload(this.gameObject.scene.name);
        SceneController.Instance.Load("MainMenu");
    }

    void ConstructDialogueDatabase()
    {
        int height = 0;
        int width = 0;
        for (int i = 0; i < LevelController.database.Count; i++)
        {
            if (i % 3 == 0 && i != 0)
            {
                height -= 160;
                width = 0;
            }
            var button_script = Instantiate(Resources.Load("VitalObjects/UI/LevelCard"), Vector3.zero, Quaternion.identity) as GameObject;
            button_script.name = LevelController.database[i].ID.ToString();
            //button_script.GetComponentInChildren<Text>().text = "Level " + (i+1).ToString();
            button_script.transform.Find("Text").GetComponent<Text>().text = naziv[LevelController.database[i].ID].ToString();
            button_script.transform.Find("TextCro").GetComponent<Text>().text = ime[LevelController.database[i].ID].ToString();
            //Debug.Log(width);
            button_script.transform.SetParent(Panel.transform, false);
            button_script.transform.localPosition = (new Vector2(40 + width, -100 + height));
            int rule = LevelController.database[i].id_Rule;
            int id = LevelController.database[i].ID;
            int map = LevelController.database[i].id_Map;
            button_script.AddComponent<Button>().onClick.AddListener(() => LevelController.Run(rule,id,map));            
            if (LevelController.database[i].Pass)
            {
                button_script.GetComponent<Image>().color = Color.green;
                button_script.GetComponent<Transform>().Find("Score").GetComponent<Text>().text = "Highest Score: " + LevelController.database[i].Score.ToString();
            }               
            width += 160;                  
        }
    }
}
