using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageable
{
    public IEnumerator OnHit(int _damage, int _ignore);
}
