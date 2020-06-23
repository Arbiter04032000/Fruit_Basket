using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourManager : MonoBehaviour
{
    //  reference to baskets
    public GameObject[] arrayOfBaskets;

    //  reference to materials + used fruit mats
    public List<Material> colourMats, colourMatsStored, fruitUsedMats;

    //  default mat + mats for filled baskets
    public Material defaultMat, basketMatGood, basketMatBad;

    //  generates random number from 1-3
    private int randomNumber;

    // max limit of one colour being drawn
    private int colourMax;

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

    /// <summary>
    /// Implemented to Assign Colour to baskets at the beginning of each round
    /// </summary>
    public void fillBaskets()
    {
        //  resets baskets to default mat
        resetBaskets();

        //  create list of colour mats that are to be used
        List<Material> colourMatsBeingUsed = new List<Material>();

        //  transfer all colour mats materials into the newly created list
        foreach (Material mat in colourMats)
        {
            colourMatsBeingUsed.Add(mat);
        }

        //  fill arrayOfBaskets
        arrayOfBaskets = GameObject.FindGameObjectsWithTag("Basket");

        //  max limit of colours
        colourMax = arrayOfBaskets.Length / colourMatsBeingUsed.Count;

        for(int i = 0; i < arrayOfBaskets.Length; i++)
        {
            // generate a random number from the colour mats which will count as the index
            int randomNumber = Random.Range(0, colourMatsBeingUsed.Count);

            // Assign the found material from the random number used as index to basket material
            arrayOfBaskets[i].transform.GetChild(0).GetComponent<Renderer>().material = colourMatsBeingUsed[randomNumber];

            // check if the function to assign materials should assign depending on conditions
            check(colourMatsBeingUsed);

            // add relevant tag to the fruit depending on the colour mats listed
            AddRelevantTag();
        }
    }

    /// <summary>
    /// Caps the amount of colour_mats assigned to baskets
    /// By Iterating through colour mat array and basket array.
    /// Iterating to check if a material is the same on both arrays
    /// Deleting the material in colour mat array if it appears more than specified
    /// </summary>
    void check(List<Material> colourList)
    {
        int increment = 0;

        //  iterate through each colour mat
        for(int i = 0; i < colourList.Count; i++)
        {
            //  iterate through each basket
            for(int j = 0; j < arrayOfBaskets.Length; j++)
            {
                // material of basket child
                Material childMat = arrayOfBaskets[j].transform.GetChild(0).GetComponent<Renderer>().sharedMaterial;

                //  if material matches material of basket
                if(colourList[i] == childMat)
                {
                    //  add to increment to keep track of
                    //  amount of same mats
                    increment++;
                }
            }

            // if the colour mat appeared max amount of times
            if(increment == colourMax)
            {
                //  remove the mat from the list
                colourList.RemoveAt(i);
            }

            //  reset increment for next check
            increment = 0;
        }
    }

    /// <summary>
    /// Implemented to look through each position on colour mat stored and assign it the correct tag
    /// Index 0: Apple
    /// Index 1: Grape
    /// Index 2: DFruit
    /// </summary>
    void AddRelevantTag()
    {
        for(int i = 0; i < colourMatsStored.Count; i++)
        {
            for(int j = 0; j < arrayOfBaskets.Length; j++)
            {
                // material of basket child
                Material childMat = arrayOfBaskets[j].transform.GetChild(0).GetComponent<Renderer>().sharedMaterial;

                //  if colour in colour mats stored index matches mat of basket material
                if (colourMatsStored[i] == childMat)
                {
                    if (i == 0)
                    {
                        arrayOfBaskets[j].gameObject.GetComponent<TagContainer>().fruitTag = "Apple";
                    }
                    if (i == 1)
                    {
                        arrayOfBaskets[j].gameObject.GetComponent<TagContainer>().fruitTag = "Grape";
                    }
                    if (i == 2)
                    {
                        arrayOfBaskets[j].gameObject.GetComponent<TagContainer>().fruitTag = "DFruit";
                    }
                    if (i == 3)
                    {
                        arrayOfBaskets[i].gameObject.GetComponent<TagContainer>().fruitTag = "Pineapple";
                    }
                }
            }
        }
    }

    //  adds default mat to each basket
    void resetBaskets()
    {
        //  fill arrayOfBaskets
        arrayOfBaskets = GameObject.FindGameObjectsWithTag("Basket");

        for(int i = 0; i < arrayOfBaskets.Length; i++)
        {
            arrayOfBaskets[i].transform.GetChild(0).GetComponent<Renderer>().material = defaultMat;
        }
    }
   
}
