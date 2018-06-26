using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    [SerializeField] private float MaxHealthPoints = 100f;

    private float currentHealthPoints = 100f;

    public float healthAsPercentage { get { return currentHealthPoints / MaxHealthPoints; } }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, MaxHealthPoints);
        if (currentHealthPoints <= 0) { Destroy(gameObject); }

    }
}
