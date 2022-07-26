using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    private void Awake()
    {
        #region 싱글톤
        if (instance == null) instance = this;
        else if (instance != null) return;
        #endregion

        StartCoroutine(Count());
    }

    IEnumerator Count()
    {
        yield return new WaitForSeconds(1);
    }
}
