using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    GameObject MainScreen;
    GameObject CreditScreen;

    public void Start()
    {
        MainScreen = GameObject.Find("/UI/Canvas/MainScreen");
        CreditScreen = GameObject.Find("/UI/Canvas/CreditScreen");
    }

    //------------------- Main Menu ----------------//
    public void MainStartBtn(string Testing) {

        SceneController.Instance.Unload(this.gameObject.scene.name);
        SceneController.Instance.Load(Testing);       
    }

    public void CreditsOnBtn()
    {
        MainScreen.SetActive(false);
        CreditScreen.SetActive(true);
    }

    public void CreditsOffBtn()
    {
        MainScreen.SetActive(true);
        CreditScreen.SetActive(false);
    }

    public void MainExitBtn() {
		Application.Quit();
	}

	//------------------- End Screen ----------------//
	public void EndExitBtn(string MainMenu) {

		SceneManager.LoadScene(MainMenu);
	}

	public void EndTryBtn() {

		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
//------------------- End Of Script----------------//
//----------------------------------------------//
