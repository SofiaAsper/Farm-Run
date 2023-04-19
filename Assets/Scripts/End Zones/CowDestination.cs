using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowDestination : Destinations
{
    // Start is called before the first frame update
    private void Awake() {
        buildingsList[0].cost = 50f; 
        buildingsList[1].cost = 400f;       
    
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfFull();   
    }
}
