using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MistakesCollider : MonoBehaviour
{
    public int NumOfMistakes = 0;
    public TMP_Text mistakesText;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("half"))
        {
            //Fruit fruit = other.transform.parent.parent.GetComponent<Fruit>();
            Debug.Log("all ok");
            return;
        }

        if (other.CompareTag("fruit"))
        {
            Fruit fruit = other.transform.GetComponent<Fruit>();
            if (fruit.IsSliced == false)
            {
                fruit.NumOfPasses++;
                if (fruit.NumOfPasses > 1)
                {
                    NumOfMistakes++;
                    if (NumOfMistakes == 1)
                        mistakesText.text = "X";
                    else if (NumOfMistakes == 2)
                        mistakesText.text = "XX";
                    else if (NumOfMistakes == 3)
                        mistakesText.text = "XXX";
                }
            }
            return;
        }    
    }


}
