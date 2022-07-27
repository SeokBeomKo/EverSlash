using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void Update()
    {
        if (target == null)
            target = GameManager.instance.player.transform;

        transform.position = target.position + offset;
    }
}
