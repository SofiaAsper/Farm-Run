using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPlayScene : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonTxt;
    [SerializeField] Slider loadingBar;
    [SerializeField] GameObject panel;

    void Start()
    {
        // loadingBar.value = 0f;
    }

    public void LoadScene()
    {
        buttonTxt.text = "Loading...";
        StartCoroutine(StartGame());



        // loadingBar.gameObject.SetActive(true);
        // Debug.Log(SceneManager.GetActiveScene().name);
        // StartCoroutine(LoadAsyncScene());
        //disable the button
        // gameObject.GetComponent<Button>().interactable = false;
        //load the scene
        // SceneManager.LoadScene("GamePlay",LoadSceneMode.Single);
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        panel.SetActive(false);
    }


    // Start is called before the first frame update

    // IEnumerator LoadAsyncScene()
    // {
    //     Debug.Log("Im loading");
    //     //create an async operation
    //     AsyncOperation gamePlay = SceneManager.LoadSceneAsync("GamePlay",LoadSceneMode.Single);
    //     while (gamePlay.progress < 1)
    //     {
    //         // take the progress bar and fill with async operation progress
    //         loadingBar.value = gamePlay.progress;
    //         yield return new WaitForEndOfFrame();
    //     }
    // }
}
