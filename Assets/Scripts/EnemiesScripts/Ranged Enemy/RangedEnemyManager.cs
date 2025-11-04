using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyManager : MonoBehaviour
{
    private Rigidbody2D rb;

    public EnemyRange rangedEnemy;
    public bool canDisable;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canDisable)
        {
            rb.linearVelocity = Vector3.zero;
            rangedEnemy.enabled = false;
        }
        else
        {
            rangedEnemy.enabled = true;
        }
    }
}
