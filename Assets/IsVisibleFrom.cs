using UnityEngine;

public class IsVisibleFrom : MonoBehaviour
{
    Camera mainCamera;
    Transform Predator;
    public bool onScreen;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Predator = GameObject.FindGameObjectWithTag("PredatorSpawner").GetComponent<Transform>().Find("PredatorSpawner").GetComponent<Transform>();
    }

    void Update()
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(Predator.position);
        onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}