using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateObject : MonoBehaviour
{
    
    public Vector3 animalStartPosition;
    private Vector3 animalPositionRand;
    public float rand =0.2f;

    // Start is called before the first frame update

    public void InstantiateAnimal(GameObject animal)
    {
        animalPositionRand = animalStartPosition + new Vector3(Random.Range(-rand, rand), 0, Random.Range(-rand, rand));
        Instantiate(animal,  transform);
    }


}
