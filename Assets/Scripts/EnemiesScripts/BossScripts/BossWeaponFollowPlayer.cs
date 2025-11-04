using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponFollowPlayer : MonoBehaviour
{
    public GameObject weaponPivot;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        //WeaponPivot = GameObject.Find("BossShortSwordPivot");
    }
    void Update()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Debug.Log("Boss Direction" + direction);

        weaponPivot.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
