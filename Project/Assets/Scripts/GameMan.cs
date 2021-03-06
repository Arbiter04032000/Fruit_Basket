﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using JMF.Systems.EventSystem.UnityRenderer;

public class GameMan : MonoBehaviour
{
    //vars
    private int score; //Current score
    private float time; //Seconds elapsed
    private bool gameEnd; //Disables game inputs on gameend
    private bool inHoop; //True when ball enters basket

    public Transform ballSpawn, ball;
    private List<GameObject> ballList;
    public GameObject ballfab, scene;
    public float timeLimit = 10f; //Seconds for forced gameend
    public UnityEvent timeUp;
    public int ballMax, ballCount;
    public GameObject grapePrefab, applePrefab, dFruitPrefab, pineapplePrefab;



    //SINGLETONS
    private static GameMan instance;
    public static GameMan Instance { get { return instance; } }

    //On game launch
    private void Awake()
    {
        instance = this;
        ballList = new List<GameObject>();
    }

    

    //Reset
    public void Restart()
    {
        score = 0; //Reset score
        time = 0; //Reset timer
        gameEnd = false; //Reset gameend logic
        inHoop = false; //Reset ball tracker
        ballCount = ballMax; //Sets ball counter to the maximum on boot
        UIManager.Instance.PrintUI("Reset!");
        if (ballList.Count > 0) { foreach (GameObject obj in ballList) { Destroy(obj); } } //Destroys all balls in play...

        //Resets basket trigger
        foreach (GameObject basket in ColourManager.Instance.arrayOfBaskets)
        {
            basket.transform.GetChild(1).gameObject.SetActive(true);
        }
        // fill baskets with the correct colour
        ColourManager.Instance.fillBaskets();

        NewBall(); //And spawns a new one
        
    }

    public void ResetBall(bool ovrr) //Bool override determines whether reset function should ignore ballcount. TODO make cleaner and more efficient
    {
        UIManager.Instance.UpdateScore();
        if (ball && ballSpawn && ovrr == true)
        {
            ball.SetPositionAndRotation(ballSpawn.position, ballSpawn.rotation); //Reset ball to spawnpoint
            ball.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero; //Reset ball velocity; prevents weirdness
            ball.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; //Same as above
            UIManager.Instance.PrintUI("Balls left: <color=#ff0000ff>" + ballCount.ToString() + "</color>"); //Prints ball count to UI
        }
        if (ovrr != true) //If override is off, it treats it like a new ball, without actually spawning a new ball
        {
            if (ballfab && ballSpawn && ballCount <= 0)
            {
                UIManager.Instance.EndScreen(1); //Invokes game end functions
                UIManager.Instance.PrintUI("<color=#ff0000ff>Out of balls!</color>");
                return;
            }
            ball.SetPositionAndRotation(ballSpawn.position, ballSpawn.rotation); //Reset ball to spawnpoint
            ball.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero; //Reset ball velocity; prevents weirdness
            ball.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; //Same as above
            --ballCount; //Ticks down ball counter
            UIManager.Instance.UpdateBall(); //Updates counter in UI
            ball.GetComponentInChildren<ParticleSystem>().Play(); //Plays particle FX
            return;
        }
    }

    public void NewBall() //Function for spawning a new ball, instead of tping the current one
    {
        UIManager.Instance.UpdateScore();
        if (ballfab && ballSpawn && ballCount > 0)
        {
            Debug.Log(ballCount);
            AssignFruitPrefab();
            GameObject newball = Instantiate<GameObject>(ballfab , ballSpawn.position, Random.rotation, scene.transform); //Spawns ball in holder
            --ballCount; //Ticks down ball counter
            UIManager.Instance.UpdateBall(); //Updates counter in UI
            UIManager.Instance.PrintUI("Balls left: <color=#ff0000ff>" + ballCount.ToString() + "</color>"); //Displays new count
            ball = newball.transform; //This is to ensure the correct ball is affected by ResetBall
            ballList.Add(newball); //Adds ball to list of all balls in play
        }
        else
        {
            UIManager.Instance.EndScreen(1); //Invokes game end functions
            UIManager.Instance.PrintUI("<color=#ff0000ff>Out of balls!</color>");
            return;
        }

    }


    /// <summary>
    /// Assigns the correct prefab of the fruit to the ball holder
    /// depending on the colour mats stored
    /// Index 0: Apple
    /// Index 1: Grape
    /// Index 2: DFruit
    /// Index 3: Pineapple
    /// </summary>
    private void AssignFruitPrefab()
    {
        int randomColour = Random.Range(0, ColourManager.Instance.arrayOfBaskets.Length);
        //Debug.Log("RC" + randomColour);
        Material basketMaterial = ColourManager.Instance.arrayOfBaskets[randomColour].transform.GetChild(0).GetComponent<Renderer>().sharedMaterial;

        Material appleMaterial = ColourManager.Instance.colourMatsStored[0];
        Material grapesMaterial = ColourManager.Instance.colourMatsStored[1];
        Material dFruitMaterial = ColourManager.Instance.colourMatsStored[2];
        Material pineappleMaterial = ColourManager.Instance.colourMatsStored[3];

        if (basketMaterial == appleMaterial)
        {
            ballfab = applePrefab;
        }
        else if(basketMaterial == grapesMaterial)
        {
            ballfab = grapePrefab;
        }
        else if(basketMaterial == dFruitMaterial)
        {
            ballfab = dFruitPrefab;
        }
        else if (basketMaterial == pineappleMaterial)
        {
            ballfab = pineapplePrefab;
        }
    }

    /// <summary>
    /// Check if all baskets have been scored.
    /// </summary>
    public void checkForWin()
    {
        //  Store array of baskets
        GameObject[] arrayOfBaskets = GameObject.FindGameObjectsWithTag("Basket");

        //  to keep track of scored baskets
        int total = 0;

        //  search through each basket
        for(int i = 0; i < arrayOfBaskets.Length; i++)
        {
            //  check if each basket has been scored
            if(arrayOfBaskets[i].GetComponent<TagContainer>().scored == true)
            {
                //  increment total count
                total++;
            }
        }


        //Debug.Log("Baskets " + total);

        // if total equal to amount of baskets
        if (total == arrayOfBaskets.Length)
        {
            // end game
            UIManager.Instance.EndScreen(0);
        }

        // reset total for checking again
        total = 0;
    }

    
    void Start()
    {
        /*new WaitForFixedUpdate();
        Restart(); //Reset to initialize game values*/
    }

    //Timer - constantly ticks up as long as gameend has not been reached
    private bool Timer()
    {
        if(time >= timeLimit) //If it reaches the time limit, triggers game end
        {
            checkForWin();
            gameEnd = true;
            time = timeLimit;
            return true;
        }
        else
        {
            time += Time.deltaTime;
            return false;
        }
    }

    private void FixedUpdate()
    {
        if (!gameEnd) //If not gameend
        {
            
            if(Timer()) //Check if time has reached the limit
            {
                //timeUp.Invoke(); //Run game end event
                UIManager.Instance.EndScreen(2);
            }
        }
    }
    
    //Get score
    public int Score //Score property
    {
        get
        {
            return score; //Returns current score
        }
        set
        {
            if (inHoop) //Checks if ball is in hoop
            {
                score += value; //Add new value
                UIManager.Instance.PrintUI("Scored! <color=#FFE621ff>" + score.ToString() + "</color>");
            }
        }
    }


    public float TimeElapsed
    {
        get
        {
            return time;
        }
    }

    //Get time property
    public float ReturnTime { get { return time; } }

    //In hoop property
    public bool InHoop { get { return inHoop; } set { inHoop = value; } }

}
