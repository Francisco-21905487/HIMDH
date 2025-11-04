using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyManager : MonoBehaviour
{
    private Rigidbody2D rb;

    public MeleeEnemyBehavior meleeBehavior;
    public MeleeEnemyAttack meleeAttack;
    public WeaponFollowPlayer weaponFollowPlayer;

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
            meleeBehavior.enabled = false;
            meleeAttack.enabled = false;
            weaponFollowPlayer.enabled = false;
        }
        else
        {
            meleeBehavior.enabled = true;
            meleeAttack.enabled = true;
            weaponFollowPlayer.enabled = true;
        }
    }
}
