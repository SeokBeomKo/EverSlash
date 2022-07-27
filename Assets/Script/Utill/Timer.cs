using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;

    public int allSeconds;
    public int minutes;
    public int seconds;
    public bool isDoing;

    private void Start() {
        allSeconds = 0;
        minutes = 0;
        seconds = 0;
        isDoing = true;

        StartCoroutine(Count());
    }

    IEnumerator Count()
    {
        var wait = new WaitForSeconds(1f);
        while(isDoing)
        {
            Debug.Log("타이머 돌아");
            //isDoing = GameManager.instance.isAlive;
            SynchroTime();
            yield return wait;

            seconds++;
            allSeconds++;
            if (seconds == 60)
            {
                minutes++;
                seconds = 0;
            }
        }
    }

    private void SynchroTime()
    {
        TimeManager.instance.Time = allSeconds;
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
