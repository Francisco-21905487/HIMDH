using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLooking : MonoBehaviour
{
    public GameObject pivot;
    public GameObject weapon;
    public Transform player;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePosition.z = 0;

        Vector3 direction = (mousePosition - player.transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        if (mousePosition.x > player.transform.position.x)
        {
            pivot.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (mousePosition.x < player.transform.position.x)
        {
            pivot.transform.localScale = new Vector3(-1, 1, 1);
        }

        pivot.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}