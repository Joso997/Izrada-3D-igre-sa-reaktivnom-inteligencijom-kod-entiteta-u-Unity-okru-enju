using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuUI : EventTrigger {

    GameObject startButton;
    GameObject startImage;
    GameObject startselectedImage;

    GameObject optionsButton;
    GameObject optionsImage;
    GameObject optionsselectedImage;

    GameObject exitButton;
    GameObject exitImage;
    GameObject exitselectedImage;

    int x = 0;

    // Use this for initialization
    void Start () {

        startButton = GameObject.Find("Start Button");
        startImage = GameObject.Find("Start");
        startselectedImage = GameObject.Find("Start Selected");
        
        optionsButton = GameObject.Find("Options Button");
        optionsImage = GameObject.Find("Options");
        optionsselectedImage = GameObject.Find("Options Selected");
       
        exitButton = GameObject.Find("Exit Button");
        exitImage = GameObject.Find("Exit");
        exitselectedImage = GameObject.Find("Exit Selected");

        Debug.Log(startselectedImage);
    }

    private void Update() {

        if(x == 0) {

            startselectedImage.SetActive(false);
            optionsselectedImage.SetActive(false);
            exitselectedImage.SetActive(false);

            x += 1;
        }
    }

    public override void OnPointerEnter(PointerEventData data) {

        if (this.gameObject == startButton)
        {
           
            startImage.SetActive(false);
            startselectedImage.SetActive(true);
        }

        if (this.gameObject == optionsButton)
        {
            
            optionsImage.SetActive(false);
            optionsselectedImage.SetActive(true);
        }

        if (this.gameObject == exitButton)
        {
            
            exitImage.SetActive(false);
            exitselectedImage.SetActive(true);
        }

    }

    public override void OnPointerExit(PointerEventData data) {

        if (this.gameObject == startButton)
        {
            startImage.SetActive(true);
            startselectedImage.SetActive(false);
        }

        if (this.gameObject == optionsButton)
        {
            optionsImage.SetActive(true);
            optionsselectedImage.SetActive(false);
        }

        if (this.gameObject == exitButton)
        {
            exitImage.SetActive(true);
            exitselectedImage.SetActive(false);
        }
    }
}
