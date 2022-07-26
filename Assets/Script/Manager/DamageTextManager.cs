using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextManager : MonoBehaviour
{
    #region 싱글톤
    private static DamageTextManager instance = null;

    public static DamageTextManager Instance {
        get {
            if(instance == null) {
                instance = GameObject.FindObjectOfType<DamageTextManager>();
                if (instance == null) {
                    Debug.LogError("There's no active DamageTextController Object");
                }
            }
            return instance;
        }
    }
    #endregion

    public Canvas canvas;
    public GameObject dmgTxt;
    public int random;
    public Camera cam;

    public void CreateDamageText(Vector3 hitPoint, int hitDamage) {
        Debug.Log(hitPoint);
        GameObject damageText = Instantiate(dmgTxt, hitPoint , Quaternion.identity, canvas.transform);
        damageText.GetComponentInChildren<Text>().text = hitDamage.ToString();
        random = Random.Range(0,4);
        damageText.GetComponentInChildren<Animator>().SetInteger("Random",random);
    }
}
