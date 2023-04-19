using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlobalUI : MonoBehaviour
{
    public GameObject storeUI;
    public GameObject achievementUI;

    [Header("Upper Pannel")]
    [SerializeField] private TMP_Text moneyTxt;
    [SerializeField] private TMP_Text milkTxt;
    [SerializeField] private TMP_Text woolTxt;
    [SerializeField] private TMP_Text hamTxt;
    [SerializeField] private TMP_Text eggsTxt;
    [SerializeField] private TMP_Text coinsTxt;

    [Header("Add End Point Buttons")]
    [SerializeField] private AddEndPointBtn chickenBtn;
    [SerializeField] private AddEndPointBtn cowBtn;
    [SerializeField] private AddEndPointBtn sheepBtn;
    [SerializeField] private AddEndPointBtn pigBtn;


    private void Start() {
        UpdateUpperPannelTxt();
        Invoke("UpdateEndPointBtns", 30f);
    }

    private void Update()
    {
        UpdateEndPointBtns();
    }

    public void UpdateUpperPannelTxt()
    {
        moneyTxt.text = GameData.NumShortCut(GameData.Money);
        milkTxt.text = GameData.NumShortCut(GameData.Milk);
        woolTxt.text = GameData.NumShortCut(GameData.Wool);
        hamTxt.text = GameData.NumShortCut(GameData.Ham);
        eggsTxt.text = GameData.NumShortCut(GameData.Eggs);
        coinsTxt.text = GameData.NumShortCut(GameData.Coins);
        
    }

    public void UpdateEndPointBtns()
    {
        chickenBtn.gameObject.SetActive(true);
        // if all the chicken end zones are open then enable the pig button
        if (PlayerPrefs.HasKey("EZCh1") && PlayerPrefs.HasKey("EZCh2") && PlayerPrefs.HasKey("EZCh3"))
            pigBtn.gameObject.SetActive(true);
        // if all the pig end zones are open then enable the cow button 
        if (PlayerPrefs.HasKey("EZPi1"))
            sheepBtn.gameObject.SetActive(true); 
        // if all the sheep end zones are open then enable the cow button
        if (PlayerPrefs.HasKey("EZSh1") && PlayerPrefs.HasKey("EZSh2"))
            cowBtn.gameObject.SetActive(true);
            
    }

    
}
