using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementTemplate : MonoBehaviour
{
    Achievement achievement;
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text Amount;
    public string code;
    public Image achImage;

    private GlobalUI globalUI;
    private AchievementManager achievementManager;

    private void Awake() 
    {
        globalUI = GameObject.Find("UI").GetComponent<GlobalUI>();
        achievementManager = globalUI.achievementUI.GetComponent<AchievementManager>();
    }

    public void Initialize(Achievement achievement) 
    {
        this.achievement = achievement;
        title.text = achievement.title;
        description.text = achievement.description;
        Amount.text = achievement.amount.ToString();
        code = achievement.code;

        switch (achievement.title)
        {
            case "Step By Step" :
                achImage.sprite = achievementManager.stepByStepAch;
                break;
            case "Upgraded" :
                achImage.sprite = achievementManager.upgradeAch;
                break;
            case "Eggs master" :
                achImage.sprite = achievementManager.chickenAch;
                break;
            case "Milk master" :
                achImage.sprite = achievementManager.cowAch;
                break;
            case "Ham master" :
                achImage.sprite = achievementManager.pigAch;
                break;
            case "Wool master" :
                achImage.sprite = achievementManager.sheepAch;
                break;

            default:
            break;
        }
    }

    public void ClaimReward() 
    {
        PlayerPrefs.SetInt(code, 2);
        Debug.Log("Claimed" + PlayerPrefs.GetInt(code));
        achievement.UpdateAchievements();
        GameData.Coins += achievement.amount;
        globalUI.UpdateUpperPannelTxt();
        //destroy this gameobject
        Destroy(gameObject, 0.1f);
        
    }


}
