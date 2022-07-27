using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextManager : MonoBehaviour
{
    public static DamageTextManager instance;
    private void Awake() {
        #region 싱글톤
        if (instance == null) instance = this;
        else if (instance != null) return;
        #endregion
    }

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
