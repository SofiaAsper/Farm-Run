using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    private bool inProgress;
    private DateTime TimerStart;
    private DateTime TimerEnd;


    [Header("Production Time")]
    public int Days;
    public int Hours;
    public int Minutes;
    public int Seconds;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI TimeLeftText;
    [SerializeField] private Button skipBtn;
    [SerializeField] private Button buildBtn;
    [SerializeField] private GameObject timerBubble;
    [SerializeField] private Slider timeSlider;

    #region Unity Methods


    #endregion

    #region UI Methods

    public void InitializeUI()
    {
        timerBubble.SetActive(true);
        TimeLeftText.text = "";
        StartCoroutine(DisplayTime());
    }

    private IEnumerator DisplayTime()
    {
        DateTime start = DateTime.Now;
        TimeSpan timeLeft = TimerEnd - start;
        double totalSecondsLeft = timeLeft.TotalSeconds;
        double totalSeconds = (TimerEnd - TimerStart).TotalSeconds;
        string text;

        while(timerBubble.activeSelf)
        {
            text = "";
            timeSlider.value = 1- Convert.ToSingle((TimerEnd - DateTime.Now).TotalSeconds / totalSeconds);

            if(totalSecondsLeft > 1)
            {
                if (timeLeft.Days != 0)
                {
                    text += timeLeft.Days + "d ";
                    text += timeLeft.Hours + "h ";
                    yield return new WaitForSeconds(timeLeft.Minutes * 60);
                }
                else if (timeLeft.Hours != 0)
                {
                    text += timeLeft.Hours + "h ";
                    text += timeLeft.Minutes + "m ";
                    yield return new WaitForSeconds(timeLeft.Seconds);
                }
                else if (timeLeft.Minutes != 0)
                {
                    TimeSpan ts = TimeSpan.FromSeconds(totalSecondsLeft);
                    text += ts.Minutes + "m ";
                    text += ts.Seconds + "s ";
                }
                else
                {
                    text += Mathf.FloorToInt((float) totalSecondsLeft) + "s ";
                }

                TimeLeftText.text = text;

                totalSecondsLeft -= Time.deltaTime;
                yield return null;
            }

            else 
            {
                TimeLeftText.text = "Finished";
                skipBtn.gameObject.SetActive(false);
                buildBtn.gameObject.SetActive(true);
                inProgress = false;
                break;
             
            }
        }
        yield return null;
    }

    #endregion

    #region Timed Events
    public void StartTimer()
    {
        TimerStart = DateTime.Now;
        TimeSpan time = new TimeSpan(Days, Hours, Minutes, Seconds);
        TimerEnd = TimerStart.Add(time);
        inProgress = true;

        StartCoroutine(Timer());

        InitializeUI();
    }

    private IEnumerator Timer()
    {
        DateTime start = DateTime.Now;
        double secondsToFinish = (TimerEnd - start).TotalSeconds;
        yield return new WaitForSeconds(Convert.ToSingle(secondsToFinish));

        inProgress = false;
        // Do something when the timer is finished - add new building..
        Debug.Log("Timer is finished");
    }
    #endregion



}
