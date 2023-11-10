using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int SelectedWeapon = 0;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int preSelectedWeapon = SelectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if(SelectedWeapon >= transform.childCount - 1)
            {
                SelectedWeapon = 0;
            }
            else
            {
                SelectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (SelectedWeapon <= 0)
            {
                SelectedWeapon = transform.childCount - 1;
            }
            else
            {
                SelectedWeapon--;
            }
        }

        if (preSelectedWeapon != SelectedWeapon)
        {
            SelectWeapon();
        }

    }

    private void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == SelectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}