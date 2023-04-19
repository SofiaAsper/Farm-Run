using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AddEndPointBtn : MonoBehaviour 
{
    [SerializeField] GameObject destinations;
    [SerializeField] TMP_Text labelTxt;
    [SerializeField] ParticleSystem particle;


    bool isAffordable = false;

    void Awake()
    {
        gameObject.SetActive(false);        
        ShopManager.OnPurchase += UpdateLocation;
    }

    void OnEnable()
    {
        labelTxt.text = destinations.GetComponent<Destinations>().buildingsList[0].cost.ToString();
        UpdateLocation();
    }

    void Update()
    {
        CheckAffordable();
    }

    void OnDisable()
    {
        ShopManager.OnPurchase -= UpdateLocation;
    }


    public void UpdateLocation()
    {
        // loop through all the children of the destinations gameobject
        for (int i = 0; i < destinations.transform.childCount; i++)
        {
            // get the child of the destinations gameobject
            Transform endZone = destinations.transform.GetChild(i);
            // get the child of the child of the destinations gameobject
            Transform buildingsFather = endZone.GetChild(1);

            // check if there are any buildings in the buildings father
            if (buildingsFather.childCount == 0)
            {
                // set the position of this game object to be relatively to the end zone and add 6 to the y position
                transform.position = endZone.position + new Vector3(0, 1f, 0);
                labelTxt.text = destinations.GetComponent<Destinations>().buildingsList[0].cost.ToString();

                return;
            }
        }
        // disable the button if there are no more buildings to add
            gameObject.SetActive(false);        
    }

    private void CheckAffordable()
    {
        // if the player can afford the building than begin the particle system
        if (int.Parse(labelTxt.text) <= GameData.Money && !isAffordable)
        {
            isAffordable = true;
            particle.Play();
        }
        else if (int.Parse(labelTxt.text) > GameData.Money && isAffordable)
        {
            isAffordable = false;
            particle.Stop();
        }
    }

}
