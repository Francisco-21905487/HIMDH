using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public float heal;
    private GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Health playerHealth = player.GetComponent<Health>();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerHealth.GiveHealthFromKey(heal); 
        }
    }
}
