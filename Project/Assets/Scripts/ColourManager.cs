using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourManager : MonoBehaviour
{
    //  reference to baskets
    public GameObject[] arrayOfBaskets;
    
    //  reference to materials
    public List<Material> colourMats, colourMatsStored;

    //  reference to fruit prefabs
    public List<GameObject> fruitsArray;

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

    public void fillBaskets()
    {
        //Repopulates main list from storage
        //colourMats = colourMatsStored;
        if (colourMats.Count == 0)
        {
            foreach (Material mat in colourMatsStored)
            {
                colourMats.Add(mat);
            }
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

    /// <summary>
    /// Caps the amount of colour_mats assigned to baskets
    /// By Iterating through colour mat array and basket array.
    /// Iterating to check if a material is the same on both arrays
    /// Deleting the material in colour mat array if it appears more than specified
    /// </summary>
    void check()
    {
        int increment = 0;

        //  iterate through each colour mat
        for(int i = 0; i < colourMats.Count; i++)
        {
            //  iterate through each basket
            for(int j = 0; j < arrayOfBaskets.Length; j++)
            {
                // material of basket child
                Material childMat = arrayOfBaskets[j].transform.GetChild(0).GetComponent<Renderer>().sharedMaterial;

                //  if material matches material of basket
                if(colourMats[i] == childMat)
                {
                    if(i == 0)
                    {
                        arrayOfBaskets[j].gameObject.GetComponent<TagContainer>().fruitTag = "Apple";
                        //arrayOfBaskets[j].GetComponent<TagContainer>().fruitTag = "Apple";
                    }
                    else if(i == 1)
                    {
                        arrayOfBaskets[j].gameObject.GetComponent<TagContainer>().fruitTag = "Grape";
                        //arrayOfBaskets[j].GetComponent<TagContainer>().fruitTag = "Grape";
                    }
                    else if (i == 2)
                    {
                        arrayOfBaskets[j].gameObject.GetComponent<TagContainer>().fruitTag = "DFruit";
                        //arrayOfBaskets[j].GetComponent<TagContainer>().fruitTag = "DFruit";
                    }

                    //  add to increment to keep track of
                    //  amount of same mats
                    increment++;
                }
            }

            // if the colour mat appeared max amount of times
            if(increment == colourMax)
            {
                //  remove the mat from the list
                colourMats.RemoveAt(i);
            }

            //  reset increment for next check
            increment = 0;
        }
    }
}
