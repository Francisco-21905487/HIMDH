using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int selectedWeapon = 0;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int preSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }

        if (preSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }

    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            weapon.gameObject.SetActive(i == selectedWeapon);

            i++;
        }
    }
}