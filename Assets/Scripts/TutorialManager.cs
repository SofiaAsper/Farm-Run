using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] private GameObject[] tutorialPages;
    [SerializeField] private GameObject tutorialCanvas;

    private bool tutorailActive;


    private void Start() {
        if (PlayerPrefs.HasKey("TutorialActive") && PlayerPrefs.GetInt("TutorialActive") == 0) tutorailActive = false;
        else if(PlayerPrefs.HasKey("TutorialActive") && PlayerPrefs.GetInt("TutorialActive") == 1) tutorailActive = true;
        else {
            PlayerPrefs.SetInt("TutorialActive", 1);
            tutorailActive = true;
            }

        if (tutorailActive)
        {
            Invoke("InitialTutorial", 1f);
        }

    }

    public void EndTutorial()
    {
        tutorailActive = false;
        PlayerPrefs.SetInt ( "TutorialActive", 0 );
    }

    public void StartTutorial()
    {
        tutorialCanvas.SetActive(true);
        tutorialPages[0].SetActive(true);
        tutorialPages[1].SetActive(false);
        tutorialPages[2].SetActive(false);
    }

    void InitialTutorial()
    {
            tutorialCanvas.SetActive(true);
            tutorialPages[0].SetActive(true);
    }




}
