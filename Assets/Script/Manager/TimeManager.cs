using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public UnityAction<int> eventPerSecond;

    [SerializeField]
    private int _time;
    public int Time{
        get{return _time;}
        set
        {
            _time = value;
            eventPerSecond?.Invoke(_time);
        }
    }

    private void Awake()
    {
        #region 싱글톤
        if (instance == null) instance = this;
        else if (instance != null) return;
        #endregion
    }

}
