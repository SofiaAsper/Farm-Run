using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{

    public Destinations destinations;
    public SliderSC slider;
    // public GameObject[] path;
    public int amoutOfUpgrades;
    [SerializeField] GameObject buildingsFather;
    [SerializeField] string endZoneCode;

    public int AmoutOfUpgrades {
		get{ return amoutOfUpgrades; }
		set{ PlayerPrefs.SetInt ( endZoneCode, (amoutOfUpgrades = value) ); }
	}

    private void Start() {
        //deletet the key from the player prefs
        if (PlayerPrefs.HasKey(endZoneCode)) PlayerPrefs.DeleteKey(endZoneCode);  //******************************** delete ********************************
        GameData.ClearGameData();  //******************************** delete ********************************
        Initialize();
    }

    public void Initialize()
    {
        if (amoutOfUpgrades > 0 || PlayerPrefs.HasKey(endZoneCode)){
            AmoutOfUpgrades = PlayerPrefs.HasKey(endZoneCode)? PlayerPrefs.GetInt(endZoneCode) : amoutOfUpgrades;
            for (int i = 0; i < AmoutOfUpgrades; i++)
            {
                AddNewBuilding();
            }
        }

    }    
    
    
    // Instantiate the next building on the buildings list
    public void SpawnNewBuilding()
    {
        int numberOfCurrentBuilding = buildingsFather.transform.childCount;
        if (numberOfCurrentBuilding < destinations.buildingsList.Count)
        {
            BuildingSO building = destinations.buildingsList[numberOfCurrentBuilding];
            if (numberOfCurrentBuilding - 1 >= 0) buildingsFather.transform.GetChild(numberOfCurrentBuilding - 1).gameObject.SetActive(false);
            //increase the cost by 150% of the building's cost
            building.cost = (int)(building.cost * 7.5f);

            GameObject newBuilding = Instantiate(building.gameObject, buildingsFather.transform);
            slider.InitializeSlider(); 
        }
    }

    public void AddNewBuilding()
    {
        if (buildingsFather.transform.childCount == 0)
        {
            slider.gameObject.SetActive(true);
        }
        SpawnNewBuilding();

        // invoke the StartTimer methid from the TimerManager
    }

    // using TimerManager to instantiate the TimerCanvas in the building position

    


}
