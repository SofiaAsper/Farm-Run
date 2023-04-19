using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementManager : MonoBehaviour
{
    [Header("Achievements UI Elements")]
    [SerializeField] GameObject achPrefab;
    [SerializeField] Text achAmountText;
    [SerializeField] GameObject achNotification;
    [SerializeField] GameObject achPanel;
    [SerializeField] GameObject achIconBtn;

    [SerializeField] GameObject instantiateFather;

    [Header("Achievements images")]
    public Sprite cowAch;
    public Sprite chickenAch;
    public Sprite sheepAch;
    public Sprite pigAch;
    public Sprite stepByStepAch;
    public Sprite upgradeAch;


    public static List<Achievement> achievements;


    [Header("Parameters")]
    public int numOfActiveAchievements;
    public int integer;

    public bool AchievementUnlocked(string achievementName)
    {
        bool result = false;

        if (achievements == null)
            return false;

        Achievement[] achievementsArray = achievements.ToArray();
        Achievement a = Array.Find(achievementsArray, ach => achievementName == ach.title);

        if (a == null)
            return false;

        result = a.achieved;

        return result;
    }

    private void Start()
    {
        InitializeAchievements();
        CleanPlayerPrefs();  
        //*******************delete this line when you are done testing
         if (achievements == null)
            return;

        foreach (var achievement in achievements)
        {
            achievement.UpdateAchievements();
        }
    }

    private void InitializeAchievements()
    {
        if (achievements != null)
            return;

        achievements = new List<Achievement>();
        achievements.Add(new Achievement("AchS1","Step By Step", "Unlock the PIGGY home", 5, (object o) => PlayerPrefs.HasKey("EZPi1")));
        achievements.Add(new Achievement("AchS2","Step By Step", "Unlock the COW home",7, (object o) => PlayerPrefs.HasKey("EZCo1")));
        achievements.Add(new Achievement("AchS3","Step By Step", "Unlock the SHEEP home",10, (object o) => PlayerPrefs.HasKey("EZSh1")));

        achievements.Add(new Achievement("AchU1","Upgraded", "Unlock All areas",20, (object o) => PlayerPrefs.HasKey("EZCh2") && PlayerPrefs.HasKey("EZCh3") && PlayerPrefs.HasKey("EZCo1") && PlayerPrefs.HasKey("EZPi1") && PlayerPrefs.HasKey("EZSh1") && PlayerPrefs.HasKey("EZSh2")));
        achievements.Add(new Achievement("AchU2","Upgraded", "Upgrade All areas at least once",40, (object o) => PlayerPrefs.GetInt("EZCh1") > 0 && PlayerPrefs.GetInt("EZCh2") > 0 && PlayerPrefs.GetInt("EZCh3") > 0 && PlayerPrefs.GetInt("EZCo1") > 0 && PlayerPrefs.GetInt("EZPi1") > 0 && PlayerPrefs.GetInt("EZSh1") > 0 && PlayerPrefs.GetInt("EZSh2") > 0));
        achievements.Add(new Achievement("AchU3","Upgraded", "Upgrade All areas to the fullest",400, (object o) => PlayerPrefs.GetInt("EZCh1") >= 3 && PlayerPrefs.GetInt("EZCh2") >= 3 && PlayerPrefs.GetInt("EZCh3") >= 3 && PlayerPrefs.GetInt("EZCo1") >= 2 && PlayerPrefs.GetInt("EZPi1") >= 3 && PlayerPrefs.GetInt("EZSh1") >= 2 && PlayerPrefs.GetInt("EZSh2") >= 2));
        achievements.Add(new Achievement("AchU4","Upgraded", "Upgrade the Chicken area at least once",5, (object o) => PlayerPrefs.GetInt("EZCh1") >= 2 && PlayerPrefs.GetInt("EZCh2") >= 2 && PlayerPrefs.GetInt("EZCh3") >= 2));
        achievements.Add(new Achievement("AchU5","Upgraded", "Upgrade the Chicken area at least twice",15, (object o) => PlayerPrefs.GetInt("EZCh1") >= 3 && PlayerPrefs.GetInt("EZCh2") >= 3 && PlayerPrefs.GetInt("EZCh3") >= 3));
        achievements.Add(new Achievement("AchU6","Upgraded", "Upgrade the Pig area at least once",12, (object o) => PlayerPrefs.GetInt("EZPi1") >= 2));
        achievements.Add(new Achievement("AchU7","Upgraded", "Upgrade the Pig area at least twice",20, (object o) => PlayerPrefs.GetInt("EZPi1") >= 3));
        achievements.Add(new Achievement("AchU8","Upgraded", "Upgrade the Cow area at least once",22, (object o) => PlayerPrefs.GetInt("EZCo1") >= 2));
        achievements.Add(new Achievement("AchU9","Upgraded", "Upgrade the Sheep area at least once",15, (object o) => PlayerPrefs.GetInt("EZSh1") >= 2 && PlayerPrefs.GetInt("EZSh2") >= 2));
        // achievements.Add(new Achievement("AchU10","Upgraded", "Upgrade the Sheep area at least twice",45, (object o) => PlayerPrefs.GetInt("EZSh2") >= 3 && PlayerPrefs.GetInt("EZSh2") >= 3));

        
        achievements.Add(new Achievement("AchE1","Eggs master", "Get 50 eggs", 5, (object o) => GameData.Eggs >= 50));
        achievements.Add(new Achievement("AchE2","Eggs master", "Get 100 eggs",10, (object o) => GameData.Eggs >= 100));
        achievements.Add(new Achievement("AchE3","Eggs master", "Get 500 eggs",50, (object o) => GameData.Eggs >= 500));
        achievements.Add(new Achievement("AchE4","Eggs master", "Get 1000 eggs",100 ,(object o) => GameData.Eggs >= 1000));
        achievements.Add(new Achievement("AchE5","Eggs master", "Get 2000 eggs",300, (object o) => GameData.Eggs >= 2000));

        achievements.Add(new Achievement("AchM1","Milk master", "Get 50 milk",5, (object o) => GameData.Milk >= 50));
        achievements.Add(new Achievement("AchM2","Milk master", "Get 100 milk",10, (object o) => GameData.Milk >= 100));
        achievements.Add(new Achievement("AchM3","Milk master", "Get 500 milk",30, (object o) => GameData.Milk >= 500));
        achievements.Add(new Achievement("AchM4","Milk master", "Get 1000 milk",100, (object o) => GameData.Milk >= 1000));
        achievements.Add(new Achievement("AchM5","Milk master", "Get 2000 milk",300, (object o) => GameData.Milk >= 2000));

        achievements.Add(new Achievement("AchH1","Ham master", "Get 50 ham",5, (object o) => GameData.Ham >= 50));
        achievements.Add(new Achievement("AchH2","Ham master", "Get 100 ham",10, (object o) => GameData.Ham >= 100));
        achievements.Add(new Achievement("AchH3","Ham master", "Get 500 ham", 30,(object o) => GameData.Ham >= 500));
        achievements.Add(new Achievement("AchH4","Ham master", "Get 1000 ham",100, (object o) => GameData.Ham >= 1000));
        achievements.Add(new Achievement("AchH5","Ham master", "Get 2000 ham",300, (object o) => GameData.Ham >= 2000));

        achievements.Add(new Achievement("AchW1","Wool master", "Get 50 wool",5, (object o) => GameData.Wool >= 50));
        achievements.Add(new Achievement("AchW2","Wool master", "Get 100 wool",10, (object o) => GameData.Wool >= 100));
        achievements.Add(new Achievement("AchW3","Wool master", "Get 500 wool", 30,(object o) => GameData.Wool >= 500));
        achievements.Add(new Achievement("AchW4","Wool master", "Get 1000 wool",100, (object o) => GameData.Wool >= 1000));
        achievements.Add(new Achievement("AchW5","Wool master", "Get 2000 wool",300, (object o) => GameData.Wool >= 2000));

    }

    private void Update()
    {
        CheckAchievementCompletion();
    }

    private void CheckAchievementCompletion()
    {
            if (achievements == null)
            return;

        foreach (var achievement in achievements)
        {
            achievement.UpdateCompletion();
            if (achievement.achieved && !achievement.unlocked && !achievement.counted)
            {
                numOfActiveAchievements ++;
                achievement.counted = true;
            }
            else if (achievement.unlocked && !achievement.completed)
            {
                numOfActiveAchievements --;
                // achievement.completed = true;
                PlayerPrefs.SetInt(achievement.code, 3);
                achievement.UpdateAchievements( );
            }

        }
        // update the notification number
        if(numOfActiveAchievements > 0)
        {
            achNotification.SetActive(true);
            // start animation
            achIconBtn.GetComponent<Animator>().SetTrigger("Ready");
            achAmountText.text = numOfActiveAchievements.ToString();
        }
        else
        {
            achNotification.SetActive(false);
            achIconBtn.GetComponent<Animator>().SetTrigger("NoAchivements");
            if (achPanel.activeSelf)    achPanel.SetActive(false);
        }


    }

    public void OpenActiveAchList()
    {
        //check if achPanel is active
        if (achPanel.activeSelf)
        {
            achPanel.SetActive(false);
            return;
        }
        else
        {
            achPanel.SetActive(true);
            ShowActiveAchivements();
        }
    }

    private void ShowActiveAchivements()
    {
        if (achievements == null)
            return;

        foreach (var achievement in achievements)
        {
            if (achievement.achieved && !achievement.unlocked && !achievement.shown )
            {
                // instantiate achievement prefab
                GameObject AchievementTemp = Instantiate(achPrefab,instantiateFather.transform);
                AchievementTemp.GetComponent<AchievementTemplate>().Initialize(achievement);
                achievement.shown = true;

            }
        }
    }

    private void CleanPlayerPrefs()
    {
        if (achievements == null)
            return;
        foreach (var achievement in achievements)
        {
            //if the key code exist in player prefs delete it
            if (PlayerPrefs.HasKey(achievement.code)){
                PlayerPrefs.DeleteKey(achievement.code);
            }
        }
    }



}

// ********************************************************************************************************************** Achievement**************************

public class Achievement
{
    public Achievement(string code, string title, string description, float amount, Predicate<object> requirement)
    {
        this.code = code;
        this.title = title;
        this.description = description;
        this.amount = amount;
        this.requirement = requirement;
    }
// Achieved = 1, Unlocked = 2, Completed = 3, counted 4
    public string code;
    public string title;
    public string description;
    public float amount;
    public Predicate<object> requirement;

    public bool achieved = false;
    public bool completed = false;
    public bool unlocked = false;
    public bool counted = false;
    public bool shown = false;

    public void UpdateAchievements(){
        //check if the code is inside the playerprefs
        if (PlayerPrefs.HasKey(code))
        {
            //check if the code is completed
            if (PlayerPrefs.GetInt(code) == 3) completed = true;
            
            else if (PlayerPrefs.GetInt(code) == 2) unlocked = true;
            
            else if (PlayerPrefs.GetInt(code) == 1) achieved = true;
            
        }
    }

    public void UpdateCompletion()
    {
        if (achieved)
            return;

        if (RequirementsMet())
        {
            achieved = true;
            //udate the player prefs to show that the achievement has been Achieved
            PlayerPrefs.SetInt(code, 1);
            UpdateAchievements();

        }
    }

    public bool RequirementsMet()
    {
        return requirement.Invoke(null);
    }
}
