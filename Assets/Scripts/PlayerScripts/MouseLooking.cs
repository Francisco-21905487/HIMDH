using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLooking : MonoBehaviour
{
    public GameObject Pivot;
    public GameObject Weapon;
    public Transform Player;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.z = 0;

        Vector3 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        if (mousePosition.x > Player.transform.position.x)
        {
            Pivot.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (mousePosition.x < Player.transform.position.x)
        {
            Pivot.transform.localScale = new Vector3(-1, 1, 1);
        }

        Pivot.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}