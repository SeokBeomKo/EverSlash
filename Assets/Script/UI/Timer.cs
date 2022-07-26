using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;

    public int minutes;
    public int seconds;

    private void Awake() {
        minutes = 0;
        seconds = 0;

        StartCoroutine(Count());
    }

    IEnumerator Count()
    {
        SetUI();
        yield return new WaitForSeconds(1f);
        seconds++;
        if (seconds == 60)
        {
            minutes++;
            seconds = 0;
        }
        StartCoroutine(Count());
    }

    private void SetUI()
    {
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
