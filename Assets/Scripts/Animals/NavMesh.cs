using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMesh : MonoBehaviour
{
    protected Vector3 destination;
    protected NavMeshAgent agent;
    protected Destinations destinations;
    public float value;
    public float resourceValue;
    protected Animator animator;

    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        

    }


    // Update is called once per frame
    // void Update()
    // {
    //     agent.SetDestination(destination);
    // }


    // protected int GetRandNumber(Destinations dest)
    //  {
    //     int rand = Random.Range(0, dest.numbers.Count);
    //     return dest.numbers[rand];
    //  }


    //  protected void UpdateDestination(Destinations dest)
    //  {
    //     destination = dest.transform.GetChild(GetRandNumber(dest)).transform.position;
    //  }
     

}
