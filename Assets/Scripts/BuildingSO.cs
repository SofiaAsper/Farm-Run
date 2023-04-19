using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BuildingSO", menuName = "BuildingSO", order = 1)]
public class BuildingSO : ScriptableObject {
    
    public enum PaymentType {
        coins,
        milk,
        wool,
        ham,
        egg,
        money
    }

    public enum AnimalType{
        cow,
        sheep,
        pig,
        chicken
    }

    public GameObject gameObject;
    public string buildingName;
    public string description;
    public float cost;
    public int capacity;
    public PaymentType paymentType;
    public AnimalType animalType;
    public Sprite image;

}

