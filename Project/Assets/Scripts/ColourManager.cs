using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourManager : MonoBehaviour
{
    //  reference to baskets
    private GameObject[] arrayOfBaskets;
    
    [SerializeField]
    //  reference to materials
    private List<Material> colourMats;

    //  generates random number from 1-3
    private int randomNumber;

    private int colourMax;

    /// <summary>
    /// Implemented to Assign Colour to baskets at the beginning of each round
    /// </summary>

    private void fillBaskets()
    {
        //  fill arrayOfBaskets
        arrayOfBaskets = GameObject.FindGameObjectsWithTag("Basket");

        //  max limit of colours
        colourMax = arrayOfBaskets.Length / colourMats.Count;

        //for(int i = 0; i < arrayOfBaskets.Length; i++)
        //{

        //    if (blueMax >= 4)
        //        while(randomNumber == 0)
        //        {
        //            randomNumber = Random.Range(0, colourMats.Count);
        //            //randomNumber = Random.Range(1, 4);
        //        }
        //    else if (redMax >= 4)
        //        while (randomNumber == 1)
        //        {
        //            randomNumber = Random.Range(0, colourMats.Count);
        //        }
        //    else if (yellowMax >= 4)
        //        while (randomNumber == 2)
        //        {
        //            randomNumber = Random.Range(0, colourMats.Count);
        //        }
        //    randomNumber = Random.Range(0, colourMats.Count);

        //    switch (randomNumber)
        //    {
        //        case 0:
        //            arrayOfBaskets[i].transform.GetChild(0).GetComponent<Renderer>().material = colourMats[randomNumber];
        //            blueMax++;
        //            break;
        //        case 1:
        //            arrayOfBaskets[i].transform.GetChild(0).GetComponent<Renderer>().material = colourMats[randomNumber];
        //            redMax++;
        //            break;
        //        case 2:
        //            arrayOfBaskets[i].transform.GetChild(0).GetComponent<Renderer>().material = colourMats[randomNumber];
        //            yellowMax++;
        //            break;
        //    }

        //}

        for(int i = 0; i < arrayOfBaskets.Length; i++)
        {
            int randomNumber = Random.Range(0, colourMats.Count);
            arrayOfBaskets[i].transform.GetChild(2).GetComponent<Renderer>().material = colourMats[randomNumber];
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

                //Debug.Log("Material Name: " + arrayOfBaskets[j].transform.GetChild(0).GetComponent<Renderer>().material);
                Material childMat = arrayOfBaskets[j].transform.GetChild(0).GetComponent<Renderer>().sharedMaterial;

                // if basket array index                    ==          colour mat index material
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

    // Update is called once per frame
    void Start()
    {
       fillBaskets();
       Debug.Log("Pressed P");
    }
}
