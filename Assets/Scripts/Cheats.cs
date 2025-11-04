using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{

    public MeleeAttack PlayerMeleeAttack;
    public NormalArrow PlayerNormalArrow;
    public bool PlayerInvencible = false;
    public float NewDamage = 1000f;
    public bool IsPressed = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("7"))
        {
            PlayerInvencible = !PlayerInvencible;
        }

        if (Input.GetKeyDown("6"))
        {
            if (IsPressed)
            {
                PlayerMeleeAttack.damage = NewDamage;
                PlayerNormalArrow.damage = NewDamage;
            }
            else
            {
                PlayerMeleeAttack.damage = 75f;
                PlayerNormalArrow.damage = 40f;
            }

            IsPressed = !IsPressed;
        }
    }
}
