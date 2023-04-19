using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Money : MonoBehaviour
{
    public float money;
    private TMP_Text moneyText;


    // Start is called before the first frame update
    void Start()
    {
        moneyText = gameObject.GetComponent<TMP_Text>();
        moneyText.text = "0"; 
    }


    public void UpdateMoneyNum(float num){
        money += num;
        moneyText.text = NumShortCut();
    }

    public string NumShortCut()
    {
        // if the number is over 1K then shorten it to num K , if over 1M then shorten it to num M and so on with switch case   
        switch (money)
        {
            case float n when n >= 1000 && n < 1000000:
                return (money / 1000).ToString("F1") + "K";
            case float n when n >= 1000000 && n < 1000000000:
                return (money / 1000000).ToString("F1") + "M";
            case float n when n >= 1000000000 && n < 1000000000000:
                return (money / 1000000000).ToString("F1") + "B";
            case float n when n >= 1000000000000 && n < 1000000000000000:
                return (money / 1000000000000).ToString("F1") + "T";
            case float n when n >= 1000000000000000:
                return (money / 1000000000000000).ToString("F1") + "Q";
            default:            
                return money.ToString("F1");     
        }

    }

    public float GetReward()
    {
        return 1.6f*money;
    }


}
