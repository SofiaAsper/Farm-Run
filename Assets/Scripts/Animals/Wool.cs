using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Wool : MonoBehaviour
{
    public float woolNum;
    private TMP_Text woolText;


    // Start is called before the first frame update
    void Awake()
    {
        woolText = gameObject.GetComponent<TMP_Text>();
        woolText.text = "0"; 
    }


    public void UpdateWoolNum(float num){
        woolNum += num;
        woolText.text = NumShortCut();
    }

    public string NumShortCut()
    {
        // if the number is over 1K then shorten it to num K , if over 1M then shorten it to num M and so on with switch case   
        switch (woolNum)
        {
            case float n when n >= 1000 && n < 1000000:
                return (woolNum / 1000).ToString("F1") + "K";
            case float n when n >= 1000000 && n < 1000000000:
                return (woolNum / 1000000).ToString("F1") + "M";
            case float n when n >= 1000000000 && n < 1000000000000:
                return (woolNum / 1000000000).ToString("F1") + "B";
            case float n when n >= 1000000000000 && n < 1000000000000000:
                return (woolNum / 1000000000000).ToString("F1") + "T";
            case float n when n >= 1000000000000000:
                return (woolNum / 1000000000000000).ToString("F1") + "Q";
            default:            
                return woolNum.ToString("F1");
        }

    }

}
