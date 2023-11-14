using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLooking : MonoBehaviour
{
    public GameObject PistolPivot;
    public GameObject pistol;
    public Transform Player;

    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (mousePosition.x < transform.position.x)
        {
            PistolPivot.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            PistolPivot.transform.localScale = new Vector3(1, 1, 1);
        }

        PistolPivot.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (mousePosition.x < Player.position.x)
        {
            transform.localScale = new Vector3(3, -3, 1);
        }

        if(mousePosition.x > Player.position.x)
        {
            transform.localScale = new Vector3(3, 3, 1);
        }


    }
}
