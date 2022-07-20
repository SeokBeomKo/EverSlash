using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassComplete : MonoBehaviour
{
    public int classObject = 10;
    public string className = null;
    public void StartClick()
    {
        if (classObject >= 10)
            return;
        // save
        DataManager.instance.nowPlayer.playerClass = classObject;
        DataManager.instance.nowPlayer.playerClassName = className;
        DataManager.instance.SaveData();

        SceneManager.LoadScene("03.GameScene");
    }
}
