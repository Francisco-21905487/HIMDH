using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private Rigidbody2D rb;

    public BossBehavior bossBehavior;
    public BossAttack BossAttack;
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
            bossBehavior.enabled = false;
            BossAttack.enabled = false;
            weaponFollowPlayer.enabled = false;
        }
        else
        {
            bossBehavior.enabled = true;
            BossAttack.enabled = true;
            weaponFollowPlayer.enabled = true;
        }
    }
}
