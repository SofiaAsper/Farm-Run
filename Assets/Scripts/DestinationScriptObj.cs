
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Destination", menuName = "Destination", order = 0)]
public class DestinationScriptObj : ScriptableObject 
    
{

    public GameObject gameObject;
    public string buildingName;
    public int cost;
    public int capacity;
    public Slider slider;
    public Transform transform;
  

}
