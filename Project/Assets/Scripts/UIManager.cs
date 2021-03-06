﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //VARS
    public Text printText; //in-game console

    public List<Text> scoreTextList; //Score text boxes
    public List<Text> timerTextList; //Timer text boxes
    public List<Text> ballTextList; //Ball counter text boxes

    public int printTextMaxLines; //Max lines before console resets
    private int printTextLines; //Current # of lines

    private bool isPaused;
    public GameObject pauseMenu, startMenu, endMenu;
    public GameObject gameUI;
    public GameObject pauseButton, ballButton;

    public GameObject endMenuText;

    //SINGLETON
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    void Awake()
    {
        instance = this;
        isPaused = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(GameMan.Instance.Score.ToString() + " s \n" + Mathf.Floor(GameMan.Instance.ReturnTime).ToString() + " t");
        printText.text = "Start";
        UpdateScore();
        UpdateBall();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameMan.Instance.Score.ToString() + " s \n" + Mathf.Floor(GameMan.Instance.ReturnTime).ToString() + " t");
        //PrintUI(printTextLines.ToString());
        UpdateTimer();
    }

    //Update all score texts - All texts share format of "Text: <color>" + [val] + "</color>"
    public void UpdateScore()
    {
        foreach (Text stext in scoreTextList) stext.text = ("Score: <color=#FFE620ff>" + GameMan.Instance.Score.ToString() + "</color>");
    }

    //Update all ball texts
    public void UpdateBall()
    {
        foreach (Text btext in ballTextList) btext.text = ("Fruit left: <color=#41F86Aff>" + GameMan.Instance.ballCount.ToString() + "</color>");
    }

    //Update all timer texts
    public void UpdateTimer()
    {
        foreach (Text ttext in timerTextList) ttext.text = ("Time: <color=#00ffffff>" + (Math.Round(GameMan.Instance.TimeElapsed, 2).ToString("f2") + "</color>s"));
        
    }

    
    public void PrintUI(string text) //In-game console, will not be in final version
    {
        if (printText != null)
        {
            if (printTextLines < printTextMaxLines)
            {
                printText.text += "\n" + text;
                ++printTextLines;
                return;
            }
            else if (printTextLines >= printTextMaxLines)
            {
                printText.text = text;
                printTextLines = 1;
                return;
            }
            return;
        }
        else
        {
            Debug.LogError("Console has not been set!");
            return;
        }
    }

    public void Void()
    {
        printText.text = ("<color=002f59ff>Text cleared!</color>");
        printTextLines = printTextMaxLines;
    }

    public void Pause(bool noPause)
    {
        if(isPaused == false)
        {
            pauseMenu.SetActive(true); //Activates menu, making it visible and interactive
            gameUI.SetActive(false); //Disables game UI
            isPaused = true; //Bool to detect if menu is open
            Time.timeScale = 0; //0 disables "Time"
            PrintUI("Game paused!");

            if(noPause == true)
            {
                pauseButton.SetActive(false); //Disables the unpause button if boolean is true - This is to prevent unpausing after gameend
                ballButton.SetActive(false);
            }
            return;
        }
        else
        {
            pauseButton.SetActive(true); //Re-enables pause button, for future menu opening - this must happen before entire menu is disabled
            ballButton.SetActive(true);
            pauseMenu.SetActive(false); //Disables menu
            startMenu.SetActive(false);
            gameUI.SetActive(true); //Disables game UI
            isPaused = false;
            Time.timeScale = 1; //1 is normal timescale
            PrintUI("Game resumed!");
            return;
        }
    }

    public void QuitGame()
    {
        Application.Quit(); //Oculus does not like this, treats it as a crash
    }

    //Function for end of game screen, will replace "nopause" functionally
    public void EndScreen(int endType)
    {
        endMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
        if(endType == 0)
        {
            endMenuText.transform.GetChild(0).GetComponent<Text>().text = "You filled all the baskets!";
        }
        if (endType == 1)
        {
            endMenuText.transform.GetChild(0).GetComponent<Text>().text = "You ran out of fruit!";
        }
        if (endType == 2)
        {
            endMenuText.transform.GetChild(0).GetComponent<Text>().text = "You ran out of time!";
        }

        endMenuText.transform.GetChild(1).GetComponent<Text>().text = "With a score of " + GameMan.Instance.Score.ToString() + "...";
        endMenuText.transform.GetChild(2).GetComponent<Text>().text = "In " + (Math.Round(GameMan.Instance.TimeElapsed, 2).ToString("f2")) + " seconds...";
        endMenuText.transform.GetChild(3).GetComponent<Text>().text = "And " + GameMan.Instance.ballCount.ToString() + " fruit remaining!";
    }

    public void EndScreenRestart()
    {
        endMenu.SetActive(false);
        gameUI.SetActive(true);
        Time.timeScale = 1;
        isPaused = false;
        GameMan.Instance.Restart();
    }
}
