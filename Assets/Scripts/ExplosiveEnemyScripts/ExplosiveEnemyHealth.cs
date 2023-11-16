using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnemyHealth : MonoBehaviour
{
    public int Health;
    public int MaxHealth;

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    private void Update()
    {
        //Debug.Log(Health);
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
