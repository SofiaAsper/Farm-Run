using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeTemplate : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text description;
    public TMP_Text price;
    public TMP_Text progress;
    public Image image;
    public Slider slider;
    public int orderNumber;

    private ShopManager shopManager;
    private GlobalUI GlobalUI;

    private void Start() {
        GlobalUI = GameObject.Find("UI").GetComponent<GlobalUI>();
        shopManager = GlobalUI.storeUI.GetComponent<ShopManager>();
    }

    public void PurchaseItem()
    {
        shopManager.PurchaseUpgrade(orderNumber);
        //manage buying getting the order number and calling the purchase upgrade method
        
    }
}
