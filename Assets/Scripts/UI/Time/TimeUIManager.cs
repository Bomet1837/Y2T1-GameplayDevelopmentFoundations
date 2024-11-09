using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeUIManager : MonoBehaviour
{
    public static TimeUIManager instance;

    [Header("UI")] 
    [SerializeField] private GameObject timeUI;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text dateText;

    [Header("Customisation")] 
    [SerializeField] private bool playOnAwake = false;
    [SerializeField] private string timeAwakeInformation = "12:00 AM";
    [SerializeField] private string dateAwakeInformation = "1st January 2000";
    [SerializeField] private float durationAwake = 5f;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        if (playOnAwake)
        {
            ShowTime(timeAwakeInformation, dateAwakeInformation, durationAwake);
        }
    }

    public void ShowTime(string time, string date, float duration)
    {
        timeText.text = time;
        dateText.text = date;

        StartCoroutine(PlayAnimation(duration));
    }
    
    IEnumerator PlayAnimation(float duration)
    {
        timeUI.SetActive(true);
        timeUI.GetComponent<Animation>().Play("FadeIn");

        yield return new WaitForSeconds(1f);
        
        yield return new WaitForSeconds(duration);
        
        timeUI.GetComponent<Animation>().Play("FadeOut");
        
        yield return new WaitForSeconds(1f);
        
        timeUI.SetActive(false);
    }
}
