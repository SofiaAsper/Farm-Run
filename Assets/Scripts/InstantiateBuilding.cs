using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateBuilding : MonoBehaviour
{
  
    Destinations destinations; 


    void Awake()
    {
        destinations = gameObject.GetComponent<Destinations>();
    }
    


    // public void SpawnNewBuilding(int desNum)
    // {
    //     GameObject building = destinations.destinationsList[desNum].gameObject;
    //     Vector3 buildingPosition =destinations.destinationsList[desNum].transform.position;
    //     SliderSC slider = destinations.destinationsList[desNum].slider.GetComponent<SliderSC>();
    //     GameObject newBuilding = Instantiate(building, buildingPosition, Quaternion.identity , transform);
    //     newBuilding.AddComponent<Destination>();
    //     slider.InitializeSlider();
    // }
    
}
