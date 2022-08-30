using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    public int minDamage;
    public int maxDamage;
    public int ignore;
    private void OnEnable() 
    {
        StartCoroutine("AutoDisable");
    }
    
    private IEnumerator AutoDisable()
    {
        yield return new WaitForFixedUpdate();
        gameObject.SetActive(false);
    }
    public void SetDamage(int min, int max, int ig)
    {
        minDamage = min;
        maxDamage = max;
        ignore = ig;
    }

    public int TurnIgnore()
    {
        return ignore;
    }
    public int TurnDamage()
    {
        int rand = Random.Range(minDamage,maxDamage);
        return rand;
    }
}
