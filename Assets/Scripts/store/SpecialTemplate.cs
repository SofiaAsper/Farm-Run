using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpecialTemplate : MonoBehaviour
{
    private ShopManager shopManager;
    private GlobalUI GlobalUI;
    // public enum AnimalType { cow, sheep, pig };

    private void Start() {
        GlobalUI = GameObject.Find("UI").GetComponent<GlobalUI>();
        shopManager = GlobalUI.storeUI.GetComponent<ShopManager>();
    }
 
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text price;
    public Image image;
    public int orderNumber;
    public Destinations destinations;


    public void PurchaseItem()
    {
        shopManager.PurchaseItem(destinations , orderNumber);
    }
}
