using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Enemy : MonoBehaviour
{

    [SerializeField] float MaxHealthPoints = 100f;
    [SerializeField] private float attackRadius = 4f;
    GameObject player = null;

    private float currentHealthPoints = 100f;
    AICharacterControl aiCharacterControl = null;

    public float healthAsPercentage
    {
        get
        {
            return currentHealthPoints / MaxHealthPoints;
        }
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        aiCharacterControl = GetComponent<AICharacterControl>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceToPlayer <= attackRadius)
        {
            aiCharacterControl.SetTarget(player.transform);
        }
        else
        {
            aiCharacterControl.SetTarget(transform);
        }
    }
}
