using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Destinations : MonoBehaviour ,IPointerClickHandler
{
    public delegate void UpdateAction();
    public static event UpdateAction OnUpdateNumbers;

    public List<BuildingSO> buildingsList = new List<BuildingSO>();
    public List<UpgradeSO> upgradesList = new List<UpgradeSO>();
    public List<int> numbers = new List<int>();
    public TMP_Text labelTxt;

    [Header("UI")]
    [Tooltip("The main button that instantiates the animal prefab")]
    [SerializeField] Button mainButton;
    [SerializeField] Button AddNewEndPointBtn;
    
    int numberOfActiveEndPoints;
    Destination destination;
    EndZone endZone;
    AddEndPointBtn addEndPointBtn;

    public int NumberOfActiveEndPoints 
    {
		get{ return numberOfActiveEndPoints; }
		set{ PlayerPrefs.SetInt ( gameObject.name, (numberOfActiveEndPoints = value) ); }
	}

    private void Start() {
        if(PlayerPrefs.HasKey(gameObject.name)) PlayerPrefs.DeleteKey(gameObject.name); //******************************** delete ********************************

    }


    public void CheckIfFull() 
    {
        // if there are no buildings in any of the children then set active false the button
        if (transform.GetChild(0).GetChild(1).childCount == 0) mainButton.gameObject.SetActive(false);
        else mainButton.gameObject.SetActive(true);

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform endZone = transform.GetChild(i);
            Transform buildingsFather = endZone.GetChild(1);
            if (buildingsFather.childCount != 0){
                destination = buildingsFather.GetChild(buildingsFather.childCount -1).GetComponent<Destination>();
                
                if (destination.IsFull && numbers.Contains(i))
                {
                    numbers.Remove(i);
                    if (numbers.Count > 0) OnUpdateNumbers?.Invoke();
                }
                else if (!destination.IsFull && !numbers.Contains(i)) {
                    numbers.Add(i);
                    OnUpdateNumbers?.Invoke();
                }
            }
        }
        DisableButton();
    }


    private void DisableButton()
    {
        if (numbers.Count == 0)  mainButton.interactable = false;   
        else  mainButton.interactable = true;
    }


    public void AddNewEndPoint()
    { 
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetChild(1).childCount == 0)
                {
                    endZone = transform.GetChild(i).GetComponent<EndZone>();
                    endZone.AddNewBuilding();
                    NumberOfActiveEndPoints++;
                    endZone.AmoutOfUpgrades++;
                    return;
                }
            }
        } 

    }


    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Output to console the clicked GameObject's name and the following message. You can replace this with your own actions for when clicking the GameObject.
        Debug.Log(name + " Game Object Clicked!");
    }


    



}
