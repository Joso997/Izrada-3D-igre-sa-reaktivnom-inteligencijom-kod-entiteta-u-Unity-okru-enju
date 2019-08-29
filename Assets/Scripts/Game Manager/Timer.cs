using UnityEngine;

public class Timer : MonoBehaviour {
    public float timerLength = 0f;//Needs to be made a property
    public int direction = 0;
    public bool stopTimer = false;

	// Update is called once per frame
	void Update () {
        if (stopTimer == false)
        {
            timerLength += Time.deltaTime * (direction);
        }
    }
}
