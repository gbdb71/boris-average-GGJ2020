using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI textTimer = null;
    [SerializeField] private char characterSplitter = ':';

    [Header("Logic")]
    private float timer;
    private bool isActive;


    // Update is called once per frame
    void Update()
    {
        if (isActive)
		{
            timer += Time.deltaTime;
            UpdateText();
		}
        
    }

    private void UpdateText()
    {
        float seconds = timer % 60;
        float minutes = ((int)(timer / 60) % 60);
        textTimer.text = minutes.ToString("00") + characterSplitter + seconds.ToString("00");
    } 

    public void StartTimer() {
        StartTimer(0);
    }

    public void StartTimer(float seconds) {
        isActive = true;
        timer = seconds;
        UpdateText();
    }

    public void PauseTimer() {
        isActive = false;
    }

    public void ResetTimer() {
        StartTimer();
    }

    public void ToggleTimer() {
        isActive = !isActive;
        UpdateText();
    }

    public float getScore() {
        return timer;
    }
}
