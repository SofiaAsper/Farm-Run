using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UpgradeSO", menuName = "UpgradeSO", order = 2)]
public class UpgradeSO : ScriptableObject {

    //create a list of buildingSOs
    public enum PaymentType {
        money,
        coins,
        milk,
        wool,
        ham,
        eggs
    }
    public enum UpgardedValue {
        money,
        coins,
        milk,
        whool,
        ham,
        eggs,
        capacity 
    }
    public string title;
    public string description;
    public float cost;
    public PaymentType paymentType;
    public UpgardedValue upgardedValue;
    public GameObject AnimalForUpgrade;
    public List<BuildingSO> buildingsList = new List<BuildingSO>();
    public Sprite image;
    public float increasePercentage;
    public int maxUpgrade;
    public int progress;
    public int orderNumber;


}



