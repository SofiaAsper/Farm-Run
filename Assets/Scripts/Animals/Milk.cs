using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Milk : MonoBehaviour
{
    public float milkNum;
    private TMP_Text milkText;


    // Start is called before the first frame update
    void Awake()
    {
        milkText = gameObject.GetComponent<TMP_Text>();
        milkText.text = "0"; 
    }


    public void UpdateMilkNum(float num){
        milkNum += num;
        milkText.text = NumShortCut();
    }

    public string NumShortCut()
    {

        // if the number is over 1K then shorten it to num K , if over 1M then shorten it to num M and so on with switch case   
        switch (milkNum)
        {
            case float n when n >= 1000 && n < 1000000:
                return (milkNum / 1000).ToString("F1") + "K";
            case float n when n >= 1000000 && n < 1000000000:
                return (milkNum / 1000000).ToString("F1") + "M";
            case float n when n >= 1000000000 && n < 1000000000000:
                return (milkNum / 1000000000).ToString("F1") + "B";
            case float n when n >= 1000000000000 && n < 1000000000000000:
                return (milkNum / 1000000000000).ToString("F1") + "T";
            case float n when n >= 1000000000000000:
                return (milkNum / 1000000000000000).ToString("F1") + "Q";
            default:            
                return milkNum.ToString("F1");
        }


    }
}
