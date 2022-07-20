using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public bool nextScene;
    public float i = 0;
    private void FixedUpdate() {
        i += 0.01f;
        RenderSettings.skybox.SetFloat("_Rotation",i);
        Button();
    }

    private void Button(){
        if (Input.anyKeyDown && nextScene){
            SceneManager.LoadScene("01.CharacterSelect");
        }
    }
}
