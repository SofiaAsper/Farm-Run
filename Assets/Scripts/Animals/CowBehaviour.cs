using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowBehaviour : NavMesh
{
    

    public override void Start()
    {
        Destinations.OnUpdateNumbers += UpdateDestination; 
        base.Start();
        destinations = GameObject.Find("Cow Destinations").GetComponent<CowDestination>();
        animator.SetTrigger("Run");
        UpdateDestination();
    }

    void OnDisable() {
        Destinations.OnUpdateNumbers -= UpdateDestination;    
    }

    void Update()
    {
        agent.SetDestination(destination);
    }


     int GetRandNumber()
     {
        int rand = Random.Range(0, destinations.numbers.Count);
        return destinations.numbers[rand];
     }


      void UpdateDestination( )
     {
        destination = destinations.transform.GetChild(GetRandNumber()).transform.position;
     }




}
