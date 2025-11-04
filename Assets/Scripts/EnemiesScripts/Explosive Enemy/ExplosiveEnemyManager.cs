using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemyManager : MonoBehaviour
{
    private Rigidbody2D rb;

    public ExplosiveEnemyBehavior explosiveBehavior;

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
            explosiveBehavior.enabled = false;
        }
        else
        {
            explosiveBehavior.enabled = true;
        }
    }
}
