using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Destination : MonoBehaviour
{
    // public int numberOfAnimals;
    public bool IsFull = false;
    public BuildingSO buildingSO;
    private GlobalUI GlobalUI;
    private EndZone endZone;
    private ShopManager shopManager;



    private void Awake() {
        GlobalUI = GameObject.Find("UI").GetComponent<GlobalUI>();
        
    }

    private void Start() {
        endZone = transform.parent.parent.GetComponent<EndZone>();
        shopManager = GlobalUI.storeUI.GetComponent<ShopManager>();
    }
    
    public void AddAnimal()
    {
        endZone.slider.SliderValue++;
        // numberOfAnimals++;
    }


    private void OnTriggerEnter(Collider other) 
    {
        GameObject GO = other.gameObject;
        if (GO.tag == "Cow" || GO.tag == "Sheep" || GO.tag == "Pig" || GO.tag == "Chicken")
        {
            AddAnimal();
            Destroy(GO);
            
            if (!IsFull)
            {
                GameData.Money += GO.GetComponent<NavMesh>().value;
                
                if (GO.tag == "Cow") GameData.Milk += GO.GetComponent<NavMesh>().resourceValue;
                if (GO.tag == "Sheep") GameData.Wool += GO.GetComponent<NavMesh>().resourceValue;
                if (GO.tag == "Pig") GameData.Ham += GO.GetComponent<NavMesh>().resourceValue;
                if (GO.tag == "Chicken") GameData.Eggs += GO.GetComponent<NavMesh>().resourceValue;

                GlobalUI.UpdateUpperPannelTxt();
            }
        }    
    }

    private void OnMouseDown() {
        //get the Destinations script fron the end zone parent
        shopManager.GetTheEndZone(transform.parent.parent.GetComponent<EndZone>());
        shopManager.GetDestinations(transform.parent.parent.parent.GetComponent<Destinations>());
        shopManager.OpenStoreBuildings();
    }

    private void UpdateBuilding() {
        endZone.SpawnNewBuilding();
    }


    



}
