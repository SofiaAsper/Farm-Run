using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardSystem : MonoBehaviour
{
    [SerializeField] GameObject rewardUI;
    [SerializeField] GlobalUI globalUI; 
    [SerializeField] TMP_Text rewardText;
    // add particle system for reward
    [SerializeField] ParticleSystem moneyFX;

    // add all payment systems here

    private float reward;

    public void GrantReward()
    {
        // make the reward a random number between GameData.Money * 0.1 and GameData.Money * 0.3
        reward = Mathf.Round(Random.Range(GameData.Money * 0.1f, GameData.Money * 0.3f));
        // reward = Mathf.Round(GameData.Money * 20f) / 100f;
        Debug.Log("Reward granted!");
        rewardUI.SetActive(true);
        rewardText.text = "+" + reward.ToString();
    }

    public void OnCloseRewardUI()
    {
        GameData.Money += reward;
        globalUI.UpdateUpperPannelTxt();
        moneyFX.Play();
        rewardUI.SetActive(false);
        //start particle effect


    }
}
