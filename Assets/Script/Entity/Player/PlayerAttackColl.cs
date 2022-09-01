using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackColl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Enemy"))    
        {
            StartCoroutine(other.transform.GetComponent<Enemy>().OnHit(10,0));
        }
    }
}
