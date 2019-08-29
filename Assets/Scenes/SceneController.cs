using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public static SceneController Instance;//Need to be edited

	void Awake () {
        Instance = this;
        Load("MainMenu");
    }
	
	public void Load (string sceneName) {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
    }

    public void Unload(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
            SceneManager.UnloadSceneAsync(sceneName);
    }
}
