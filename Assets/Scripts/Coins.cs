using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coins : MonoBehaviour
{

    public float coinsNum;
    private TMP_Text coinsText;


    // Start is called before the first frame update
    void Start()
    {
        coinsText = gameObject.GetComponent<TMP_Text>();
        coinsText.text = coinsNum.ToString();; 
    }


    public void UpdateCoinsNum(float num){
        coinsNum += num;
        coinsText.text = NumShortCut();
    }

    public string NumShortCut()
    {
        // if the number is over 1K then shorten it to num K , if over 1M then shorten it to num M and so on with switch case   
        switch (coinsNum)
        {
            case float n when n >= 1000 && n < 1000000:
                return (coinsNum / 1000).ToString("F1") + "K";
            case float n when n >= 1000000 && n < 1000000000:
                return (coinsNum / 1000000).ToString("F1") + "M";
            case float n when n >= 1000000000 && n < 1000000000000:
                return (coinsNum / 1000000000).ToString("F1") + "B";
            case float n when n >= 1000000000000 && n < 1000000000000000:
                return (coinsNum / 1000000000000).ToString("F1") + "T";
            case float n when n >= 1000000000000000:
                return (coinsNum / 1000000000000000).ToString("F1") + "Q";
            default:            
                return coinsNum.ToString("F1");     
        }

    }

    public float GetReward()
    {
        return coinsNum*0.2f +1 ;
    }

}

