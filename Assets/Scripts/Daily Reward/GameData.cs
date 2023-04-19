using UnityEngine;

public static class GameData {
	private static float money = 0;
	private static float coins = 0;
	private static float milk = 0;
	private static float eggs = 0;
	private static float ham = 0;
	private static float wool = 0;

	//Static Constructor to load data from playerPrefs
	static GameData ( ) {
		money = PlayerPrefs.GetFloat ( "Money", 0 );
		coins = PlayerPrefs.GetFloat ( "Coins", 0 );
		milk = PlayerPrefs.GetFloat ( "Milk", 0 );
		eggs = PlayerPrefs.GetFloat ( "Eggs", 0 );
		ham = PlayerPrefs.GetFloat ( "Ham", 0 );
		wool = PlayerPrefs.GetFloat ( "Wool", 0 );

		

	}

	public static float Money {
		get{ return money; }
		set{ PlayerPrefs.SetFloat ( "Money", (money = value) ); }
	}

	public static float Coins {
		get{ return coins; }
		set{ PlayerPrefs.SetFloat ( "Coins", (coins = value) ); }
	}

	public static float Milk {
		get{ return milk; }
		set{ PlayerPrefs.SetFloat ( "Milk", (milk = value) ); }
	}

	public static float Eggs {
		get{ return eggs; }
		set{ PlayerPrefs.SetFloat ( "Eggs", (eggs = value) ); }
	}

	public static float Ham {
		get{ return ham; }
		set{ PlayerPrefs.SetFloat ( "Ham", (ham = value) ); }
	}

	public static float Wool {
		get{ return wool; }
		set{ PlayerPrefs.SetFloat ( "Wool", (wool = value) ); }
	}

	public static string NumShortCut(float value)
    { 
        switch (value)
        {
            case float n when n >= 1000 && n < 1000000:
                return (value / 1000).ToString("F1") + "K";
            case float n when n >= 1000000 && n < 1000000000:
                return (value / 1000000).ToString("F1") + "M";
            case float n when n >= 1000000000 && n < 1000000000000:
                return (value / 1000000000).ToString("F1") + "B";
            case float n when n >= 1000000000000 && n < 1000000000000000:
                return (value / 1000000000000).ToString("F1") + "T";
            case float n when n >= 1000000000000000:
                return (value / 1000000000000000).ToString("F1") + "Q";
            default:
                return value.ToString("F1");
        }
    }

	public static void ClearGameData()
	{
		Money = 0;
		Coins = 0;
		Milk = 0;
		Eggs = 0;
		Ham = 0;
		Wool = 0;
	}



	/*---------------------------------------------------------
		this line:
		set{ PlayerPrefs.SetInt ( "Gems", (_gems = value) ); }

		is equivalent to:
		set{ 
			_gems = value;
			PlayerPrefs.SetInt ( "Gems", _gems ); 
		}
	------------------------------------------------------------*/
}


//connect the number of upgrades of a specific destination to the player prefs, and then use it to check the requirement of the reward.
// one way to do it is to put here veriables of each destination upgrade, it will be easier to pull it after and use everywhere,
// second option is to send the upgrade valur to player pref fron the destinations component. 
// another thing to be considered is the saved data about how many and what building is currently active and what is the current capacity ...
// mayne it will be better to store it in the endzone component. in that way each end zone gets the specific data of itself.
// we need to know how many end zones are active and what is the upgrade in each end zone and the capacityy of the building