using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    [SerializeField] int enemyLayer = 9;
    [SerializeField] float MaxHealthPoints = 100f;
    [SerializeField] float damagePerHit = 10f;

    private float currentHealthPoints = 100f;
    private GameObject currentTarget;
    private CameraRaycaster cameraRaycaster;

    public float healthAsPercentage { get { return currentHealthPoints / MaxHealthPoints; } }

    void Start()
    {
        cameraRaycaster = FindObjectOfType<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += OnMouseClick;
    }

    void OnMouseClick(RaycastHit raycastHit, int layerHit)
    {
        if (layerHit == enemyLayer)
        {
            var enemy = raycastHit.collider.gameObject;
            currentTarget = enemy;
            var enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent.TakeDamage(damagePerHit);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, MaxHealthPoints);
        //if (currentHealthPoints <= 0) { Destroy(gameObject); }
    }
}
