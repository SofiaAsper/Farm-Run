using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageBuying : MonoBehaviour
{

    [SerializeField] GameObject cow;
    [SerializeField] GameObject pig;
    [SerializeField] GameObject Sheep ;
    [SerializeField] Destinations destinations;

    private ShopManager shopManager;
    private NavMesh navMeshCow, navMeshPig, navMeshSheep;

    private void Start()
    {
        shopManager = transform.GetComponent<ShopManager>();
        navMeshCow = cow.GetComponent<NavMesh>();
        navMeshPig = pig.GetComponent<NavMesh>();
        navMeshSheep = Sheep.GetComponent<NavMesh>(); 
    }



    public void BuyUpgrade(UpgradeSO upgradeSO)
    {
        switch (upgradeSO.upgardedValue.ToString())
        {
            case "money":
                // update the money each animal gives
                upgradeSO.AnimalForUpgrade.GetComponent<NavMesh>().value *= (1 + upgradeSO.increasePercentage);
                break;
            case "capacity":
            // loop threw the existing buildings and update the capacity of each building
                foreach (BuildingSO buildingSO in upgradeSO.buildingsList)
                {
                    buildingSO.capacity = (int)Mathf.Floor(buildingSO.capacity * (1 + upgradeSO.increasePercentage));
                }
                break;
            case "coins":
                // update the coins 
                Debug.Log("You payed for coins");
                break;
            case "cows":
                // update the milk
                navMeshCow.resourceValue *= (1 + upgradeSO.increasePercentage);
                break;
            case "sheep":
                navMeshSheep.resourceValue *= (1 + upgradeSO.increasePercentage);
                break;
            case "pig":
                navMeshPig.resourceValue *= (1 + upgradeSO.increasePercentage);

                break;

            default:
                break;

        }

    }


}
