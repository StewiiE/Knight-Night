using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public float currentHealth;
    float maxHealth = 100f;
    public float damage;

    private void Start()
    {

    }

    void Death()
    {
        Destroy(gameObject);
    }
}
