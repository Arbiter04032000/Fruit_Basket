using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourManager : MonoBehaviour
{
    //  reference to baskets
    public GameObject[] arrayOfBaskets;
    
    //  reference to materials
    public List<Material> colourMats, colourMatsStored;

    //  generates random number from 1-3
    private int randomNumber;

    private int colourMax;

    /// <summary>
    /// Implemented to Assign Colour to baskets at the beginning of each round
    /// </summary>

    //Singleton
    private static ColourManager instance;
    public static ColourManager Instance { get { return instance;  } }

    void Awake()
    {
        instance = this;
        //Populates storage list with main list
        //colourMatsStored = colourMats;
        foreach(Material mat in colourMats)
        {
            colourMatsStored.Add(mat);
        }
    }

    private void fillBaskets()
    {
        //Repopulates main list from storage
        //colourMats = colourMatsStored;
        foreach (Material mat in colourMatsStored)
        {
            colourMats.Add(mat);
        }

        //  fill arrayOfBaskets
        arrayOfBaskets = GameObject.FindGameObjectsWithTag("Basket");

        //  max limit of colours
        colourMax = arrayOfBaskets.Length / colourMats.Count;

        for(int i = 0; i < arrayOfBaskets.Length; i++)
        {
            int randomNumber = Random.Range(0, colourMats.Count);
            arrayOfBaskets[i].transform.GetChild(0).GetComponent<Renderer>().material = colourMats[randomNumber];
            check();
        }
    }

    void check()
    {
        int increment = 0;
        for(int i = 0; i < colourMats.Count; i++)
        {
            for(int j = 0; j < arrayOfBaskets.Length; j++)
            {
                // material of basket chilkd
                Material childMat = arrayOfBaskets[j].transform.GetChild(0).GetComponent<Renderer>().sharedMaterial;

                if(colourMats[i] == childMat)
                {
                    increment++;
                }
            }

            // if the colour appeared 4 times
            if(increment == colourMax)
            {
                Debug.Log("Incremnet" + increment);
                colourMats.RemoveAt(i);
            }
            increment = 0;
        }
    }

    void assignPrefab()
    {
        //if(gameObject.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial == )
        {

        }
    }

    // Update is called once per frame
    void Start()
    {
       fillBaskets();
       Debug.Log("Started Game!");
    }
}
