using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    public static PlayerTargeting Instance // singlton
    {
        get{
           if (instance == null){
               instance = FindObjectOfType<PlayerTargeting>();
               if(Instance == null){
                   var instanceContainer = new GameObject("PlayerTargeting");
                   instance = instanceContainer.AddComponent<PlayerTargeting>();
               }
           }
           return instance;
        }
    }
    private static PlayerTargeting instance;
}
