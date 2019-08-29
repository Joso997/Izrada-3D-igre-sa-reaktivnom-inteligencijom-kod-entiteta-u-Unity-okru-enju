using Controllers;
using UnityEngine;
using UnityEngine.UI;

 class ButtonController : ScriptController, ControllersTemplate
{
    bool openMenu = false;
    public void StartScript()
    {
        CanvasController.Canvas.transform.Find("Menu").gameObject.GetComponent<Button>().onClick.AddListener(() => ActOnMenu());
        Reset();
    }
    public void Reset()
    {
        openMenu = false;
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            ActOnMenu();
        }
    }

	public void ActOnMenu()
    {
        if (!LevelController.IfEndGame())
        {
            if (openMenu)
                openMenu = false;
            else
                openMenu = true;
            //
            if (openMenu)
                LevelController.OpenMenu();
            else
                LevelController.CloseMenu();
        }else
        {
            LevelController.ExitLevel();
        }
        
    }
}
