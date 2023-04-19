using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSC : MonoBehaviour
{
    public EndZone endZone;
    public bool IsFull = false;
    public Destinations destinations;

    [Tooltip("The code in the player prefs for this slider")]
    [SerializeField] string code;

    Destination destination;
    Slider slider;
    public int sliderValue;

    public int SliderValue {
		get{ return sliderValue; }
		set{ PlayerPrefs.SetInt ( code, (sliderValue = value) ); }
	}

    private void Awake() {
        
        PlayerPrefs.SetInt ( code, 0 ); //******************************** delete ********************************
        slider = GetComponent<Slider>();
        SliderValue = PlayerPrefs.HasKey(code)? PlayerPrefs.GetInt(code) : 0;
        InitializeSlider();
    }

    private void Update() {
        if (endZone.transform.GetChild(1).childCount > 0)
        {
            ChangeValue();
        }
    }
    //EZCh1Slider



    public void InitializeSlider()
    {
        Transform buildingsFather = endZone.transform.GetChild(1); 
        if (buildingsFather.childCount > 0)
        {
            destination = buildingsFather.GetChild(buildingsFather.childCount - 1).GetComponent<Destination>();
            if (slider){
                slider.maxValue = destination.buildingSO.capacity;
                slider.value = SliderValue;
                slider.fillRect.GetComponent<Image>().color = Color.yellow;

            }

        }
    }

    public void ResetSliderValue()
    {
        SliderValue = 0;
        slider.value = 0;
    }

    public void ChangeValue(){
        if (slider.value < slider.maxValue)
        {
            slider.value = SliderValue;
            IsFull = false;
        } else {
            // chnage the color of the fill to red
            slider.fillRect.GetComponent<Image>().color = Color.red;
            IsFull = true;
            destination.IsFull = true;
            destinations.CheckIfFull();
        }
    }
    
}
