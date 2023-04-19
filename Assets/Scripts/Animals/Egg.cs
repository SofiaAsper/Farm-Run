using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Egg : MonoBehaviour
{

    public float eggNum;
    private TMP_Text eggText;

    // Start is called before the first frame update
    void Start()
    {
        eggText = gameObject.GetComponent<TMP_Text>();
        eggText.text = "0";
    }
    public void UpdateEggNum(float num)
    {
        eggNum += num;
        eggText.text = NumShortCut();
    }
    public string NumShortCut()
    {

        // if the number is over 1K then shorten it to num K , if over 1M then shorten it to num M and so on with switch case   
        switch (eggNum)
        {
            case float n when n >= 1000 && n < 1000000:
                return (eggNum / 1000).ToString("F1") + "K";
            case float n when n >= 1000000 && n < 1000000000:
                return (eggNum / 1000000).ToString("F1") + "M";
            case float n when n >= 1000000000 && n < 1000000000000:
                return (eggNum / 1000000000).ToString("F1") + "B";
            case float n when n >= 1000000000000 && n < 1000000000000000:
                return (eggNum / 1000000000000).ToString("F1") + "T";
            case float n when n >= 1000000000000000:
                return (eggNum / 1000000000000000).ToString("F1") + "Q";
            default:
                return eggNum.ToString("F1");
        }
    }

}
