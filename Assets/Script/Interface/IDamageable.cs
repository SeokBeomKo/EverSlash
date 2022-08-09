using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IDamageable
{
    public IEnumerator OnDamage(int damage);
}
