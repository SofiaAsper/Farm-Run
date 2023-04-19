using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using TMPro;

namespace DailyRewardSystem {
	public enum RewardType {
		Money,
		Coins
	}

	[Serializable] public struct Reward {
		public RewardType Type;
		public int Amount;
	}

	public class DailyRewards : MonoBehaviour {

		[Header ( "Main Menu UI" )]
		[SerializeField] TMP_Text moneyText;
		[SerializeField] TMP_Text coinsText;
		// [SerializeField] Text gemsText;

		[Space]
		[Header ( "Reward UI" )]
		[SerializeField] GameObject rewardsCanvas;
		[SerializeField] Button openButton;
		[SerializeField] Button closeButton;
		[SerializeField] Image rewardImage;
		[SerializeField] TMP_Text rewardAmountText;
		[SerializeField] Button claimButton;
		[SerializeField] GameObject rewardsNotification;
		[SerializeField] GameObject noMoreRewardsPanel;


		[Space]
		[Header ( "Rewards Images" )]
		[SerializeField] Sprite iconMoneySprite;
		[SerializeField] Sprite iconCoinsSprite;
		// [SerializeField] Sprite iconGemsSprite;

		[Space]
		[Header ( "Rewards Database" )]
		[SerializeField] RewardsDatabase rewardsDB;

		[Space]
		[Header ( "FX" )]
		[SerializeField] ParticleSystem fxMoney;
		[SerializeField] ParticleSystem fxCoins;
		// [SerializeField] ParticleSystem fxGems;

		[Space]
		[Header ( "Timing" )]
		//wait 23 Hours to activate the next reward (it's better to use 23h instead of 24h)
		[SerializeField] double nextRewardDelay = 23f;
		//check if reward is available every 5 seconds
		[SerializeField] float checkForRewardDelay = 5f;


		private int nextRewardIndex;
		private bool isRewardReady = false;

		void Start ( ) {
			Initialize ( );

			StopAllCoroutines ( );
			StartCoroutine ( CheckForRewards ( ) );
		}

		void Initialize ( ) {
			InitialDateTime ( );
			nextRewardIndex = PlayerPrefs.GetInt ( "Next_Reward_Index", 0 );

			//Update Mainmenu UI (moneys,coins,eggs,ham,wool, milk)
			UpdateMoneyTextUI ( );
			UpdateCoinsTextUI ( );
			// UpdateGemsTextUI ( );

			//Add Click Events
			openButton.onClick.RemoveAllListeners ( );
			openButton.onClick.AddListener ( OnOpenButtonClick );

			closeButton.onClick.RemoveAllListeners ( );
			closeButton.onClick.AddListener ( OnCloseButtonClick );

			claimButton.onClick.RemoveAllListeners ( );
			claimButton.onClick.AddListener ( OnClaimButtonClick );

			//Check if the game is opened for the first time then set Reward_Claim_Datetime to the current datetime
			if ( string.IsNullOrEmpty ( PlayerPrefs.GetString ( "Reward_Claim_Datetime" ) ) )
			{
				ActivateReward();
				PlayerPrefs.SetString ( "Reward_Claim_Datetime", DateTime.Now.ToString ( ) );
			}
		}

		IEnumerator CheckForRewards ( ) {
			while ( true ) {
				if ( !isRewardReady ) {
					DateTime currentDatetime = DateTime.Now;
					DateTime rewardClaimDatetime = DateTime.Parse ( PlayerPrefs.GetString ( "Reward_Claim_Datetime", currentDatetime.ToString ( ) ) );

					//get total Hours between this 2 dates
					double elapsedHours = (currentDatetime - rewardClaimDatetime).TotalHours;

					if ( elapsedHours >= nextRewardDelay)
						ActivateReward ( );
					else
						DesactivateReward ( );
				}

				yield return new WaitForSeconds ( checkForRewardDelay );
			}
		}

		void ActivateReward ( ) {
			isRewardReady = true;

			noMoreRewardsPanel.SetActive ( false );
			claimButton.gameObject.SetActive(true);
			rewardsNotification.SetActive ( true );
			// ger the animation of the button
			openButton.GetComponent<Animator>().SetTrigger ( "RewardReady" );

			//Update Reward UI
			Reward reward = rewardsDB.GetReward ( nextRewardIndex );

			//Icon
			if ( reward.Type == RewardType.Money )
				rewardImage.sprite = iconMoneySprite;
			else if ( reward.Type == RewardType.Coins )
				rewardImage.sprite = iconCoinsSprite;
			// else
			// 	rewardImage.sprite = iconGemsSprite;

			//Amount
			rewardAmountText.text = string.Format ( "+{0}", reward.Amount );

		}

		void DesactivateReward ( ) {
			isRewardReady = false;

			noMoreRewardsPanel.SetActive ( true );
			claimButton.gameObject.SetActive(false);
			rewardsNotification.SetActive ( false );
			openButton.GetComponent<Animator>().SetTrigger ( "NoReward" );
		}

		void OnClaimButtonClick ( ) {
			Reward reward = rewardsDB.GetReward ( nextRewardIndex );

			//check reward type
			if ( reward.Type == RewardType.Money ) {
				Debug.Log ( "<color=white>" + reward.Type.ToString ( ) + " Claimed : </color>+" + reward.Amount );
				GameData.Money += reward.Amount;
				fxMoney.Play ( );
				UpdateMoneyTextUI ( );

			} else if ( reward.Type == RewardType.Coins ) {
				Debug.Log ( "<color=yellow>" + reward.Type.ToString ( ) + " Claimed : </color>+" + reward.Amount );
				GameData.Coins += reward.Amount;
				fxCoins.Play ( );
				UpdateCoinsTextUI ( );

			} else {//reward.Type == RewardType.Gems
				// Debug.Log ( "<color=green>" + reward.Type.ToString ( ) + " Claimed : </color>+" + reward.Amount );
				// GameData.Gems += reward.Amount;
				// fxGems.Play ( );
				// UpdateGemsTextUI ( );

			}

			isRewardReady = false;
			//Save next reward index
			nextRewardIndex++;
			if ( nextRewardIndex >= rewardsDB.rewardsCount )
				nextRewardIndex = 0;

			PlayerPrefs.SetInt ( "Next_Reward_Index", nextRewardIndex );

			//Save DateTime of the last Claim Click
			PlayerPrefs.SetString ( "Reward_Claim_Datetime", DateTime.Now.ToString ( ) );

			DesactivateReward ( );
		}

		//Update Mainmenu UI (metals,coins,gems)--------------------------------
		void UpdateMoneyTextUI ( ) {
			moneyText.text = GameData.NumShortCut(GameData.Money);
		}

		void UpdateCoinsTextUI ( ) {
			coinsText.text = GameData.NumShortCut(GameData.Coins);
		}

		// void UpdateGemsTextUI ( ) {
		// 	gemsText.text = GameData.Gems.ToString ( );
		// }

		//Open | Close UI -------------------------------------------------------
		void OnOpenButtonClick ( ) {
			rewardsCanvas.SetActive ( true );
		}

		void OnCloseButtonClick ( ) {
			rewardsCanvas.SetActive ( false );
		}


	//***************************************************delete after finish****************************************************

	private void InitialDateTime(){
		PlayerPrefs.SetString ( "Reward_Claim_Datetime",null );
	}

	}
}

