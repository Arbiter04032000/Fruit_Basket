using System;
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
    public GameObject pauseMenu;
    public GameObject gameUI;

    //SINGLETON
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    void Awake()
    {
        instance = this;
        isPaused = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GameMan.Instance.Score.ToString() + " s \n" + Mathf.Floor(GameMan.Instance.ReturnTime).ToString() + " t");
        printText.text = "Start";
        UpdateScore();
        UpdateBall();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GameMan.Instance.Score.ToString() + " s \n" + Mathf.Floor(GameMan.Instance.ReturnTime).ToString() + " t");
        //PrintUI(printTextLines.ToString());
        UpdateTimer();
    }

    //Update all score texts - All texts share format of Text: <val>, where the value output is coloured
    public void UpdateScore()
    {
        foreach (Text stext in scoreTextList) stext.text = ("Score: <color=#FFE620ff>" + GameMan.Instance.Score.ToString() + "</color>");
    }

    //Update all ball texts
    public void UpdateBall()
    {
        foreach (Text btext in ballTextList) btext.text = ("Balls left: <color=#41F86Aff>" + GameMan.Instance.ballCount.ToString() + "</color>");
    }

    //Update all timer texts
    public void UpdateTimer()
    {
        foreach (Text ttext in timerTextList) ttext.text = ("Time: <color=#00ffffff>" + (Math.Round(GameMan.Instance.TimeElapsed, 2).ToString("f2") + "</color>s"));
        
    }

    
    public void PrintUI(string text)
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

    public void Pause()
    {
        if(isPaused == false)
        {
            pauseMenu.SetActive(true);
            gameUI.SetActive(false);
            isPaused = true;
            Time.timeScale = 0;
            PrintUI("Game paused!");
            return;
        }
        else
        {
            pauseMenu.SetActive(false);
            gameUI.SetActive(true);
            isPaused = false;
            Time.timeScale = 1;
            PrintUI("Game resumed!");
            return;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
