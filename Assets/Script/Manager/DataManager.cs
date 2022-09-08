using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class UserData
{
    public int playerClass;
    public string name;
    public string playerClassName;
    public int level = 1;
    public string ability1;
    public string ability2;
    public string ability3;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public UserData nowPlayer = new UserData();
    public string path;
    public int nowSlot;
    private void Awake() {
        #region 싱글톤
        if (instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject);
        #endregion
        
        path = Application.persistentDataPath + "/save";
    }
    private void Start() {
        
    }
    public void SaveData(){
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowSlot.ToString(),data);
    }
    public void LoadData(){
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<UserData>(data);
    }

    public void DeleteData(){
        File.Delete(path + nowSlot.ToString());
    }

    public void DataClear(){
        nowSlot = -1;
        nowPlayer = new UserData();
    }

}
