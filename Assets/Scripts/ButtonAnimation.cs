using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    string buttonName;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        buttonName = gameObject.name;
    }
    //update the button image when pressed and change back when released
    public void Pressed()
    {
        switch (buttonName)
        {
            case "Pig Btn":
                anim.SetTrigger("PigPressed");
                break;
            case "Chicken Btn":
                anim.SetTrigger("ChickenPressed");
                break;
            case "Sheep Btn":
                anim.SetTrigger("SheepPressed");
                break;
            case "Cow Btn":
                anim.SetTrigger("CowPressed");
                break;

        }
    }
    
}
