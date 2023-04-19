using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] GlobalUI GlobalUI;
    // [SerializeField] GameObject StoreUIPanel;
    [SerializeField] GameObject notEnoughMoneyImg;

    [Space ]
    [Header("Shop Panel Components")]

    [SerializeField] GameObject headerPanel;
    [SerializeField] GameObject upgradesPanel;
    [SerializeField] GameObject buildingsPanel;
    [SerializeField] GameObject sectionsPanel;
    [SerializeField] GameObject packsPanel;

    [Space]
    [Header ( "Section Buttons" )] 
      // sections buttons
    [SerializeField] Toggle buildingToggle;
    [SerializeField] Toggle upgradesToggle;
    [SerializeField] Toggle packsToggle;

    [Space]
    [Header ("Templates for Sections")] 

    public BuildingTemplate[] buildingTemplates;
    public UpgradeTemplate[] upgradeTemplates;
    public SpecialTemplate[] specialTemplates;
    private List<UpgradeSO> upgardeSOs = new List<UpgradeSO>();
    public List<SpecialSO> specialSOs = new List<SpecialSO>();

    private Destinations destinations;
    private EndZone endZone;
    private HeaderPanel headerPanelScript;
    private ManageBuying manageBuying;

    // ******** Event ********
    public delegate void UpdateAction();
    public static event UpdateAction OnPurchase;

    // ******** Event ********

    
    private void Start() {
        manageBuying = GetComponent<ManageBuying>();
        GlobalUI = GameObject.Find("UI").GetComponent<GlobalUI>();
        gameObject.SetActive(false);
        // LoadPanels();
        // LoadUpgrades();
    }

    private void notEnoughPopUp(){
        // instantiate the not enough money gameobject and destroy it after 1 second
        GameObject notEnoughMoney = Instantiate(notEnoughMoneyImg, GlobalUI.transform);
        Destroy(notEnoughMoney, 2f);
    }
  

    public void UpdateTxt() {
        GlobalUI.UpdateUpperPannelTxt();
    }

    public void UpdateHeaderTxt() {
        if (headerPanelScript != null) 
        {
            headerPanelScript.coins.text = GameData.NumShortCut(GameData.Coins);
            headerPanelScript.money.text = GameData.NumShortCut(GameData.Money);
            headerPanelScript.milk.text = GameData.NumShortCut(GameData.Milk);
            headerPanelScript.wool.text = GameData.NumShortCut(GameData.Wool);
            headerPanelScript.ham.text = GameData.NumShortCut(GameData.Ham);
            headerPanelScript.eggs.text = GameData.NumShortCut(GameData.Eggs);

        }
    }


    private void OnEnable() {
        //update the text of the skulls and money in the header panel
        headerPanelScript = headerPanel.GetComponent<HeaderPanel>();
        UpdateHeaderTxt();
    }

    // when the player closes the store UI, all the panels are hidden
    public void CloseStore()
    {
        upgradesPanel.SetActive(false);
        buildingsPanel.SetActive(false);
        packsPanel.SetActive(false);
        gameObject.SetActive(false);
        ResetColor();
    }

    private void ResetColor()
    {
        buildingToggle.GetComponent<Image>().color = Color.white;
        upgradesToggle.GetComponent<Image>().color = Color.white;
        packsToggle.GetComponent<Image>().color = Color.white;
    }

    public void OpenStoreUpgrades()
    {
        //check if the game object is active
        if (!upgradesPanel.activeInHierarchy)
        {
            gameObject.SetActive(true);
            LoadUpgrades();
            CheckPurchaseableUpgrades();
            upgradesToggle.isOn = true;
        }
        gameObject.SetActive(true);
        packsPanel.SetActive(false);
        upgradesPanel.SetActive(true);
        buildingsPanel.SetActive(false);
        // change the toggle color to 93D79E color
        ResetColor();
        upgradesToggle.GetComponent<Image>().color = new Color32(147, 215, 158, 255);
    }

    public void OpenStoreBuildings()
    {
        Debug.Log(gameObject.activeSelf); // log
        buildingsPanel.SetActive(true);
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            CheckPurchaseableBuildings(destinations);
            buildingToggle.isOn = true;
        }
        packsPanel.SetActive(false);
        upgradesPanel.SetActive(false);
        // change the toggle color to 93D79E color
        ResetColor();
        buildingToggle.GetComponent<Image>().color = new Color32(147, 215, 158, 255);
    }

    public void GetDestinations(Destinations destinations)
    {
        this.destinations = destinations;
        LoadPanels(destinations);
    }

    public void OpenStorePacks()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            // LoadPacks();
            packsToggle.isOn = true;
        }
        packsPanel.SetActive(true);
        upgradesPanel.SetActive(false);
        buildingsPanel.SetActive(false);
        // LoadPacks();
        // CheckPurchaseablePacks();
        // change the toggle color to 93D79E color
        ResetColor();
        packsToggle.GetComponent<Image>().color = new Color32(147, 215, 158, 255);
    }

    public string NumShortCut(float price , string paymentType)
    {  
        switch (price)
        {
            case float n when n >= 1000 && n < 1000000:
                return paymentType +": " + (price / 1000).ToString() + "K";
            case float n when n >= 1000000 && n < 1000000000:
                return paymentType +": " + (price / 1000000).ToString() + "M";
            case float n when n >= 1000000000 && n < 1000000000000:
                return paymentType +": " + (price / 1000000000).ToString() + "B";
            case float n when n >= 1000000000000 && n < 1000000000000000:
                return paymentType +": " + (price / 1000000000000).ToString() + "T";
            case float n when n >= 1000000000000000:
                return paymentType +": " + (price / 1000000000000000).ToString() + "Q";
            default:            
                return paymentType +": " + price.ToString();
        }
    }
    //************************************************************** Buildings ************************************************************


    public void LoadPanels(Destinations destinations)
    {
        foreach (BuildingTemplate buildingTemplate in buildingTemplates)
        {
            buildingTemplate.gameObject.SetActive(false);
        }
        for (int i = 1; i < destinations.buildingsList.Count; i++)
        {
            buildingTemplates[i-1].gameObject.SetActive(true);
            buildingTemplates[i-1].title.text = destinations.buildingsList[i].buildingName;
            buildingTemplates[i-1].description.text = destinations.buildingsList[i].description;
            buildingTemplates[i-1].price.text = NumShortCut(destinations.buildingsList[i].cost , destinations.buildingsList[i].paymentType.ToString());
            buildingTemplates[i-1].image.sprite = destinations.buildingsList[i].image;
            buildingTemplates[i-1].orderNumber = i;
            buildingTemplates[i-1].destinations = destinations;

        }
    }


    public void CheckPurchaseableBuildings(Destinations destinations)
    {
        for (int i = 1; i < destinations.buildingsList.Count; i++)
        {
            BuildingSO SO = destinations.buildingsList[i];
            Transform buildingsFather = endZone.transform.GetChild(1);
            int numOfBuildings = buildingsFather.childCount;
            switch (SO.paymentType.ToString())
            {
                case "coins":
                    if (GameData.Coins >= SO.cost && i >= numOfBuildings)
                    {
                        buildingTemplates[i-1].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        buildingTemplates[i-1].GetComponent<Button>().interactable = false;
                    }
                    break;

                case "money":
                    if (GameData.Money >= SO.cost && i >= numOfBuildings)
                    {
                        buildingTemplates[i-1].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        buildingTemplates[i-1].GetComponent<Button>().interactable = false;
                    }
                    break;

                case "milk":
                    if (GameData.Milk >= SO.cost && i >= numOfBuildings)
                    {
                        buildingTemplates[i-1].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        buildingTemplates[i-1].GetComponent<Button>().interactable = false;
                    }
                    break;

                case "wool":
                    if (GameData.Wool >= SO.cost && i >= numOfBuildings)
                    {
                        buildingTemplates[i-1].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        buildingTemplates[i-1].GetComponent<Button>().interactable = false;
                    }
                    break;

                case "ham":
                    if (GameData.Ham >= SO.cost && i >= numOfBuildings)
                    {
                        buildingTemplates[i-1].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        buildingTemplates[i-1].GetComponent<Button>().interactable = false;
                    }
                    break;

                case "eggs":
                    if (GameData.Eggs >= SO.cost && i >= numOfBuildings)
                    {
                        buildingTemplates[i-1].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        buildingTemplates[i-1].GetComponent<Button>().interactable = false;
                    }
                    break;

                default:
                    break;
            }
        }
    }

    public void PurchaseFirstBuilding(Destinations destinations)
    {
        BuildingSO SO = destinations.buildingsList[0];
        PaymentTypeCheck(SO , destinations);
    }

    public void PurchaseItem(Destinations destinations , int orderNumber)
    {
        BuildingSO SO = destinations.buildingsList[orderNumber];
        PaymentTypeCheck(SO , destinations);
    }


private void PaymentTypeCheck(BuildingSO SO , Destinations destinations)
{        switch (SO.paymentType.ToString())
        {
        case "coins":
            if (GameData.Coins >= SO.cost)
            {
                GameData.Coins -= SO.cost;
                PurchaseSequence(destinations);
            }
            else
            {
                Debug.Log("Not enough coins");
                notEnoughPopUp();
            }
            break;

        case "money":
            if (GameData.Money >= SO.cost)
            {
                GameData.Money -= SO.cost;
                PurchaseSequence(destinations);
            }
            else
            {
                Debug.Log("Not enough money " + "You have " + GameData.Money + " and you need " + SO.cost);
                notEnoughPopUp();
            }
            break;

        case "milk":
            if (GameData.Milk >= SO.cost)
            {
                GameData.Milk -= SO.cost;
                PurchaseSequence(destinations);
            }
            else
            {
                Debug.Log("Not enough milk");
                notEnoughPopUp();
            }
            break;

        case "wool":
            if (GameData.Wool >= SO.cost)
            {
                GameData.Wool -= SO.cost;
                PurchaseSequence(destinations);
            }
            else
            {
                Debug.Log("Not enough wool");
                notEnoughPopUp();
            }
            break;

        case "ham":
            if (GameData.Ham >= SO.cost)
            {
                GameData.Ham -= SO.cost;
                PurchaseSequence(destinations);
            }
            else
            {
                Debug.Log("Not enough ham");
                notEnoughPopUp();
            }
            break;

        case "eggs":
            if (GameData.Eggs >= SO.cost)
            {
                GameData.Eggs -= SO.cost;
                PurchaseSequence(destinations);
            }
            else
            {
                Debug.Log("Not enough eggs");
                notEnoughPopUp();
            }
            break;

        default:
        Debug.Log("Payment type not found");
            break;
        }
    }


    public void GetTheEndZone(EndZone endZone)
    {
        this.endZone = endZone;
        //get the Destinations script from the father of the end zone
        Destinations destinations = endZone.transform.parent.GetComponent<Destinations>();
        LoadPanels(destinations);
        CheckPurchaseableBuildings(destinations);
    }

    private void PurchaseSequence(Destinations destinations)
    {
        if (endZone != null) {
            endZone.AddNewBuilding();
            endZone.AmoutOfUpgrades++;
            endZone.slider.ResetSliderValue() ;
            CheckPurchaseableBuildings(destinations);
        }
        else destinations.AddNewEndPoint();
        UpdateTxt();
        UpdateHeaderTxt();
        LoadPanels(destinations);
        ResetEndZone();
        OnPurchase?.Invoke();        
    }

    public void ResetEndZone()
    {
        endZone = null;
    }
    

    //************************************************************** Upgrades ************************************************************
    public void LoadUpgrades()
    { 
        upgardeSOs = destinations.upgradesList;
        foreach (UpgradeTemplate upgradeTemplate in upgradeTemplates)
        {
            upgradeTemplate.gameObject.SetActive(false);
        }
        for (int i = 0; i < upgardeSOs.Count; i++)
        {
            upgradeTemplates[i].gameObject.SetActive(true);
            upgradeTemplates[i].title.text = upgardeSOs[i].title;
            upgradeTemplates[i].description.text = upgardeSOs[i].description + "- Increase by " + (upgardeSOs[i].increasePercentage *100) + "%";
            upgradeTemplates[i].price.text = NumShortCut(upgardeSOs[i].cost , upgardeSOs[i].paymentType.ToString());
            upgradeTemplates[i].progress.text = upgardeSOs[i].progress.ToString() + "/" + upgardeSOs[i].maxUpgrade.ToString();
            upgradeTemplates[i].image.sprite = upgardeSOs[i].image;
            upgradeTemplates[i].slider.maxValue = upgardeSOs[i].maxUpgrade;
            upgradeTemplates[i].slider.value = upgardeSOs[i].progress;
            upgradeTemplates[i].orderNumber = i;
        
        }
    }

    public void CheckPurchaseableUpgrades()
    {
        for (int i = 0; i < upgardeSOs.Count; i++)
        {
            switch (upgardeSOs[i].paymentType.ToString())
            {
                case "coins":
                    if (GameData.Coins >= upgardeSOs[i].cost)
                    {
                        upgradeTemplates[i].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        upgradeTemplates[i].GetComponent<Button>().interactable = false;
                    }
                    break;

                case "money":
                    if (GameData.Money >= upgardeSOs[i].cost)
                    {
                        upgradeTemplates[i].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        upgradeTemplates[i].GetComponent<Button>().interactable = false;
                    }
                    break;

                case "milk":
                    if (GameData.Milk >= upgardeSOs[i].cost)
                    {
                        upgradeTemplates[i].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        upgradeTemplates[i].GetComponent<Button>().interactable = false;
                    }
                    break;

                case "wool":
                    if (GameData.Wool >= upgardeSOs[i].cost)
                    {
                        upgradeTemplates[i].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        upgradeTemplates[i].GetComponent<Button>().interactable = false;
                    }
                    break;

                case "ham":
                    if (GameData.Ham >= upgardeSOs[i].cost)
                    {
                        upgradeTemplates[i].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        upgradeTemplates[i].GetComponent<Button>().interactable = false;
                    }
                    break;

                case "eggs":
                    if (GameData.Eggs >= upgardeSOs[i].cost)
                    {
                        upgradeTemplates[i].GetComponent<Button>().interactable = true;
                    }
                    else
                    {
                        upgradeTemplates[i].GetComponent<Button>().interactable = false;
                    }
                    break;

                default:
                    break;
            }
        }
       
    }

    public void PurchaseUpgrade(int SOnum)
    {
        switch (upgardeSOs[SOnum].paymentType.ToString())
        {
            case "coins":
                if (GameData.Coins >= upgardeSOs[SOnum].cost)
                {
                    GameData.Coins -= upgardeSOs[SOnum].cost;
                    PurchaseUpgradeSequence(SOnum);
                }
                else
                {
                    Debug.Log("Not enough coins");
                    
                }
                break;

            case "money":
                if (GameData.Money >= upgardeSOs[SOnum].cost)
                {
                    GameData.Money -= upgardeSOs[SOnum].cost;
                    PurchaseUpgradeSequence(SOnum);
                }
                else
                {
                    Debug.Log("Not enough money");
                }
                break;

            case "milk":
                if (GameData.Milk >= upgardeSOs[SOnum].cost)
                {
                    GameData.Milk -= upgardeSOs[SOnum].cost;
                    PurchaseUpgradeSequence(SOnum);
                }
                else
                {
                    Debug.Log("Not enough milk");
                }
                break;

            case "wool":
                if (GameData.Wool >= upgardeSOs[SOnum].cost)
                {
                    GameData.Wool -= upgardeSOs[SOnum].cost;
                    PurchaseUpgradeSequence(SOnum);
                }
                else
                {
                    Debug.Log("Not enough wool");
                }
                break;  

            case "ham":
                if (GameData.Ham >= upgardeSOs[SOnum].cost)
                {
                    GameData.Ham -= upgardeSOs[SOnum].cost;
                    PurchaseUpgradeSequence(SOnum);
                }
                else
                {
                    Debug.Log("Not enough ham");
                }
                break;

            case "eggs":
                if (GameData.Eggs >= upgardeSOs[SOnum].cost)
                {
                    GameData.Eggs -= upgardeSOs[SOnum].cost;
                    PurchaseUpgradeSequence(SOnum);
                }
                else
                {
                    Debug.Log("Not enough eggs");
                }
                break;

            default:
                break;
        }

    }

        public void PurchaseUpgradeSequence(int SOnum)
        {
            upgardeSOs[SOnum].progress++;
            // round up to nearest integer
            upgardeSOs[SOnum].cost = Mathf.CeilToInt(upgardeSOs[SOnum].cost * 1.2f);
            manageBuying.BuyUpgrade(upgardeSOs[SOnum]);
            //update the cost of the upgrade


            if (upgardeSOs[SOnum].progress >= upgardeSOs[SOnum].maxUpgrade)
            {
                upgradeTemplates[SOnum].GetComponent<Button>().interactable = false;
                //change the color of the button to green
                upgradeTemplates[SOnum].GetComponent<Image>().color = Color.green;

                //Get coins
                GameData.Coins += 20;
            }
            upgradeTemplates[SOnum].progress.text = upgardeSOs[SOnum].progress.ToString() + "/" + upgardeSOs[SOnum].maxUpgrade.ToString();
            upgradeTemplates[SOnum].slider.value = upgardeSOs[SOnum].progress;
            CheckPurchaseableUpgrades();
            UpdateTxt();
            UpdateHeaderTxt();
            LoadUpgrades();
            // OnPurchase?.Invoke();
        }

    


}
