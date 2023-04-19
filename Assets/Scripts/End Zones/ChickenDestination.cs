using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenDestination : Destinations
{
    // Start is called before the first frame update
    private void Awake() {
        buildingsList[0].cost = 1.33f; 
        buildingsList[1].cost = 3f;       
        buildingsList[2].cost = 600f;  

        // update the player prefs with 1 upgrade
        PlayerPrefs.SetInt(gameObject.name, 1);    
    
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfFull();   
    }
}
