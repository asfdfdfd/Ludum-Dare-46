using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingController : MonoBehaviour
{
    public TMP_Text textName;

    public TMP_Text textCounter;

    public TMP_Text textDescription;
    
    void Start()
    {
        switch (GameState.Instance.ending)
        {
            case Ending.A:
                textName.text = "cowArd";
                textDescription.text = "Cowardly monster finished your adventure.";
                PlayerPrefs.SetInt("A", 1);
                break;
            case Ending.B:
                textName.text = "it is the Boss";
                textDescription.text = "Ghost is simply overpowered. Or not?";
                PlayerPrefs.SetInt("B", 1);
                break;
            case Ending.C:
                textName.text = "Cruel sword";
                textDescription.text = "It awaits new adventurer.";   
                PlayerPrefs.SetInt("C", 1);
                break;
            case Ending.D:
                textName.text = "friend in need is a friend indeeD";
                textDescription.text = "My dear friend. Where are you?";
                PlayerPrefs.SetInt("D", 1);
                break;
            case Ending.E:
                textName.text = "End of arthur";
                textDescription.text = "Goodbye, Arthur.";      
                PlayerPrefs.SetInt("E", 1);
                break;
            case Ending.F:
                textName.text = "Fruitless eFFort";
                textDescription.text = "Eat fruits!";
                PlayerPrefs.SetInt("F", 1);
                break;
            case Ending.G:
                textName.text = "Give up";
                textDescription.text = "Never gonna let down.";
                PlayerPrefs.SetInt("G", 1);
                break;            
            case Ending.N:
                textName.text = "";
                textDescription.text = "";
                break;
        }

        var amountOfEndings = PlayerPrefs.GetInt("A", 0) + PlayerPrefs.GetInt("B", 0) + PlayerPrefs.GetInt("C", 0) +
                              PlayerPrefs.GetInt("D", 0) + PlayerPrefs.GetInt("E", 0) + PlayerPrefs.GetInt("F", 0) +  + PlayerPrefs.GetInt("G", 0);
        

        textCounter.text = "(" + amountOfEndings.ToString() + "/7)";
    }
}
