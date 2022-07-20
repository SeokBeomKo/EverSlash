using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public void DestoryEvent() {
        Destroy(transform.parent.gameObject);
    }
}
