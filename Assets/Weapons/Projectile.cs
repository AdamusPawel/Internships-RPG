using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    
    public float projectileSpeed; // other classes can set it.
    float damageCaused;

    public void SetDamage(float damage)
    {
        damageCaused = damage;
    }

    void OnCollisionEnter(Collision collision)
    {
        Component damageableComponent = collision.gameObject.GetComponent(typeof(IDamageable));
        print("Damageable component" + damageableComponent);
        if (damageableComponent)
        {
            (damageableComponent as IDamageable).TakeDamage(damageCaused);
        }
    }
}
